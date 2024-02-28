using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommand: IRequest<DeletedUserResponse>
    {

        public int Id { get; set; }

        public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, DeletedUserResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(predicate: u => u.Id == request.Id, cancellationToken:cancellationToken);

                await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

                await _userRepository.DeleteAsync(user!);

                DeletedUserResponse response = _mapper.Map<DeletedUserResponse>(user);
                return response;
            }
        }

    }
}
