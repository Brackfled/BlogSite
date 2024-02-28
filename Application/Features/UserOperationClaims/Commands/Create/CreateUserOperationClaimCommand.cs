using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.Create
{
    public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimResponse>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class CreateUserOperationClaimCommandHandler
            : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimResponse>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public CreateUserOperationClaimCommandHandler(
                IUserOperationClaimRepository userOperationClaimRepository,
                IMapper mapper,
                UserOperationClaimBusinessRules userOperationClaimBusinessRules
            )
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<CreatedUserOperationClaimResponse> Handle(
                CreateUserOperationClaimCommand request,
                CancellationToken cancellationToken
            )
            {
                await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
                    request.UserId,
                    request.OperationClaimId
                );
                UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);

                UserOperationClaim createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);

                CreatedUserOperationClaimResponse createdUserOperationClaimDto = _mapper.Map<CreatedUserOperationClaimResponse>(
                    createdUserOperationClaim
                );
                return createdUserOperationClaimDto;
            }
        }
    }
}
