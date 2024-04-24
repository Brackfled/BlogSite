using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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

namespace Application.Features.FeedBacks.Queries.GetList
{
    public class GetListFeedBackQuery: IRequest<GetListResponse<GetListFeedBackListItemDto>>, ICachableRequest, ISecuredRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };
        public PageRequest PageRequest { get; set; }

        public string CacheKey => $"GetListFeedBackQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

        public bool ByPassCache { get;}

        public string? CacheGroupKey => "GetListFeedBacks";

        public TimeSpan? SlidingExpiration { get; }

        public GetListFeedBackQuery()
        {
            PageRequest = new PageRequest();
        }

        public GetListFeedBackQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public class GetListFeedBackQueryHandler: IRequestHandler<GetListFeedBackQuery, GetListResponse<GetListFeedBackListItemDto>>
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private IMapper _mapper;

            public GetListFeedBackQueryHandler(IFeedBackRepository feedBackRepository, IMapper mapper)
            {
                _feedBackRepository = feedBackRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListFeedBackListItemDto>> Handle(GetListFeedBackQuery request, CancellationToken cancellationToken)
            {
                IPaginate<FeedBack>? feedBacks = await _feedBackRepository.GetListAsync(
                                                                                        index:request.PageRequest.PageIndex,
                                                                                        size:request.PageRequest.PageSize,
                                                                                        withDeleted:true,
                                                                                        cancellationToken:cancellationToken
                                                                                        );

                GetListResponse<GetListFeedBackListItemDto> response = _mapper.Map<GetListResponse<GetListFeedBackListItemDto>>(feedBacks);
                return response;
            }
        }
    }
}
