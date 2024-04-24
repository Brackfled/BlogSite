using Amazon.Runtime.Internal;
using Application.Features.Files.Constants;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConserns.Exceptions.Types;
using Domain.Entities;
using Infrastructure.Stroage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Commands.DeleteSubjectImageFile
{
    public class DeleteSubjectImageFileCommand: IRequest<DeletedSubjectImageFileResponse>, ISecuredRequest, ICacheRemoverRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };

        public Guid Id { get; set; }

        public string? CacheKey => "";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetListSubjectImageFiles";

        public class DeleteSubjectImageFileCommandHandler: IRequestHandler<DeleteSubjectImageFileCommand, DeletedSubjectImageFileResponse>
        {
            private readonly ISubjectImageFileRepository _subjectImageFileRepository;
            private readonly IStroage _stroage;

            public DeleteSubjectImageFileCommandHandler(ISubjectImageFileRepository subjectImageFileRepository, IStroage stroage)
            {
                _subjectImageFileRepository = subjectImageFileRepository;
                _stroage = stroage;
            }

            public async Task<DeletedSubjectImageFileResponse> Handle(DeleteSubjectImageFileCommand request, CancellationToken cancellationToken)
            {
                SubjectImageFile? subjectImageFile = await _subjectImageFileRepository.GetAsync(predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (subjectImageFile == null)
                    throw new BusinessException(FileMessages.SubjectImageFileIsNotExists);

                await _stroage.DeleteFileAsync(bucketName: "flepix-blog-subjectfiles", fileName:subjectImageFile.Name);
                await _subjectImageFileRepository.DeleteAsync(subjectImageFile, permanent:true);

                DeletedSubjectImageFileResponse response = new() { Id = subjectImageFile.Id };
                return response;
                
            }
        }
    }
}
