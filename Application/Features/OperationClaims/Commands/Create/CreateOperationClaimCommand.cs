﻿using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.Create
{
    public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimResponse>, ISecuredRequest, ILoggableRequest
    {
        public string Name { get; set; }

        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin}; 

        public CreateOperationClaimCommand()
        {
            Name = string.Empty;
        }

        public CreateOperationClaimCommand(string name)
        {
            Name = name;
        }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimResponse>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public CreateOperationClaimCommandHandler(
                IOperationClaimRepository operationClaimRepository,
                IMapper mapper,
                OperationClaimBusinessRules operationClaimBusinessRules
            )
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<CreatedOperationClaimResponse> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenCreating(request.Name);
                OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);

                OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);

                CreatedOperationClaimResponse response = _mapper.Map<CreatedOperationClaimResponse>(createdOperationClaim);
                return response;
            }
        }
    }
}
