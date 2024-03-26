using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
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

namespace Application.Features.Files.Queries.GetListSubjectImageFile
{
    public class GetListSubjectImageFileQuery: IRequest<GetListResponse<GetListSubjectImageFileListItemDto>>
    {
        public class GetListSubjectImageFileQueryHandler: IRequestHandler<GetListSubjectImageFileQuery, GetListResponse<GetListSubjectImageFileListItemDto>>
        {
            private readonly ISubjectImageFileRepository _subjectImageFileRepository;
            private IMapper _mapper;

            public GetListSubjectImageFileQueryHandler(ISubjectImageFileRepository subjectImageFileRepository, IMapper mapper)
            {
                _subjectImageFileRepository = subjectImageFileRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListSubjectImageFileListItemDto>> Handle(GetListSubjectImageFileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<SubjectImageFile>? files = await _subjectImageFileRepository.GetListAsync( index:0,
                                                                                                    size:1000,
                                                                                                    withDeleted:false,
                                                                                                    cancellationToken:cancellationToken
                                                                                                    );

                GetListResponse<GetListSubjectImageFileListItemDto> response = _mapper.Map<GetListResponse<GetListSubjectImageFileListItemDto>>(files);
                return response;
            }
        }
    }
}
