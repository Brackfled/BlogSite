using Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlogFile: Entity<Guid>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }

        public BlogFile()
        {
            Name = string.Empty;
            Path = string.Empty;
            Url = string.Empty;
        }

        public BlogFile(Guid id, string name, string path, string url):base(id)
        {
            Name = name;
            Path = path;
            Url = url;
        }
    }
}
