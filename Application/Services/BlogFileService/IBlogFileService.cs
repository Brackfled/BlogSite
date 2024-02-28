using Core.Persistance.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BlogFileService
{
    public interface IBlogFileService
    {
        Task<BlogFile?> GetAsync(
           Expression<Func<BlogFile, bool>> predicate,
           Func<IQueryable<BlogFile>, IIncludableQueryable<BlogFile, object>>? include = null,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
       );

        Task<IPaginate<BlogFile>?> GetListAsync(
            Expression<Func<BlogFile, bool>>? predicate = null,
            Func<IQueryable<BlogFile>, IOrderedQueryable<BlogFile>>? orderBy = null,
            Func<IQueryable<BlogFile>, IIncludableQueryable<BlogFile, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<BlogFile> AddAsync(BlogFile blogFile);
        Task<BlogFile> UpdateAsync(BlogFile blogFile);
        Task<BlogFile> DeleteAsync(BlogFile blogFile, bool permanent = false);
    }
}
