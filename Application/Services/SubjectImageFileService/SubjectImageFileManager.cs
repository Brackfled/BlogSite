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
    public class SubjectImageFileManager : ISubjectImageFileService
    {
        private readonly ISubjectImageFileService _subjectImageFileService;

        public SubjectImageFileManager(ISubjectImageFileService subjectImageFileService)
        {
            _subjectImageFileService = subjectImageFileService;
        }

        public async Task<SubjectImageFile> AddAsync(SubjectImageFile subjectImageFile)
        {
            SubjectImageFile addedSubjectImageFile = await _subjectImageFileService.AddAsync(subjectImageFile);
            return addedSubjectImageFile;
        }

        public async Task<SubjectImageFile> DeleteAsync(SubjectImageFile subjectImageFile, bool permanent = false)
        {
            SubjectImageFile deletedSubjectImageFile = await _subjectImageFileService.DeleteAsync(subjectImageFile);
            return deletedSubjectImageFile;
        }

        public async Task<SubjectImageFile?> GetAsync(Expression<Func<SubjectImageFile, bool>> predicate, Func<IQueryable<SubjectImageFile>, IIncludableQueryable<SubjectImageFile, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            SubjectImageFile? subjectImageFile = await _subjectImageFileService.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
            return subjectImageFile;
        }

        public async Task<IPaginate<SubjectImageFile>?> GetListAsync(Expression<Func<SubjectImageFile, bool>>? predicate = null, Func<IQueryable<SubjectImageFile>, IOrderedQueryable<SubjectImageFile>>? orderBy = null, Func<IQueryable<SubjectImageFile>, IIncludableQueryable<SubjectImageFile, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IPaginate<SubjectImageFile>? subjetcImageFiles = await _subjectImageFileService.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
            return subjetcImageFiles;
        }

        public async Task<SubjectImageFile> UpdateAsync(SubjectImageFile subjectImageFile)
        {
            SubjectImageFile updatedSubjectImageFile = await _subjectImageFileService.UpdateAsync(subjectImageFile);
            return updatedSubjectImageFile;
        }
    }
}
