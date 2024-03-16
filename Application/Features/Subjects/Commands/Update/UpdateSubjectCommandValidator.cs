using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Update
{
    public class UpdateSubjectCommandValidator: AbstractValidator<UpdateSubjectCommad>
    {
        public UpdateSubjectCommandValidator()
        {
            RuleFor(s => s.UpdateSubjectDto.Id).NotEmpty();
            RuleFor(s => s.UpdateSubjectDto.Title).MinimumLength(3).MaximumLength(30);
            RuleFor(s => s.UpdateSubjectDto.Text).MinimumLength(15).MaximumLength(1000);
            RuleFor(s => s.UpdateSubjectDto.Summary).MinimumLength(3).MaximumLength(100);
        }
    }
}
