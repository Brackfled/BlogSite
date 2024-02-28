using Application.Features.Users.Rules;
using Application.Services.UserService;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQuery:IRequest<GetByIdUserResponse>
    {
        public int Id { get; set; }

        public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery ,GetByIdUserResponse> 
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public GetByIdUserQueryHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userService = userService;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

                User? user = await _userService.GetAsync(predicate: u => u.Id == request.Id);

                GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);

                return response;
            }
        }
    }
}
