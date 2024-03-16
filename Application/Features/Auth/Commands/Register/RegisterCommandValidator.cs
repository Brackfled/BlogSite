using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.UserForRegisterDto.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.UserForRegisterDto.FirstName).NotEmpty().MinimumLength(3).MaximumLength(16);
            RuleFor(r => r.UserForRegisterDto.LastName).NotEmpty().MinimumLength(3).MaximumLength(16);
            RuleFor(r => r.UserForRegisterDto.Password).NotEmpty().MinimumLength(8).MaximumLength(16);
        }
    }
}
