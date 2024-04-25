using Application.Features.Subjects.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Queries.GetListFromAuth
{
    public class GetListFromAuthQuery: IRequest<GetListResponse<GetListFromAuthSubjectListItemDto>>, ICachableRequest
    {
        public int UserId { get; set; }

        public string CacheKey => $"GetListFromAuth({UserId})";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetListSubjects";

        public TimeSpan? SlidingExpiration { get; }

        public class GetListFromAuthQueryHandler: IRequestHandler<GetListFromAuthQuery, GetListResponse<GetListFromAuthSubjectListItemDto>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly SubjectBusinessRules _subjectBusinessRules;
            private IMapper _mapper;

            public GetListFromAuthQueryHandler(ISubjectRepository subjectRepository, SubjectBusinessRules subjectBusinessRules, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _subjectBusinessRules = subjectBusinessRules;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListFromAuthSubjectListItemDto>> Handle(GetListFromAuthQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Subject>? subjects = await _subjectRepository.GetListAsync(predicate: s => s.UserId == request.UserId,
                                                                     include: s => s.Include(s => s.User).Include(s => s.Category).Include(s => s.SubjectImageFile),
                                                                     index:0,
                                                                     size:1000,
                                                                     withDeleted:true,
                                                                     cancellationToken:cancellationToken
                                                                     );

                GetListResponse<GetListFromAuthSubjectListItemDto> response = _mapper.Map<GetListResponse<GetListFromAuthSubjectListItemDto>>(subjects);
                return response;
            }
        }
    }
}
