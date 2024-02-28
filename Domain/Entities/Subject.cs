using Core.Persistance.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subject : Entity<Guid>
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }

        public virtual User? User { get; set; }
        public virtual Category? Category { get; set; }

        public Subject()
        {
            Title = string.Empty;
            Text = string.Empty;
            Summary = string.Empty;
        }

        public Subject(Guid id, int userId, int categoryId, string title, string text, string summary):base(id)
        {
            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Text = text;
            Summary = summary;
        }
    }
}
