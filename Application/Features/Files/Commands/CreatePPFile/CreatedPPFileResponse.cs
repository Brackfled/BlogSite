using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Commands.CreatePPFile
{
    public class CreatedPPFileResponse
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }

        public CreatedPPFileResponse()
        {
            Name = string.Empty;
            Path = string.Empty;
            Url = string.Empty;
        }

        public CreatedPPFileResponse(Guid ıd, int userId, string name, string path, string url, DateTime createdDate)
        {
            Id = ıd;
            UserId = userId;
            Name = name;
            Path = path;
            Url = url;
            CreatedDate = createdDate;
        }
    }
}
