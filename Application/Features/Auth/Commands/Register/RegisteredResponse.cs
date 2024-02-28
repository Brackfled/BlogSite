using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisteredResponse
    {
        public AccessToken AccessToken { get; set; }
        public Core.Security.Entities.RefreshToken RefreshToken { get; set; }

        public RegisteredResponse()
        {
            AccessToken = null!;
            RefreshToken = null!;
        }

        public RegisteredResponse(AccessToken accessToken, Core.Security.Entities.RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
