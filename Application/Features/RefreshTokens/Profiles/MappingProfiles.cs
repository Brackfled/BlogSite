using Application.Features.OperationClaims.Queries.GetList;
using Application.Features.RefreshTokens.Queries.GetList;
using AutoMapper;
using Core.Application.Response;
using Core.Persistance.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RefreshTokens.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() 
        {
            CreateMap<RefreshToken, GetListResponse<GetListRefreshTokenListItemResponse>>().ReverseMap();
            CreateMap<Paginate<RefreshToken>, GetListResponse<GetListRefreshTokenListItemResponse>>().ReverseMap();
            CreateMap<RefreshToken, GetListRefreshTokenListItemResponse>().ReverseMap();
        }
    }
}
