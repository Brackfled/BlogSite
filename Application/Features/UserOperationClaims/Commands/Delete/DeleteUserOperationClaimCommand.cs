﻿using Application.Features.UserOperationClaims.Rules;
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

namespace Application.Features.UserOperationClaims.Commands.Delete
{
    public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimResponse>, ISecuredRequest, ILoggableRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };

        public int Id { get; set; }

        public class DeleteUserOperationClaimCommandHandler
            : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimResponse>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public DeleteUserOperationClaimCommandHandler(
                IUserOperationClaimRepository userOperationClaimRepository,
                IMapper mapper,
                UserOperationClaimBusinessRules userOperationClaimBusinessRules
            )
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<DeletedUserOperationClaimResponse> Handle(
                DeleteUserOperationClaimCommand request,
                CancellationToken cancellationToken
            )
            {
                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(
                    predicate: uoc => uoc.Id == request.Id,
                    cancellationToken: cancellationToken
                );
                await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);

                await _userOperationClaimRepository.DeleteAsync(userOperationClaim!);

                DeletedUserOperationClaimResponse response = _mapper.Map<DeletedUserOperationClaimResponse>(userOperationClaim);
                return response;
            }
        }
    }
}
