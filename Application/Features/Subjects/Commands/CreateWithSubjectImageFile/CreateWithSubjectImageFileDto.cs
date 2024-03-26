using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.CreateWithSubjectImageFile
{
    public class CreateWithSubjectImageFileDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }
        public string BucketName { get; set; }
    }
}
