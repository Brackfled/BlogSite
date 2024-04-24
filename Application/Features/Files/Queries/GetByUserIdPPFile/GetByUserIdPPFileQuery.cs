using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Queries.GetByUserIdPPFile
{
    public class GetByUserIdPPFileQuery: IRequest<GetByUserIdPPFileResponse>
    {
        public int UserId { get; set; }

        public class GetByUserIdPPFileQueryHandler: IRequestHandler<GetByUserIdPPFileQuery, GetByUserIdPPFileResponse>
        {
            private readonly IPPFileRepository _ppFileRepository;
            private IMapper _mapper;

            public GetByUserIdPPFileQueryHandler(IPPFileRepository ppFileRepository, IMapper mapper)
            {
                _ppFileRepository = ppFileRepository;
                _mapper = mapper;
            }

            public async Task<GetByUserIdPPFileResponse> Handle(GetByUserIdPPFileQuery request, CancellationToken cancellationToken)
            {
                PPFile? ppFile = await _ppFileRepository.GetAsync(predicate: pp => pp.UserId == request.UserId,
                                                                  cancellationToken:cancellationToken
                );

                GetByUserIdPPFileResponse response = _mapper.Map<GetByUserIdPPFileResponse>(ppFile);
                return response;
            }
        }
    }
}
