using Core.Persistance.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ImageFileService
{
    public interface IImageFileService
    {
        Task<ImageFile?> GetAsync(
           Expression<Func<ImageFile, bool>> predicate,
           Func<IQueryable<ImageFile>, IIncludableQueryable<ImageFile, object>>? include = null,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
       );

        Task<IPaginate<ImageFile>?> GetListAsync(
            Expression<Func<ImageFile, bool>>? predicate = null,
            Func<IQueryable<ImageFile>, IOrderedQueryable<ImageFile>>? orderBy = null,
            Func<IQueryable<ImageFile>, IIncludableQueryable<ImageFile, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<ImageFile> AddAsync(ImageFile imageFile);
        Task<ImageFile> UpdateAsync(ImageFile imageFile);
        Task<ImageFile> DeleteAsync(ImageFile imageFile, bool permanent = false);
    }
}
