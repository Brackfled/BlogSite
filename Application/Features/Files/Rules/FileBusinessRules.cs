using Application.Features.Files.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConserns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Rules
{
    public class FileBusinessRules: BaseBusinessRules
    {

        private readonly IPPFileRepository _ppFileRepository;

        public FileBusinessRules(IPPFileRepository ppFileRepository)
        {
            _ppFileRepository = ppFileRepository;
        }

        public Task FileIsImageFile(string fileExtension)
        {
            string[] extensionList = { ".gif", ".png", ".jpg", ".jpeg" };

            bool isSuccess = extensionList.Any(extension => extension.Contains(fileExtension));
            if (isSuccess)
                return Task.CompletedTask;

            throw new Exception("Dosya Image Dosyası Değil!");
        }

        public async Task<Task> OneUserOnePPFile(int userId)
        {
            bool result = await _ppFileRepository.AnyAsync(predicate: pp =>pp.UserId == userId);

            if (result)
                throw new BusinessException(FileMessages.OneUserOnePPFile);

            return Task.CompletedTask;
        }

        public Task ImageSizeControl(IFormFile formFile, int maxWidht, int maxHeight)
        {
            int widht;
            int height;
            using (var imageStream = formFile.OpenReadStream())
            {
                using (var image = SixLabors.ImageSharp.Image.Load(imageStream))
                {
                    widht = image.Width;
                    height = image.Height;
                }
            }

            if (widht > maxWidht || height > maxHeight)
            {
                throw new BusinessException(FileMessages.FileSizeBigger);
            }
            return Task.CompletedTask;
        }
    }
}
