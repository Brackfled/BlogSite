using Amazon.Runtime.Internal;
using Application.Features.Files.Constants;
using Application.Features.Files.Rules;
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

namespace Application.Features.Files.Commands.DeletePPFile
{
    public class DeletePPFileCommand: IRequest<DeletedPPFileResponse>, ISecuredRequest, ICacheRemoverRequest
    {
        public Guid Id { get; set; }

        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };

        public string? CacheKey => "";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetListPPFiles";

        public class DeletePPFileCommandHandler: IRequestHandler<DeletePPFileCommand, DeletedPPFileResponse>
        {
            private readonly IPPFileRepository _ppFileRepository;
            private readonly IStroage _stroage;
            private readonly FileBusinessRules _fileBusinessRules;

            public DeletePPFileCommandHandler(IPPFileRepository ppFileRepository, IStroage stroage, FileBusinessRules fileBusinessRules)
            {
                _ppFileRepository = ppFileRepository;
                _stroage = stroage;
                _fileBusinessRules = fileBusinessRules;
            }

            public async Task<DeletedPPFileResponse> Handle(DeletePPFileCommand request, CancellationToken cancellationToken)
            {
                PPFile? ppFile = await _ppFileRepository.GetAsync(predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (ppFile == null)
                    throw new BusinessException(FileMessages.PPIsNotExists);

                await _stroage.DeleteFileAsync(bucketName:"flepix-blog-files", fileName:ppFile.Name);
                await _ppFileRepository.DeleteAsync(ppFile, permanent:true);

                DeletedPPFileResponse response = new() { Id = ppFile.Id };
                return response;
            }
        }
    }
}
