﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetList
{
    public class GetListOperationClaimQuery : IRequest<GetListResponse<GetListOperationClaimListItemDto>>, ISecuredRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };

        public PageRequest PageRequest { get; set; }

        public GetListOperationClaimQuery()
        {
            PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
        }

        public GetListOperationClaimQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public class GetListOperationClaimQueryHandler
            : IRequestHandler<GetListOperationClaimQuery, GetListResponse<GetListOperationClaimListItemDto>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListOperationClaimListItemDto>> Handle(
                GetListOperationClaimQuery request,
                CancellationToken cancellationToken
            )
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListOperationClaimListItemDto> response = _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(
                    operationClaims
                );
                return response;
            }
        }
    }
}
