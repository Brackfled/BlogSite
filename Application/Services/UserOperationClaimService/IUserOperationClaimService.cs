using Core.Persistance.Paging;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserOperationClaimService
{
    public interface IUserOperationClaimService
    {
        Task<UserOperationClaim?> GetAsync(
        Expression<Func<UserOperationClaim, bool>> predicate,
        Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

        Task<IPaginate<UserOperationClaim>?> GetListAsync(
            Expression<Func<UserOperationClaim, bool>>? predicate = null,
            Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null,
            Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<UserOperationClaim> AddAsync(UserOperationClaim userOperationClaim);
        Task<UserOperationClaim> UpdateAsync(UserOperationClaim userOperationClaim);
        Task<UserOperationClaim> DeleteAsync(UserOperationClaim userOperationClaim, bool permanent = false);
    }
}
