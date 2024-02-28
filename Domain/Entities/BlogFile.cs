using Core.Persistance.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlogFile: Entity<Guid>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }


        public virtual User? User { get; set; }

        public BlogFile()
        {
            Name = string.Empty;
            FilePath = string.Empty;
            FileUrl = string.Empty;

        }

        public BlogFile(Guid id, int userId, string name, string filePath, string fileUrl, User? user):base(id) 
        {
            UserId = userId;
            Name = name;
            FilePath = filePath;
            FileUrl = fileUrl;
            User = user;
        }
    }
}
