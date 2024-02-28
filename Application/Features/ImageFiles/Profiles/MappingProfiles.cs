using Application.Features.ImageFiles.Commands.Delete;
using Application.Features.ImageFiles.Queries.GetList;
using AutoMapper;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ImageFiles.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {

            CreateMap<ImageFile, DeleteImageFileCommand>()
                .ReverseMap();
            CreateMap<ImageFile, DeletedImageFileResponse>().ReverseMap();

            CreateMap<ImageFile, GetListImageFileListItemDto>()
                .ForMember(destinationMember: iff => iff.UserId, memberOptions:opt => opt.MapFrom(iff => iff.BlogFile.UserId))
                .ForMember(destinationMember: iff => iff.BlogFileId, memberOptions: opt => opt.MapFrom(iff => iff.BlogFile.Id))
                .ForMember(destinationMember: iff => iff.Name, memberOptions: opt => opt.MapFrom(iff => iff.BlogFile.Name))
                .ForMember(destinationMember: iff => iff.FilePath , memberOptions: opt => opt.MapFrom(iff => iff.BlogFile.FilePath))
                .ForMember(destinationMember: iff => iff.FileUrl, memberOptions: opt => opt.MapFrom(iff => iff.BlogFile.FileUrl))
                .ReverseMap();
            CreateMap<IPaginate<ImageFile>, GetListResponse<GetListImageFileListItemDto>>().ReverseMap();
        }
    }
}
