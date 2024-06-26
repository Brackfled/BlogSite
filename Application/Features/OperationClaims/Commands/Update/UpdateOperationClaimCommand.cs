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

namespace Application.Features.OperationClaims.Commands.Update
{
    public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimResponse>, ISecuredRequest, ILoggableRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };

        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateOperationClaimCommand()
        {
            Name = string.Empty;
        }

        public UpdateOperationClaimCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimResponse>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public UpdateOperationClaimCommandHandler(
                IOperationClaimRepository operationClaimRepository,
                IMapper mapper,
                OperationClaimBusinessRules operationClaimBusinessRules
            )
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<UpdatedOperationClaimResponse> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
                    predicate: oc => oc.Id == request.Id,
                    cancellationToken: cancellationToken
                );
                await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);
                await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenUpdating(request.Id, request.Name);
                OperationClaim mappedOperationClaim = _mapper.Map(request, destination: operationClaim!);

                OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(mappedOperationClaim);

                UpdatedOperationClaimResponse response = _mapper.Map<UpdatedOperationClaimResponse>(updatedOperationClaim);
                return response;
            }
        }
    }
}
