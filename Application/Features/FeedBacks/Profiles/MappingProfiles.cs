using Application.Features.FeedBacks.Commands.Create;
using Application.Features.FeedBacks.Commands.Delete;
using Application.Features.FeedBacks.Queries.GetList;
using AutoMapper;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedBacks.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<FeedBack, CreateFeedBackCommand>().ReverseMap();
            CreateMap<FeedBack, CreatedFeedBackResponse>().ReverseMap();

            CreateMap<FeedBack, DeleteFeedBackCommand>().ReverseMap();
            CreateMap<FeedBack, DeletedFeedBackResponse>().ReverseMap();

            CreateMap<FeedBack, GetListFeedBackListItemDto>().ReverseMap();
            CreateMap<IPaginate<FeedBack>, GetListResponse<GetListFeedBackListItemDto>>().ReverseMap();
        }
    }
}
