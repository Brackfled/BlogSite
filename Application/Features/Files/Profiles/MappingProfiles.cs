
using Application.Features.Files.Commands.CreateSubjectImageFile;
using Application.Features.Files.Queries.GetByIdSubjectImageFile;
using Application.Features.Files.Queries.GetListPPFile;
using Application.Features.Files.Queries.GetListSubjectImageFile;
using AutoMapper;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {

            // PPFile ile ilgili Mappingler
            CreateMap<PPFile, GetListPPFileListItemDto>()
                .ForMember(destinationMember: x => x.UserFirstName, memberOptions: opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(destinationMember: x => x.UserLastName, memberOptions: opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(destinationMember: x => x.UserEmail, memberOptions: opt => opt.MapFrom(x => x.User.Email))
                .ReverseMap();
            CreateMap<IPaginate<PPFile>, GetListResponse<GetListPPFileListItemDto>>().ReverseMap();

            CreateMap<PPFile, GetListPPFileListItemDto>()
                .ForMember(destinationMember: x => x.UserFirstName, memberOptions: opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(destinationMember: x => x.UserLastName, memberOptions: opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(destinationMember: x => x.UserEmail, memberOptions: opt => opt.MapFrom(x => x.User.Email))
                .ReverseMap();


            // SubjectImageFile ile ilgi Mappingler

            CreateMap<SubjectImageFile, CreatedSubjectImageFileResponse>().ReverseMap();

            CreateMap<SubjectImageFile, GetListSubjectImageFileListItemDto>().ReverseMap();
            CreateMap<IPaginate<SubjectImageFile>, GetListResponse<GetListSubjectImageFileListItemDto>>().ReverseMap();

            CreateMap<SubjectImageFile, GetByIdSubjectImageFileDto>().ReverseMap();
        }
    }
}
