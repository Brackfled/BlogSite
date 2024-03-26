using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Create
{
    public class CreatedSubjectResponse
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public Guid SubjectImageFileId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }

        public CreatedSubjectResponse()
        {
            Title = string.Empty;
            Text = string.Empty;
            Summary = string.Empty;
        }

        public CreatedSubjectResponse(Guid ıd, string title, string text, string summary)
        {
            Id = ıd;
            Title = title;
            Text = text;
            Summary = summary;
        }
    }
}
