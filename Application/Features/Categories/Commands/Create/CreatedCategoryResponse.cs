using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Create
{
    public class CreatedCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CreatedCategoryResponse()
        {
            Name = string.Empty;
        }

        public CreatedCategoryResponse(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }
    }
}
