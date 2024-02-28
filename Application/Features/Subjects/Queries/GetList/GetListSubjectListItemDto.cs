using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Queries.GetList
{
    public class GetListSubjectListItemDto
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public GetListSubjectListItemDto()
        {
            Title = string.Empty;
            Text = string.Empty;
            Summary = string.Empty;
        }

        public GetListSubjectListItemDto(Guid ıd, string title, string text, string summary, DateTime createdDate, DateTime? updateDate, DateTime? deletedDate)
        {
            Id = ıd;
            Title = title;
            Text = text;
            Summary = summary;
            CreatedDate = createdDate;
            UpdateDate = updateDate;
            DeletedDate = deletedDate;
        }
    }
}
