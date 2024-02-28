using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshedTokenResponse
    {
        public AccessToken AccessToken { get; set; }
        public Core.Security.Entities.RefreshToken RefreshToken { get; set; }

        public RefreshedTokenResponse()
        {
            AccessToken = null!;
            RefreshToken = null!;
        }

        public RefreshedTokenResponse(AccessToken accessToken, Core.Security.Entities.RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
