﻿using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConserns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules : BaseBusinessRules
    {

        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task UserShouldBeExistsWhenSelected(User? user)
        {
            if (user == null)
                throw new AuthorizationException(AuthMessages.UserDontExists);
            return Task.CompletedTask;
        }

        public async Task UserIdShouldExistWhenSelected(int id)
        {
            User? result = await _userRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
            if (result == null)
                throw new AuthorizationException(AuthMessages.UserDontExists);
        }

        public async Task UserEmailShouldNotExistsWhenInsert(string email)
        {
            bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email, enableTracking: false);
            if (doesExists)
                throw new BusinessException(AuthMessages.UserMailAlreadyExists);
        }

        public async Task UserEmailShouldNotExistsWhenUpdate(int id, string email)
        {
            bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email, enableTracking: false);
            if (doesExists)
                throw new BusinessException(AuthMessages.UserMailAlreadyExists);
        }

        public Task UserPasswordShouldBeMatched(User user, string password)
        {
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException(AuthMessages.PasswordDontMatched);
            return Task.CompletedTask;
        }
    }
}
