﻿using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand: IRequest<CreatedCategoryResponse>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse> 
        {
            private readonly ICategoryRepository _categoryRepository;
            private IMapper _mapper;

            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<CreatedCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                Category category = _mapper.Map<Category>(request);

                Category addedCategory = await _categoryRepository.AddAsync(category);

                CreatedCategoryResponse response = _mapper.Map<CreatedCategoryResponse>(addedCategory);
                return response;
            }
        }
    }
}