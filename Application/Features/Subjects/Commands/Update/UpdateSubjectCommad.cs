using Application.Features.Subjects.Constants;
using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConserns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Update
{
    public class UpdateSubjectCommad: IRequest<UpdatedSubjectResponse>, ICacheRemoverRequest, ISecuredRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin, Core.Security.Constants.GeneralOperationClaims.Author };

        public int UserId { get; set; }

        public string? CacheKey => "";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetSubjects";

        public UpdateSubjectDto UpdateSubjectDto { get; set; }

        public class UpdateSubjectCommandHandler: IRequestHandler<UpdateSubjectCommad, UpdatedSubjectResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly SubjectBusinessRules _subjectBusinessRules;
            private IMapper _mapper;

            public UpdateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper, SubjectBusinessRules subjectBusinessRules)
            {
                _subjectRepository = subjectRepository;
                _subjectBusinessRules = subjectBusinessRules;
                _mapper = mapper;
            }

            public async Task<UpdatedSubjectResponse> Handle(UpdateSubjectCommad request, CancellationToken cancellationToken)
            {
                Subject? subject = await _subjectRepository.GetAsync(predicate: s => s.Id == request.UpdateSubjectDto.Id);
                await _subjectBusinessRules.UserIdShouldMatch(subject.UserId, request.UserId);

                Subject newSubject = await CheckPropertiesHasAnyOneChanged(request.UpdateSubjectDto, subject);

                var updatedSubject = await _subjectRepository.UpdateAsync(newSubject);
                UpdatedSubjectResponse response = _mapper.Map<UpdatedSubjectResponse>(updatedSubject);
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
