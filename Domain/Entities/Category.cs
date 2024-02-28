using Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category: Entity<int>
    {
        public string Name { get; set; }

        public ICollection<Subject>? Subjects { get; set; }

        public Category()
        {
            Name = string.Empty;
        }

        public Category(int id, string name):base(id) 
        {
            Name = name;
        }
    }
}
