using Application.Features.Subjects.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Persistance.Paging;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SubjectService
{
    public class SubjectManager : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly SubjectBusinessRules _subjectBusinessRules;

        public SubjectManager(ISubjectRepository subjectRepository, SubjectBusinessRules subjectBusinessRules)
        {
            _subjectRepository = subjectRepository;
            _subjectBusinessRules = subjectBusinessRules;
        }

        public async Task<Subject> AddAsync(Subject subject)
        {
            await _subjectBusinessRules.SubjectTitleShouldNotExistsWhenInsert(subject.Title);

            Subject addedSubject = await _subjectRepository.AddAsync(subject);
            return addedSubject;
        }

        public async Task<Subject> DeleteAsync(Subject subject, bool permanent = false)
        {
            await _subjectBusinessRules.SubjectShouldBeExistsWhenSelected(subject.Id);

            Subject deletedSubject = await _subjectRepository.DeleteAsync(subject, permanent);
            return deletedSubject;
        }

        public async Task<Subject?> GetAsync(Expression<Func<Subject, bool>> predicate, Func<IQueryable<Subject>, IIncludableQueryable<Subject, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            Subject? subject = await _subjectRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
            return subject;
        }

        public async Task<IPaginate<Subject>?> GetListAsync(Expression<Func<Subject, bool>>? predicate = null, Func<IQueryable<Subject>, IOrderedQueryable<Subject>>? orderBy = null, Func<IQueryable<Subject>, IIncludableQueryable<Subject, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IPaginate<Subject>? subjects = await _subjectRepository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
            return subjects;
        }

        public async Task<Subject> UpdateAsync(Subject subject)
        {
            await _subjectBusinessRules.SubjectShouldBeExistsWhenSelected(subject.Id);

            Subject updatedSubject = await _subjectRepository.UpdateAsync(subject);
            return updatedSubject;
        }
    }
}
