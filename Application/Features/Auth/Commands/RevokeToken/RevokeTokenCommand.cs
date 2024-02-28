using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<RevokedTokenResponse>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }

        public RevokeTokenCommand()
        {
            Token = string.Empty;
            IpAddress = string.Empty;
        }

        public RevokeTokenCommand(string token, string ıpAddress)
        {
            Token = token;
            IpAddress = ıpAddress;
        }

        public class RevokeTokenCommandHandler: IRequestHandler<RevokeTokenCommand, RevokedTokenResponse>
        {
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IMapper _mapper;

            public RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper)
            {
                _authService = authService;
                _authBusinessRules = authBusinessRules;
                _mapper = mapper;
            }

            public async Task<RevokedTokenResponse> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
            {
                Core.Security.Entities.RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.Token);
                await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);
                await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

                await _authService.RevokeRefreshToken(refreshToken, request.IpAddress, reason: "Revoked without replacement");

                RevokedTokenResponse response = _mapper.Map<RevokedTokenResponse>(refreshToken!);
                return response;

                
            }
        }
    }
}
