using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UserOperationClaimService;
using Application.Services.UserService;
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
    public class GetListDetailUserQuery : IRequest<GetListDetailUserListItemDto>
    {
        public int Id { get; set; }

        public class GetListDetailUserQueryHandler : IRequestHandler<GetListDetailUserQuery, GetListDetailUserListItemDto>
        {
            private readonly IUserService _userService;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserBusinessRules _userBusinessRules;

            public GetListDetailUserQueryHandler(IUserService userService, IUserOperationClaimRepository userOperationClaimRepository, UserBusinessRules userBusinessRules)
            {
                _userService = userService;
                _userOperationClaimRepository = userOperationClaimRepository;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<GetListDetailUserListItemDto> Handle(GetListDetailUserQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

                User? user = await _userService.GetAsync(
                                                            predicate: u => u.Id == request.Id,
                                                            include:u => u.Include(u => u.UserOperationClaims),
                                                            withDeleted: false,
                                                            cancellationToken:cancellationToken
                                                            );


                Paginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                                                                                                                predicate: uoc => uoc.UserId == user.Id,
                                                                                                                include:uoc => uoc.Include(uoc => uoc.OperationClaim),
                                                                                                                index: 0,
                                                                                                                size:1000,
                                                                                                                withDeleted: false,
                                                                                                                cancellationToken:cancellationToken
                                                                                                                );

                List<string> roles = new();

                
                foreach (var ouc in userOperationClaims.Items)
                {
                    roles.Add(ouc.OperationClaim.Name);
                }

                GetListDetailUserListItemDto dto = new()
                {
                    Id = request.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Status = user.Status,
                    Roles = roles
                };

                return dto;

            }
        }
    }
}
