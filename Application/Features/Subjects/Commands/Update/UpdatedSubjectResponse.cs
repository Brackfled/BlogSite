using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Update
{
    public class UpdatedSubjectResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }

        public UpdatedSubjectResponse()
        {
            Title = string.Empty;
            Text = string.Empty;
            Summary = string.Empty;
        }

        public UpdatedSubjectResponse(Guid ıd, string title, string text, string summary)
        {
            Id = ıd;
            Title = title;
            Text = text;
            Summary = summary;
        }
    }
}
