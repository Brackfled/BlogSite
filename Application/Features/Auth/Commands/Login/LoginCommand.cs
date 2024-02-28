using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand: IRequest<LoggedResponse>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public LoginCommand()
        {
            UserForLoginDto = null!;
            IpAddress = string.Empty;
        }

        public LoginCommand(UserForLoginDto userForLoginDto, string ıpAddress)
        {
            UserForLoginDto = userForLoginDto;
            IpAddress = ıpAddress;
        }

        public class LoginCommandHandler: IRequestHandler<LoginCommand, LoggedResponse>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IAuthService _authService;
            private readonly IUserService _userService;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, IAuthService authService, IUserService userService)
            {
                _authBusinessRules = authBusinessRules;
                _authService = authService;
                _userService = userService;
            }

            public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userService.GetAsync(predicate:u => u.Email == request.UserForLoginDto.Email, cancellationToken:cancellationToken);
                await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
                await _authBusinessRules.UserPasswordShouldBeMatch(user!.Id, request.UserForLoginDto.Password);

                LoggedResponse loggedResponse = new();

                // Burada Doğrulana sistemi olacak

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

                Core.Security.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                Core.Security.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
                await _authService.DeleteOldRefreshTokens(user!.Id);

                loggedResponse.AccessToken = createdAccessToken;
                loggedResponse.RefreshToken = addedRefreshToken;
                return loggedResponse;
            }
        }
    }
}
