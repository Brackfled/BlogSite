using Amazon.Runtime.Internal;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetByIdDetail
{
    public class GetByIdDetailQuery: IRequest<GetByIdDetailResponse>
    {
        public int UserId { get; set; }

        public class GetByIdDetailQueryHandler: IRequestHandler<GetByIdDetailQuery, GetByIdDetailResponse>
        {
            private readonly IUserService _userService;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private IMapper _mapper;

            public GetByIdDetailQueryHandler(IUserService userService, IUserOperationClaimRepository userOperationClaimRepositoryionClaimRepository, UserBusinessRules userBusinessRules, IMapper mapper)
            {
                _userService = userService;
                _userOperationClaimRepository = userOperationClaimRepositoryionClaimRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
            }

            public async Task<GetByIdDetailResponse> Handle(GetByIdDetailQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userService.GetAsync(predicate:u => u.Id == request.UserId,
                                                         withDeleted:false);
                await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

                object roles = await _userOperationClaimRepository.GetUserOperationClaimsIdsByUserId(request.UserId);
                

                GetByIdDetailResponse response = _mapper.Map<GetByIdDetailResponse>(user);
                response.RolesAndClaims = roles;
                return response;
            }
        }
    }
}
