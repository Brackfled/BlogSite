using Application.Features.OperationClaims.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConserns.Exceptions.Types;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules: BaseBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimNameShouldNotExistWhenCreating(string name)
        {
            bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Name == name, enableTracking: false);
            if (doesExist)
                throw new BusinessException(OperationClaimsMessages.AlreadyExists);
        }

        public async Task OperationClaimNameShouldNotExistWhenUpdating(int id, string name)
        {
            bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Id != id && b.Name == name, enableTracking: false);
            if (doesExist)
                throw new BusinessException(OperationClaimsMessages.AlreadyExists);
        }

        public Task OperationClaimShouldExistWhenSelected(OperationClaim? operationClaim)
        {
            if (operationClaim == null)
                throw new BusinessException(OperationClaimsMessages.NotExists);
            return Task.CompletedTask;
        }
    }
}
