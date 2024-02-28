using Application.Features.Categories.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConserns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Rules
{
    public class CategoryBusinessRules : BaseBusinessRules
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusinessRules(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CategoryNameNotShouldExistsWhenSelected(string name)
        {
            bool doesExists = await _categoryRepository.AnyAsync(c => c.Name == name);

            if (doesExists)
            {
                throw new BusinessException(CategoryMessages.CategoryExists);
            }

        }

        public async Task CategoryShouldBeInsertWhenSelected(int id)
        {
            bool doesExists = await _categoryRepository.AnyAsync(c => c.Id == id);

            if (!doesExists)
                throw new BusinessException(CategoryMessages.CategoryNotExists);
        }
    }
}
