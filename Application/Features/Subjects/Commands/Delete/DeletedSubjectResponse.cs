using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Delete
{
    public class DeletedSubjectResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }

        public DeletedSubjectResponse()
        {
            Title = string.Empty;
            Text = string.Empty;
            Summary = string.Empty;
        }

        public DeletedSubjectResponse(Guid ıd, string title, string text, string summary)
        {
            Id = ıd;
            Title = title;
            Text = text;
            Summary = summary;
        }
    }
}
