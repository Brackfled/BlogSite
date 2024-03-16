using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedBacks.Commands.Create
{
    public class CreateFeedBackCommandValidator: AbstractValidator<CreateFeedBackCommand>
    {
        public CreateFeedBackCommandValidator()
        {
            RuleFor(fb => fb.Name).NotEmpty().MinimumLength(3).MaximumLength(16);
            RuleFor(fb => fb.Text).NotEmpty().MinimumLength(3).MaximumLength(200);
            RuleFor(fb => fb.Email).NotEmpty().EmailAddress();
        }
    }
}
