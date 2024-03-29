﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Create
{
    public class CreateSubjectDto
    {
        public int CategoryId { get; set; }
        public Guid? SubjectImageFileId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }

        public CreateSubjectDto()
        {
            Title = string.Empty;
            Text = string.Empty;
            Summary = string.Empty;
        }

        public CreateSubjectDto(int categoryId, Guid subjectImageFileId, string title, string text, string summary)
        {
            CategoryId = categoryId;
            SubjectImageFileId = subjectImageFileId;
            Title = title;
            Text = text;
            Summary = summary;
        }
    }
}
