using Application.Features.Auth.Constants;
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

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules: BaseBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserEmailShouldBeNotExists(string email)
        {
            bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email, enableTracking: false);
            if (doesExists)
                throw new BusinessException(AuthMessages.UserMailAlreadyExists);
        }

        public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
        {
            if (refreshToken == null)
                throw new BusinessException(AuthMessages.RefreshDontExists);
        }

        public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
        {
            if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
                throw new BusinessException(AuthMessages.InvalidRefreshToken);
            return Task.CompletedTask;
        }

        public Task UserShouldBeExistsWhenSelected(User? user)
        {
            if (user == null)
                throw new BusinessException(AuthMessages.UserDontExists);
            return Task.CompletedTask;
        }

        public async Task UserPasswordShouldBeMatch(int id, string password)
        {
            User? user = await _userRepository.GetAsync(predicate: u=> u.Id == id , enableTracking:false);
            await UserShouldBeExistsWhenSelected(user);
            if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
                throw new BusinessException(AuthMessages.PasswordDontMatched);
        }
    }
}
