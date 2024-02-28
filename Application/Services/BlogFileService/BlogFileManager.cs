using Application.Services.Repositories;
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
    public class BlogFileManager : IBlogFileService
    {

        private readonly IBlogFileRepository _blogFileRepository;

        public BlogFileManager(IBlogFileRepository blogFileRepository)
        {
            _blogFileRepository = blogFileRepository;
        }

        public async Task<BlogFile> AddAsync(BlogFile blogFile)
        {
            BlogFile addenBlogFile = await _blogFileRepository.AddAsync(blogFile);
            return addenBlogFile;
        }

        public async Task<BlogFile> DeleteAsync(BlogFile blogFile, bool permanent = false)
        {
            BlogFile deletedBlogFile = await _blogFileRepository.DeleteAsync(blogFile);
            return deletedBlogFile;
        }

        public async Task<BlogFile?> GetAsync(Expression<Func<BlogFile, bool>> predicate, Func<IQueryable<BlogFile>, IIncludableQueryable<BlogFile, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            BlogFile? blogFile = await _blogFileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
            return blogFile;
        }

        public async Task<IPaginate<BlogFile>?> GetListAsync(Expression<Func<BlogFile, bool>>? predicate = null, Func<IQueryable<BlogFile>, IOrderedQueryable<BlogFile>>? orderBy = null, Func<IQueryable<BlogFile>, IIncludableQueryable<BlogFile, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IPaginate<BlogFile>? blogFiles = await _blogFileRepository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
            return blogFiles;
        }

        public async Task<BlogFile> UpdateAsync(BlogFile blogFile)
        {
            BlogFile updatedBlogFile = await _blogFileRepository.UpdateAsync(blogFile);
            return updatedBlogFile;
        }
    }
}
