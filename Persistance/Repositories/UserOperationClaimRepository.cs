using Application.Services.Repositories;
using Core.Persistance.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserOperationClaimRepository:EfRepositoryBase<UserOperationClaim, int, BaseDbContext>,IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(BaseDbContext context):base(context) { }

        public async Task<IList<OperationClaim>> GetUserOperationClaimsByUserId(int userId)
        {
            List<OperationClaim> operationClaims = await Query()
                .AsNoTracking()
                .Where(p => p.UserId.Equals(userId))
                .Select(p => new OperationClaim { Id = p.OperationClaimId, Name = p.OperationClaim.Name })
                .ToListAsync();
            return operationClaims;
        }
    }
}
