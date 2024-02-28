using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeletedCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DeletedCategoryResponse()
        {
            Name = string.Empty;
        }

        public DeletedCategoryResponse(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }
    }
}
