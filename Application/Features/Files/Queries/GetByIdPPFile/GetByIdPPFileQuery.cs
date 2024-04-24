using Amazon.Runtime.Internal;
using Application.Features.Files.Constants;
using Application.Features.Files.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConserns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Queries.GetByIdPPFile
{
    public class GetByIdPPFileQuery: IRequest<GetByIdPPFileDto>, ISecuredRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };

        public Guid Id { get; set; }

        public class GetByIdPPFileQueryHandler: IRequestHandler<GetByIdPPFileQuery, GetByIdPPFileDto>
        {
            private readonly IPPFileRepository _ppFileRepository;
            private readonly FileBusinessRules _businessRules;
            private IMapper _mapper;

            public async Task<GetByIdPPFileDto> Handle(GetByIdPPFileQuery request, CancellationToken cancellationToken)
            {
                PPFile? ppFile = await _ppFileRepository.GetAsync(predicate:x => x.Id == request.Id,
                                                                  include:x => x.Include(x => x.User),
                                                                  cancellationToken:cancellationToken
                                                                 );

                if (ppFile == null)
                    throw new BusinessException(FileMessages.PPIsNotExists);

                GetByIdPPFileDto response = _mapper.Map<GetByIdPPFileDto>(ppFile);
                return response;
            }
        }
    }
}
