using Application.Features.ImageFiles.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConserns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ImageFiles.Rules
{
    public class ImageFileBusinessRules: BaseBusinessRules
    {
        private readonly IImageFileRepository _imageFileRepository;

        public ImageFileBusinessRules(IImageFileRepository imageFileRepository)
        {
            _imageFileRepository = imageFileRepository;
        }

        public Task FileIsImageFile(string fileExtension)
        {
            string[] extensionList = { ".gif", ".png", ".jpg", ".jpeg" };

            bool isSuccess = extensionList.Any(extension => extension.Contains(fileExtension));
            if (isSuccess)
                return Task.CompletedTask;

            throw new Exception("Dosya Image Dosyası Değil!");
        }

        public async Task ImageFileShouldBeExistsWhenSelected(Guid id)
        {
            bool doesExists = await _imageFileRepository.AnyAsync(x => x.Id == id);
            if (!doesExists)
                throw new BusinessException(ImageFileMessages.ImageFileNotExists);

        }
    }
}
