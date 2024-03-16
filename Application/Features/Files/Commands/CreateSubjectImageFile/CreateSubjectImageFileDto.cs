using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Commands.CreateSubjectImageFile
{
    public class CreateSubjectImageFileDto
    {
        public Guid SubjectId { get; set; }
        public string BucketName { get; set; }
    }
}
