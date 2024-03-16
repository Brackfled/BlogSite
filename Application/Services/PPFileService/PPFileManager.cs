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
    public class PPFileManager : IPPFileService
    {
        private readonly IPPFileService _ppFileService;

        public PPFileManager(IPPFileService fileService)
        {
            _ppFileService = fileService;
        }

        public async Task<PPFile> AddAsync(PPFile ppFile)
        {
            PPFile addedPPFile = await _ppFileService.AddAsync(ppFile);
            return addedPPFile;
        }

        public async Task<PPFile> DeleteAsync(PPFile ppFile, bool permanent = false)
        {
            PPFile deletedPPFile = await _ppFileService.DeleteAsync(ppFile);
            return deletedPPFile;
        }

        public async Task<PPFile?> GetAsync(Expression<Func<PPFile, bool>> predicate, Func<IQueryable<PPFile>, IIncludableQueryable<PPFile, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            PPFile? ppFile = await _ppFileService.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
            return ppFile;
        }

        public async Task<IPaginate<PPFile>?> GetListAsync(Expression<Func<PPFile, bool>>? predicate = null, Func<IQueryable<PPFile>, IOrderedQueryable<PPFile>>? orderBy = null, Func<IQueryable<PPFile>, IIncludableQueryable<PPFile, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IPaginate<PPFile>? ppFiles = await _ppFileService.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
            return ppFiles;
        }

        public async Task<PPFile> UpdateAsync(PPFile ppFile)
        {
            PPFile updatedPPFile = await _ppFileService.UpdateAsync(ppFile);
            return updatedPPFile;
        }
    }
}
