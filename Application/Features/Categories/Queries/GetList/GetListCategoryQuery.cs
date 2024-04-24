using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetListCategoryQuery: IRequest<GetListResponse<GetListCategoryListItemDto>>, ICachableRequest
    {

        public PageRequest PageRequest { get; set; }

        public string CacheKey => $"GetListByCategory({PageRequest.PageIndex},{PageRequest.PageSize})";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetCategories";

        public TimeSpan? SlidingExpiration { get;  }

        public class GetListCategoryQueryHandler: IRequestHandler<GetListCategoryQuery, GetListResponse<GetListCategoryListItemDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private IMapper _mapper;

            public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListCategoryListItemDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category>? categories = await _categoryRepository.GetListAsync(index: request.PageRequest.PageIndex,
                                                                                         size: request.PageRequest.PageSize,
                                                                                         withDeleted: true,
                                                                                         cancellationToken: cancellationToken);

                GetListResponse<GetListCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListCategoryListItemDto>>(categories);
                return response;

            }
        }
    }
}
