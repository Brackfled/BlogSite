using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Create
{
    public class CreateSubjectCommandValidator: AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidator()
        {
            RuleFor(s => s.CreateSubjectDto.Title).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(s => s.CreateSubjectDto.Text).NotEmpty().MinimumLength(15).MaximumLength(1000);
            RuleFor(s => s.CreateSubjectDto.Summary).NotEmpty().MinimumLength(3).MaximumLength(100);
        }
    }
}
