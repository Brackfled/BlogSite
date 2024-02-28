using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ImageFiles.Commands.Create
{
    public class CreatedImageFileResponse
    {
        public Guid Id { get; set; }
        public Guid BlogFileId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
        public ImageFileBracket ImageFileBracket { get; set; }
        public DateTime CreatedDate { get; set; }

        public CreatedImageFileResponse()
        {
            Name = string.Empty;
            FilePath = string.Empty;
            FilePath = string.Empty;
            FileUrl = string.Empty;
        }

        public CreatedImageFileResponse(Guid id, Guid blogFileId, int userId, string name, string filePath, string fileUrl, ImageFileBracket ımageFileBracket, DateTime createdDate)
        {
            Id = id;
            BlogFileId = blogFileId;
            UserId = userId;
            Name = name;
            FilePath = filePath;
            FileUrl = fileUrl;
            ImageFileBracket = ımageFileBracket;
            CreatedDate = createdDate;
        }
    }
}
