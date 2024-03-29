﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetListCategoryListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public GetListCategoryListItemDto()
        {
            Name = string.Empty;
        }

        public GetListCategoryListItemDto(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }
    }
}
