﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RevokeToken
{
    public class RevokedTokenResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public RevokedTokenResponse()
        {
            Token = string.Empty;
        }

        public RevokedTokenResponse(int id, string token)
        {
            Id = id;
            Token = token;
        }
    }
}
