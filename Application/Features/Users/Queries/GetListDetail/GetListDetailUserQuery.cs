using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UserOperationClaimService;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Response;
using Core.CrossCuttingConserns.Exceptions.Types;
using Core.Persistance.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetListDetail
{
    public class GetListDetailUserQuery : IRequest<GetListResponse<GetListDetailUserListItemDto>>, ISecuredRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };


        public class GetListDetailUserQueryHandler : IRequestHandler<GetListDetailUserQuery, GetListResponse<GetListDetailUserListItemDto>>
        {
            private readonly IUserService _userService;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private IMapper _mapper;

            public GetListDetailUserQueryHandler(IUserService userService, IUserOperationClaimRepository userOperationClaimRepository, UserBusinessRules userBusinessRules, IMapper mapper)
            {
                _userService = userService;
                _userOperationClaimRepository = userOperationClaimRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListDetailUserListItemDto>> Handle(GetListDetailUserQuery request, CancellationToken cancellationToken)
            {

                IPaginate<User> users = await _userService.GetListAsync(
                        index: 0,
                        size: 1000,
                        withDeleted:false,
                        cancellationToken:cancellationToken
                    );

                GetListResponse<GetListDetailUserListItemDto> response = new GetListResponse<GetListDetailUserListItemDto> { };

                foreach( User user in users.Items ) 
                {

                    object roles = await _userOperationClaimRepository.GetUserOperationClaimsIdsByUserId(user.Id);

                    GetListDetailUserListItemDto dto = new()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        RolesAndClaims = roles
                    };

                    response.Items.Add( dto );
                }
                
                return response;


            }
        }
    }
}
