using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ImageFiles.Queries.GetList
{
    public class GetListImageFileQuery: IRequest<GetListResponse<GetListImageFileListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListImageFileQueryHandler : IRequestHandler<GetListImageFileQuery, GetListResponse<GetListImageFileListItemDto>>
        {
            private readonly IImageFileRepository _imageFileRepository;
            private IMapper _mapper;

            public GetListImageFileQueryHandler(IImageFileRepository imageFileRepository, IMapper mapper)
            {
                _imageFileRepository = imageFileRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListImageFileListItemDto>> Handle(GetListImageFileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ImageFile>? imageFiles = await _imageFileRepository
                    .GetListAsync(include:
                        iff => iff.Include(bf => bf.BlogFile),
                        index: request.PageRequest.PageIndex,
                        size: request.PageRequest.PageSize,
                        withDeleted:false,
                        cancellationToken:cancellationToken
                    );

                GetListResponse<GetListImageFileListItemDto> response = _mapper.Map<GetListResponse<GetListImageFileListItemDto>>(imageFiles);
                return response;
            }
        }
    }
}
