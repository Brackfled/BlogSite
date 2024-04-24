using Amazon.Runtime.Internal;
using Application.Features.Files.Rules;
using Application.Features.Users.Rules;
using Application.Services.PPFileService;
using Application.Services.Repositories;
using Domain.Entities;
using Infrastructure.Stroage;
using Insfrastructure.Stroage.AWS;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Commands.CreatePPFile
{
    public class CreatePPFileCommand: IRequest<CreatedPPFileResponse>
    {
        public int UserId { get; set; }
        public string BucketName { get; set; }
        public IFormFile FormFile { get; set; }

        public class CreatePPFileCommandHandler: IRequestHandler<CreatePPFileCommand, CreatedPPFileResponse>
        {
            private readonly IPPFileRepository _ppFileRepository;
            private readonly IStroage _stroage;
            private readonly FileBusinessRules _fileBusinessRules;
            private readonly UserBusinessRules _userBusinessRules;

            public CreatePPFileCommandHandler(IPPFileRepository ppFileRepository, IStroage stroage, FileBusinessRules fileBusinessRules, UserBusinessRules userBusinessRules)
            {
                _ppFileRepository = ppFileRepository;
                _stroage = stroage;
                _fileBusinessRules = fileBusinessRules;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<CreatedPPFileResponse> Handle(CreatePPFileCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdShouldExistWhenSelected(request.UserId);
                await _fileBusinessRules.FileIsImageFile(request.FormFile.FileName.Substring(request.FormFile.FileName.LastIndexOf('.')));
                await _fileBusinessRules.OneUserOnePPFile(request.UserId);

                (string fileName,string bucketName, string fileUrl) uploadedFile =  await _stroage.UploadFileAsync(request.FormFile, request.BucketName);

                PPFile ppFile = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Name = uploadedFile.fileName,
                    Path = uploadedFile.bucketName,
                    Url = uploadedFile.fileUrl,
                    
                };

                PPFile addedPPFile = await _ppFileRepository.AddAsync(ppFile);

                CreatedPPFileResponse response = new()
                {
                    Id = addedPPFile.Id,
                    UserId = (int)addedPPFile.UserId,
                    Name = addedPPFile.Name,
                    Path = addedPPFile.Path,
                    Url = addedPPFile.Url,
                    CreatedDate = addedPPFile.CreatedDate
                };

                return response;
            }
        }
    }
}
