using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Commands.CreateSubjectImageFile
{
    public class CreatedSubjectImageFileResponse
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
