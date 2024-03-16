using Application.Features.Subjects.Commands.Create;
using Application.Features.Subjects.Commands.Delete;
using Application.Features.Subjects.Commands.Update;
using Application.Features.Subjects.Queries.GetById;
using Application.Features.Subjects.Queries.GetList;
using Application.Features.Subjects.Queries.GetListDetails;
using Application.Features.Subjects.Queries.GetListFromAuth;
using AutoMapper;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Subject, CreateSubjectCommand>().ReverseMap();
            CreateMap<Subject, CreatedSubjectResponse>().ReverseMap();

            CreateMap<Subject, UpdateSubjectCommad>().ReverseMap();
            CreateMap<Subject, UpdatedSubjectResponse>().ReverseMap();

            CreateMap<Subject, DeleteSubjectCommand>().ReverseMap();
            CreateMap<Subject, DeletedSubjectResponse>().ReverseMap();

            CreateMap<Subject, GetByIdSubjectQuery>().ReverseMap();
            CreateMap<Subject, GetByIdSubjectResponse>()
                .ForMember(destinationMember: s=> s.FirstName, memberOptions: opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(destinationMember: s => s.LastName, memberOptions: opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(destinationMember: s => s.CategoryName, memberOptions: opt => opt.MapFrom(s => s.Category.Name))
                .ReverseMap();

            CreateMap<Subject, GetListFromAuthSubjectListItemDto>()
                .ForMember(destinationMember: s => s.FirstName, memberOptions: opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(destinationMember: s => s.LastName, memberOptions: opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(destinationMember: s => s.CategoryName, memberOptions: opt => opt.MapFrom(s => s.Category.Name))
                .ReverseMap();
            CreateMap<IPaginate<Subject>, GetListResponse<GetListFromAuthSubjectListItemDto>>().ReverseMap();

            CreateMap<IPaginate<Subject>, GetListResponse<GetListSubjectListItemDto>>().ReverseMap();
            CreateMap<Subject, GetListSubjectListItemDto>().ReverseMap();

            CreateMap<Subject, GetListDetailSubjectListItemDto>()
                .ForMember(destinationMember: s => s.FirstName, memberOptions: opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(destinationMember: s => s.LastName, memberOptions: opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(destinationMember: s => s.Email, memberOptions: opt => opt.MapFrom(s => s.User.Email))
                .ForMember(destinationMember: s => s.CategoryName, memberOptions: opt => opt.MapFrom(s => s.Category.Name))
                .ReverseMap();
            CreateMap<IPaginate<Subject>, GetListResponse<GetListDetailSubjectListItemDto>>().ReverseMap();
        }
    }
}
