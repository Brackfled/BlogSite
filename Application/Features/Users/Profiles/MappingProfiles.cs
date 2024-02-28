using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Response;
using Core.Persistance.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, CreatedUserResponse>().ReverseMap();

            CreateMap<User, DeleteUserCommand>().ReverseMap();
            CreateMap<User, DeletedUserResponse>().ReverseMap();

            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, UpdatedUserResponse>().ReverseMap();

            CreateMap<User, GetByIdUserResponse>().ReverseMap();

            CreateMap<User, GetListUserQuery>().ReverseMap();
            CreateMap<User, GetListUserListItemDto>().ReverseMap();
            CreateMap<Paginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
        }

    }
}
