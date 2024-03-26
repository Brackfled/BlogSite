using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SubjectImageFile: BlogFile
    {
        public Guid? SubjectId { get; set; }

        public ICollection<Subject>? Subjects { get; set; }

    }
}
