using Core.Persistance.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ImageFile: Entity<Guid>
    {
        public Guid BlogFileId { get; set; }
        public ImageFileBracket ImageFileBracket { get; set; }


        public virtual BlogFile? BlogFile { get; set; }


    }
}
