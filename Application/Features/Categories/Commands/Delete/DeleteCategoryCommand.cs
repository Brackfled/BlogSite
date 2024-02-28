using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand: IRequest<DeletedCategoryResponse>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, DeletedCategoryResponse> 
        {
            private readonly ICategoryRepository _categoryRepository;
            private IMapper _mapper;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<DeletedCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                Category? category = await _categoryRepository.GetAsync(predicate: c => c.Id == request.Id);

                _mapper.Map(request, category);
                Category deletedCategory = await _categoryRepository.DeleteAsync(category);

                DeletedCategoryResponse response = _mapper.Map<DeletedCategoryResponse>(deletedCategory);
                return response;
            }
        }
    }
}
