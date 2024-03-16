using Core.Persistance.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PPFileService
{
    public interface IPPFileService
    {
        Task<PPFile?> GetAsync(
           Expression<Func<PPFile, bool>> predicate,
           Func<IQueryable<PPFile>, IIncludableQueryable<PPFile, object>>? include = null,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
       );

        Task<IPaginate<PPFile>?> GetListAsync(
            Expression<Func<PPFile, bool>>? predicate = null,
            Func<IQueryable<PPFile>, IOrderedQueryable<PPFile>>? orderBy = null,
            Func<IQueryable<PPFile>, IIncludableQueryable<PPFile, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<PPFile> AddAsync(PPFile ppFile);
        Task<PPFile> UpdateAsync(PPFile ppFile);
        Task<PPFile> DeleteAsync(PPFile ppFile, bool permanent = false);
    }
}
