using Application.Features.Auth.Commands.RevokeToken;
using AutoMapper;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
        }
    }
}
