﻿using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Application.Dtos;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand: IRequest<RegisteredResponse>, ITransactionalRequest, ILoggableRequest
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public RegisterCommand()
        {
            UserForRegisterDto = null!;
            IpAddress = string.Empty;
        }

        public RegisterCommand(UserForRegisterDto userForRegisterDto, string ıpAddress)
        {
            UserForRegisterDto = userForRegisterDto;
            IpAddress = ıpAddress;
        }

        public class RegisterCommandHandler: IRequestHandler<RegisterCommand, RegisteredResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;

            public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

                HashingHelper.CreatePasswordHash(
                        request.UserForRegisterDto.Password,
                        passwordHash: out byte[] passwordHash,
                        passwordSalt: out byte[] passwordSalt
                    );

                User newUser = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
                User createdUser = await _userRepository.AddAsync(newUser);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

                Core.Security.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                Core.Security.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
                return registeredResponse;
            }
        }
    }
}
