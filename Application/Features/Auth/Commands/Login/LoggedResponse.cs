using Core.Security.Dtos;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login
{
    public class LoggedResponse
    {
        public AccessToken AccessToken { get; set; }
        public Core.Security.Entities.RefreshToken RefreshToken { get; set; }
    }
}
