using Amazon.Runtime.Internal;
using Application.Features.Files.Rules;
using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using Infrastructure.Stroage;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Commands.CreateSubjectImageFile
{
    public class CreateSubjectImageFileCommand: IRequest<CreatedSubjectImageFileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin, Core.Security.Constants.GeneralOperationClaims.Author };

        public CreateSubjectImageFileDto CreateSubjectImageFileDto { get; set; }
        public IFormFile FormFile { get; set; }

        public string? CacheKey => "";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetListSubjectImageFiles";

        public class CreateSubjectImageFileCommandHandler: IRequestHandler<CreateSubjectImageFileCommand, CreatedSubjectImageFileResponse>
        {
            private readonly ISubjectImageFileRepository _subjectImageFileRepository;
            private readonly IStroage _stroage;
            private readonly FileBusinessRules _fileBusinessRules;
            private readonly SubjectBusinessRules _subjectBusinessRules;
            private IMapper _mapper;

            public CreateSubjectImageFileCommandHandler(ISubjectImageFileRepository subjectImageFileRepository, IStroage stroage, FileBusinessRules fileBusinessRules, SubjectBusinessRules subjectBusinessRules, IMapper mapper)
            {
                _subjectImageFileRepository = subjectImageFileRepository;
                _stroage = stroage;
                _fileBusinessRules = fileBusinessRules;
                _subjectBusinessRules = subjectBusinessRules;
                _mapper = mapper;
            }

            public async Task<CreatedSubjectImageFileResponse> Handle(CreateSubjectImageFileCommand request, CancellationToken cancellationToken)
            {
                await _subjectBusinessRules.SubjectShouldBeExistsWhenSelected(request.CreateSubjectImageFileDto.SubjectId);
                await _fileBusinessRules.FileIsImageFile(request.FormFile.FileName.Substring(request.FormFile.FileName.LastIndexOf('.')));
                await _fileBusinessRules.ImageSizeControl(request.FormFile, 600, 900);

                (string fileName, string bucketName, string fileUrl) addedFile = await _stroage.UploadFileAsync(request.FormFile, request.CreateSubjectImageFileDto.BucketName);

                SubjectImageFile subjectImageFile = new()
                {
                    Id = Guid.NewGuid(),
                    SubjectId = request.CreateSubjectImageFileDto.SubjectId,
                    Name = addedFile.fileName,
                    Path = addedFile.bucketName,
                    Url = addedFile.fileUrl,
                };

                SubjectImageFile addedSubjectImageFile = await _subjectImageFileRepository.AddAsync(subjectImageFile);

                CreatedSubjectImageFileResponse response = _mapper.Map<CreatedSubjectImageFileResponse>(addedSubjectImageFile);
                return response;
            }
        }
    }
}
