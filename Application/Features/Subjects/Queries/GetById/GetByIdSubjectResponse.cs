using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Queries.GetById
{
    public class GetByIdSubjectResponse
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}
        public DateTime? DeletedDate { get; set; }

        public GetByIdSubjectResponse()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            CategoryName = string.Empty;
            Title = string.Empty;
            Text = string.Empty;
            Summary = string.Empty;
        }

        public GetByIdSubjectResponse(Guid ıd, int userId, string firstName, string lastName, int categoryId, string categoryName, string title, string text, string summary, DateTime createdDate, DateTime? updatedDate, DateTime? deletedDate)
        {
            Id = ıd;
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Title = title;
            Text = text;
            Summary = summary;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            DeletedDate = deletedDate;
        }
    }
}
