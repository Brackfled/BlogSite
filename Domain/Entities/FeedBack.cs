using Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FeedBack: Entity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public FeedBack()
        {
            Name = string.Empty;
            Email = string.Empty;
            Text = string.Empty;
        }

        public FeedBack(Guid id, string name, string email, string text):base(id) 
        {
            Name = name;
            Email = email;
            Text = text;
        }
    }
}
