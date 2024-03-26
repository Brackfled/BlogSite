using Amazon.Runtime.Internal;
using Application.Features.Files.Rules;
using Application.Features.Subjects.Commands.Create;
using Application.Features.Subjects.Commands.Update;
using Application.Features.Subjects.Constants;
using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using Application.Services.SubjectService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConserns.Exceptions.Types;
using Domain.Entities;
using Infrastructure.Stroage;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.CreateWithSubjectImageFile
{
    public class CreateWithSubjectImageFileCommand: IRequest<CreatedWithSubjectImageFileResponse>, ICacheRemoverRequest
    {
        public int UserId { get; set; }
        public CreateWithSubjectImageFileDto CreateWithSubjectImageFileDto { get; set; }
        public IFormFile FormFile { get; set; }

        public string? CacheKey => "";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetSubjects";

        public class CreateWithSubjectImageFileCommandHandler: IRequestHandler<CreateWithSubjectImageFileCommand, CreatedWithSubjectImageFileResponse>
        {
            private readonly ISubjectService _subjectService;
            private readonly ISubjectImageFileRepository _subjectImageFileRepository;
            private readonly IStroage _stroage;
            private readonly SubjectBusinessRules _subjectBusinessRules;
            private readonly FileBusinessRules _fileBusinessRules;
            private IMapper _mapper;

            public CreateWithSubjectImageFileCommandHandler(ISubjectService subjectService, ISubjectImageFileRepository subjectImageFileRepository, IStroage stroage, SubjectBusinessRules subjectBusinessRules, FileBusinessRules fileBusinessRules, IMapper mapper)
            {
                _subjectService = subjectService;
                _subjectImageFileRepository = subjectImageFileRepository;
                _stroage = stroage;
                _subjectBusinessRules = subjectBusinessRules;
                _fileBusinessRules = fileBusinessRules;
                _mapper = mapper;
            }

            public async Task<CreatedWithSubjectImageFileResponse> Handle(CreateWithSubjectImageFileCommand request, CancellationToken cancellationToken)
            {
                Subject subject = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    CategoryId = request.CreateWithSubjectImageFileDto.CategoryId,
                    SubjectImageFileId = null,
                    Title = request.CreateWithSubjectImageFileDto.Title,
                    Text = request.CreateWithSubjectImageFileDto.Text,
                    Summary = request.CreateWithSubjectImageFileDto.Summary,
                };

                Subject addedSubject = await _subjectService.AddAsync(subject);

                if (addedSubject == null)
                    throw new BusinessException(SubjectMessages.SubjectDoesExists);

                await _fileBusinessRules.FileIsImageFile(request.FormFile.FileName.Substring(request.FormFile.FileName.LastIndexOf('.')));

                (string fileName, string bucketName, string fileUrl) addedFile = await _stroage.UploadFileAsync(request.FormFile, request.CreateWithSubjectImageFileDto.BucketName);

                SubjectImageFile subjectImageFile = new()
                {
                    Id= Guid.NewGuid(),
                    Name = addedFile.fileName,
                    Path = addedFile.bucketName,
                    Url = addedFile.fileUrl,
                    SubjectId = addedSubject.Id
                };

                SubjectImageFile addedSubjectImageFile = await _subjectImageFileRepository.AddAsync(subjectImageFile);

                UpdateSubjectDto updateSubjectDto = new() { Id = addedSubject.Id, SubjectImageFileId =addedSubjectImageFile.Id };

                Subject newSubject = await CheckPropertiesHasAnyOneChanged(updateSubjectDto, addedSubject);
                
                Subject updatedSubject = await _subjectService.UpdateAsync(newSubject);

                CreatedWithSubjectImageFileResponse response = _mapper.Map<CreatedWithSubjectImageFileResponse>(updatedSubject);
                return response;

            }

            private async Task<Subject> CheckPropertiesHasAnyOneChanged(UpdateSubjectDto request, Subject subject)
            {
                if (subject == null)
                    throw new BusinessException(SubjectMessages.SubjectForUpdateIsNull);
                if (request.SubjectImageFileId != null)
                    subject.SubjectImageFileId = request.SubjectImageFileId;
                if (request.Title != null)
                    subject.Title = request.Title;
                if (request.Text != null)
                    subject.Text = request.Text;
                if (request.Summary != null)
                    subject.Summary = request.Summary;

                return subject;
            }
        }
    }
}
