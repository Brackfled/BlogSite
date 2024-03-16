using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using Infrastructure.Stroage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Queries.GetListPPFile
{
    public class GetListPPFileQuery: IRequest<GetListResponse<GetListPPFileListItemDto>>
    {
        public class GetListPPFileQueryHandler: IRequestHandler<GetListPPFileQuery, GetListResponse<GetListPPFileListItemDto>>
        {
            private readonly IPPFileRepository _pPFileRepository;
            private readonly IStroage _stroage;
            private IMapper _mapper;

            public GetListPPFileQueryHandler(IPPFileRepository pPFileRepository, IStroage stroage, IMapper mapper)
            {
                _pPFileRepository = pPFileRepository;
                _stroage = stroage;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListPPFileListItemDto>> Handle(GetListPPFileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<PPFile>? ppFiles = await _pPFileRepository.GetListAsync(
                    include: z => z.Include(z => z.User),
                    index: 0, size: 1000, withDeleted: false, cancellationToken:cancellationToken);

                GetListResponse<GetListPPFileListItemDto> response = _mapper.Map<GetListResponse<GetListPPFileListItemDto>>(ppFiles);
                return response;
            }
        }
    }
}
