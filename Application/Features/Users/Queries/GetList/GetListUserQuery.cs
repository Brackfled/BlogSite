﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetList
{
    public class GetListUserQuery: IRequest<GetListResponse<GetListUserListItemDto>>, ISecuredRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };


        public PageRequest PageRequest { get; set; }

        public GetListUserQuery()
        {
            PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
        }

        public GetListUserQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public class GetListUserQueryHandler: IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await _userRepository.GetListAsync(
                        index:request.PageRequest.PageIndex,
                        size: request.PageRequest.PageSize,
                        cancellationToken:cancellationToken
                    );

                GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
                return response;
            }
        }
    }
}
