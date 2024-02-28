using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ImageFiles.Queries.GetList
{
    public class GetListImageFileListItemDto
    {
        public Guid Id { get; set; }
        public Guid BlogFileId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
        public ImageFileBracket ImageFileBracket { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
