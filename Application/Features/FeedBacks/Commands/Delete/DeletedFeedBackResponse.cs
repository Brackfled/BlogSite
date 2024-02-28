using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedBacks.Commands.Delete
{
    public class DeletedFeedBackResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public DeletedFeedBackResponse()
        {
            Name = string.Empty;
            Email = string.Empty;
            Text = string.Empty;
        }

        public DeletedFeedBackResponse(Guid ıd, string name, string email, string text, DateTime createdDate, DateTime? updatedDate, DateTime? deletedDate)
        {
            Id = ıd;
            Name = name;
            Email = email;
            Text = text;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            DeletedDate = deletedDate;
        }
    }
}
