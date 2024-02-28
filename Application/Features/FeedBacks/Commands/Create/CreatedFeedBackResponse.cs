using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedBacks.Commands.Create
{
    public class CreatedFeedBackResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        public CreatedFeedBackResponse()
        {
            Name = string.Empty;
            Email = string.Empty;
            Text = string.Empty;
        }

        public CreatedFeedBackResponse(Guid ıd, string name, string email, string text)
        {
            Id = ıd;
            Name = name;
            Email = email;
            Text = text;
        }
    }
}
