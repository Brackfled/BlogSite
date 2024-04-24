using Amazon.Runtime.Internal;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Request;
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

namespace Application.Features.Subjects.Queries.GetListByCategoryId
{
    public class GetListByCategoryIdSubjectQuery: IRequest<GetListResponse<GetListByCategoryIdSubjectListItemDto>>, ICachableRequest
    {
        public string CacheKey => $"GetListDetailSubject/{CategoryId}?{PageRequest.PageIndex},{PageRequest.PageSize}";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetSubjects";

        public TimeSpan? SlidingExpiration { get; }

        public int CategoryId { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListByCategoryIdSubjectQueryHandler: IRequestHandler<GetListByCategoryIdSubjectQuery, GetListResponse<GetListByCategoryIdSubjectListItemDto>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly CategoryBusinessRules _categoryBusinessRules;
            private IMapper _mapper;

            public GetListByCategoryIdSubjectQueryHandler(ISubjectRepository subjectRepository, CategoryBusinessRules categoryBusinessRules, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _categoryBusinessRules = categoryBusinessRules;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListByCategoryIdSubjectListItemDto>> Handle(GetListByCategoryIdSubjectQuery request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryShouldBeInsertWhenSelected(request.CategoryId);

                IPaginate<Subject>? subjects = await _subjectRepository.GetListAsync(
                        include: s => s.Include(s => s.User).Include(s => s.Category).Include(s => s.SubjectImageFile),
                        predicate: s => s.CategoryId == request.CategoryId,
                        index: request.PageRequest.PageIndex,
                        size: request.PageRequest.PageSize,
                        withDeleted: true,
                        cancellationToken: cancellationToken
                    );

                GetListResponse<GetListByCategoryIdSubjectListItemDto> response = _mapper.Map<GetListResponse<GetListByCategoryIdSubjectListItemDto>>(subjects);
                return response;
            }
        }
    }
}
