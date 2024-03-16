using Core.Persistance.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SubjectImageFileService
{
    public interface ISubjectImageFileService
    {
        Task<SubjectImageFile?> GetAsync(
           Expression<Func<SubjectImageFile, bool>> predicate,
           Func<IQueryable<SubjectImageFile>, IIncludableQueryable<SubjectImageFile, object>>? include = null,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
       );

        Task<IPaginate<SubjectImageFile>?> GetListAsync(
            Expression<Func<SubjectImageFile, bool>>? predicate = null,
            Func<IQueryable<SubjectImageFile>, IOrderedQueryable<SubjectImageFile>>? orderBy = null,
            Func<IQueryable<SubjectImageFile>, IIncludableQueryable<SubjectImageFile, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<SubjectImageFile> AddAsync(SubjectImageFile subjectImageFile);
        Task<SubjectImageFile> UpdateAsync(SubjectImageFile subjectImageFile);
        Task<SubjectImageFile> DeleteAsync(SubjectImageFile subjectImageFile, bool permanent = false);
    }
}
