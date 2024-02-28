using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand: IRequest<RefreshedTokenResponse>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }

        public RefreshTokenCommand()
        {
            RefreshToken = string.Empty;
            IpAddress = string.Empty;
        }

        public RefreshTokenCommand(string refreshToken, string ıpAddress)
        {
            RefreshToken = refreshToken;
            IpAddress = ıpAddress;
        }

        public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, RefreshedTokenResponse>
        {
            private readonly IAuthService _authService;
            private readonly IUserService _userService;
            private readonly AuthBusinessRules _authBusinessRules;

            public RefreshTokenCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules)
            {
                _authService = authService;
                _userService = userService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<RefreshedTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                Core.Security.Entities.RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.RefreshToken);

                await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);

                if (refreshToken!.Revoked != null)
                    await _authService.RevokeDescendantRefreshTokens(
                            refreshToken,
                            request.IpAddress,
                            reason: $"Attemped reuse of revoked ancestor token:{refreshToken.Token}"
                        ) ;
                await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

                User? user = await _userService.GetAsync(predicate: u => u.Id == refreshToken.UserId, cancellationToken:cancellationToken);
                await _authBusinessRules.UserShouldBeExistsWhenSelected(user);

                Core.Security.Entities.RefreshToken newRefreshToken = await _authService.RotateRefreshToken(
                        user: user!,
                        refreshToken: refreshToken,
                        request.IpAddress
                    );
                Core.Security.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(newRefreshToken);
                await _authService.DeleteOldRefreshTokens(refreshToken.UserId);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user!);

                RefreshedTokenResponse response = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
                return response;

            }
        }
    }
}
