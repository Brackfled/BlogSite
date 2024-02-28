using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RefreshTokens.Queries.GetList
{
    public class GetListRefreshTokenQuery: IRequest<GetListResponse<GetListRefreshTokenListItemResponse>>
    {


        public class GetListRefreshTokenQueryHandler : IRequestHandler<GetListRefreshTokenQuery, GetListResponse<GetListRefreshTokenListItemResponse>>
        {

            private readonly IRefreshTokenRepository _refreshTokenRepository;
            private IMapper _mapper;

            public GetListRefreshTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
            {
                _refreshTokenRepository = refreshTokenRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListRefreshTokenListItemResponse>> Handle(GetListRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                Paginate<RefreshToken> refreshTokens = await _refreshTokenRepository.GetListAsync(
                        include:u => u.Include(u => u.User),
                        index:0,
                        size:1000,
                        withDeleted:false,
                        cancellationToken:cancellationToken
                    );

                GetListResponse<GetListRefreshTokenListItemResponse> response = _mapper.Map<GetListResponse<GetListRefreshTokenListItemResponse>>(refreshTokens);

                return response;
            }
        }

    }
}
