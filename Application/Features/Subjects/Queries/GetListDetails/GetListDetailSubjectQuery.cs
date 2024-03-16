using Amazon.Runtime.Internal;
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

namespace Application.Features.Subjects.Queries.GetListDetails
{
    public class GetListDetailSubjectQuery: IRequest<GetListResponse<GetListDetailSubjectListItemDto>>, ICachableRequest
    {
        public string CacheKey => "GetListDetailSubject";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetSubjects";

        public TimeSpan? SlidingExpiration { get; }

        public class GetListDetailSubjectQueryHandler: IRequestHandler<GetListDetailSubjectQuery, GetListResponse<GetListDetailSubjectListItemDto>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private IMapper _mapper;

            public GetListDetailSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListDetailSubjectListItemDto>> Handle(GetListDetailSubjectQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Subject>? subjects = await _subjectRepository.GetListAsync(include: s => s.Include(s => s.User).Include(s => s.Category),
                                                                                     index:0,
                                                                                     size:1000,
                                                                                     withDeleted:false,
                                                                                     cancellationToken:cancellationToken
                                                                                     );

                GetListResponse<GetListDetailSubjectListItemDto> response = _mapper.Map<GetListResponse<GetListDetailSubjectListItemDto>>(subjects);
                return response;
            }
        }
    }
}
