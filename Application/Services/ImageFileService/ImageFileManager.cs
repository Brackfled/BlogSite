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

namespace Application.Services.ImageFileService
{
    public class ImageFileManager : IImageFileService
    {
        private readonly IImageFileRepository _imageFileRepository;

        public ImageFileManager(IImageFileRepository imageFileRepository)
        {
            _imageFileRepository = imageFileRepository;
        }

        public async Task<ImageFile> AddAsync(ImageFile imageFile)
        {
            ImageFile addedImageFile = await _imageFileRepository.AddAsync(imageFile);
            return addedImageFile;
        }

        public async Task<ImageFile> DeleteAsync(ImageFile imageFile, bool permanent = false)
        {
            ImageFile deletedImageFile = await _imageFileRepository.DeleteAsync(imageFile);
            return deletedImageFile;
        }

        public async Task<ImageFile?> GetAsync(Expression<Func<ImageFile, bool>> predicate, Func<IQueryable<ImageFile>, IIncludableQueryable<ImageFile, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            ImageFile? imageFile = await _imageFileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
            return imageFile;
        }

        public async Task<IPaginate<ImageFile>?> GetListAsync(Expression<Func<ImageFile, bool>>? predicate = null, Func<IQueryable<ImageFile>, IOrderedQueryable<ImageFile>>? orderBy = null, Func<IQueryable<ImageFile>, IIncludableQueryable<ImageFile, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IPaginate<ImageFile>? imageFiles = await _imageFileRepository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking);
            return imageFiles;
        }

        public async Task<ImageFile> UpdateAsync(ImageFile imageFile)
        {
            ImageFile updatedImageFile = await _imageFileRepository.UpdateAsync(imageFile);
            return updatedImageFile;
        }
    }
}
