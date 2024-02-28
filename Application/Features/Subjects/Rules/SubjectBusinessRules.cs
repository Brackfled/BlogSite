using Application.Features.Subjects.Commands.Update;
using Application.Features.Subjects.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConserns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Rules
{
    public class SubjectBusinessRules: BaseBusinessRules
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectBusinessRules(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task SubjectTitleShouldNotExistsWhenInsert(string title)
        {
            bool doesExists = await _subjectRepository.AnyAsync(subject => subject.Title == title);
            if (doesExists)
                throw new BusinessException(SubjectMessages.SubjectExists);
        }

        public async Task SubjectShouldBeExistsWhenSelected(Guid id)
        {
            bool doesExists = await _subjectRepository.AnyAsync(s => s.Id == id);

            if (doesExists == false)
                throw new BusinessException(SubjectMessages.SubjectDoesExists);
        }

        public Task UserIdShouldMatch(int subjectUserId, int requestUserId)
        {
            if (subjectUserId == requestUserId)
                return Task.CompletedTask;

            throw new BusinessException(SubjectMessages.UserIdDontMatch);
        }
  
    }
}
