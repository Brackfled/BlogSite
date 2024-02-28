using Application.Features.ImageFiles.Rules;
using Application.Features.Users.Rules;
using Application.Services.BlogFileService;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Stroage;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ImageFiles.Commands.Create
{
    public class CreatedImageFileCommand:IRequest<CreatedImageFileResponse>
    {
        public int UserId { get; set; }
        public string PathOrContainerName { get; set; }
        public ImageFileBracket ImageFileBracket { get; set; }
        public IFormFile FormFile { get; set; }

        public class CreateImageFileCommandHanler : IRequestHandler<CreatedImageFileCommand, CreatedImageFileResponse>
        {
            private readonly IBlogFileService _blogFileService;
            private readonly IImageFileRepository _imageFileRepository;
            private readonly IStroage _stroage;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly ImageFileBusinessRules _imageFileBusinessRules;

            public CreateImageFileCommandHanler(IBlogFileService blogFileService, IImageFileRepository imageFileRepository, IStroage stroage, UserBusinessRules userBusinessRules, ImageFileBusinessRules imageFileBusinessRules)
            {
                _blogFileService = blogFileService;
                _imageFileRepository = imageFileRepository;
                _stroage = stroage;
                _userBusinessRules = userBusinessRules;
                _imageFileBusinessRules = imageFileBusinessRules;
            }

            public async Task<CreatedImageFileResponse> Handle(CreatedImageFileCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdShouldExistWhenSelected(request.UserId);
                await _imageFileBusinessRules.FileIsImageFile(request.FormFile.FileName.Substring(request.FormFile.FileName.LastIndexOf('.')));

                (string uniqeFileName, string pathOrContainerName) uploadedFile = await _stroage.UploadAsync(request.PathOrContainerName, request.FormFile);

                string fileUrl = await _stroage.GetFileUrl(uploadedFile.uniqeFileName, request.PathOrContainerName);

                BlogFile blogFile = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    FilePath = request.PathOrContainerName,
                    Name = uploadedFile.uniqeFileName,
                    FileUrl = fileUrl
                };

                await _blogFileService.AddAsync(blogFile);

                ImageFile imageFile = new()
                {
                    Id = Guid.NewGuid(),
                    BlogFileId = blogFile.Id,
                    ImageFileBracket = request.ImageFileBracket
                };

                await _imageFileRepository.AddAsync(imageFile);

                CreatedImageFileResponse response = new()
                {
                    Id = imageFile.Id,
                    BlogFileId = blogFile.Id,
                    ImageFileBracket = imageFile.ImageFileBracket,
                    Name = blogFile.Name,
                    FilePath = blogFile.FilePath,
                    FileUrl = blogFile.FileUrl,
                    UserId = request.UserId,
                };

                return response;
            }
        }
    }
}
