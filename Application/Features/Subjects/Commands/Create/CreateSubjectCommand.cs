﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Create
{
    public class CreateSubjectCommand: IRequest<CreatedSubjectResponse>, ICacheRemoverRequest, ISecuredRequest, ILoggableRequest
    {
        public int UserId { get; set; }
        public CreateSubjectDto CreateSubjectDto { get; set; }

        public string? CacheKey => "";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetSubjects";

        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin, Core.Security.Constants.GeneralOperationClaims.Author};

        public class CreateSubjectCommandHandler: IRequestHandler<CreateSubjectCommand, CreatedSubjectResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private IMapper _mapper;

            public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<CreatedSubjectResponse> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
            {
                Subject subject = new() {Id = Guid.NewGuid(), CategoryId = request.CreateSubjectDto.CategoryId,
                                                              SubjectImageFileId = request.CreateSubjectDto.SubjectImageFileId,
                                                              Title = request.CreateSubjectDto.Title,
                                                              Text = request.CreateSubjectDto.Text,
                                                              Summary = request.CreateSubjectDto.Summary,
                                                              UserId = request.UserId
                                                                 };
                

                await _subjectRepository.AddAsync(subject);

                CreatedSubjectResponse response = _mapper.Map<CreatedSubjectResponse>(subject);
                return response;

            }
        }
    }
}
