using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Queries.GetById
{
    public class GetByIdSubjectQuery: IRequest<GetByIdSubjectResponse>
    {
        public Guid Id { get; set; }

        public class GetByIdSubjectQueryHandler : IRequestHandler<GetByIdSubjectQuery, GetByIdSubjectResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly SubjectBusinessRules _subjectBusinessRules;
            private IMapper _mapper;

            public GetByIdSubjectQueryHandler(ISubjectRepository subjectRepository, SubjectBusinessRules subjectBusinessRules, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _subjectBusinessRules = subjectBusinessRules;
                _mapper = mapper;
            }

            public async Task<GetByIdSubjectResponse> Handle(GetByIdSubjectQuery request, CancellationToken cancellationToken)
            {
                await _subjectBusinessRules.SubjectShouldBeExistsWhenSelected(request.Id);
                Subject? subject = await _subjectRepository.GetAsync(predicate: s=> s.Id == request.Id,
                                                                     include: s => s.Include(s => s.User).Include(s => s.Category).Include(s => s.SubjectImageFile),
                                                                     withDeleted:true,
                                                                     cancellationToken:cancellationToken
                                                                     );

                GetByIdSubjectResponse response = _mapper.Map<GetByIdSubjectResponse>(subject);
                return response;
            }
        }
    }
}
