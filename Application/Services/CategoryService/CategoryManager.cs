using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using Core.Persistance.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryService
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryBusinessRules _categoryBusinessRules;

        public CategoryManager(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
        {
            _categoryRepository = categoryRepository;
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _categoryBusinessRules.CategoryNameNotShouldExistsWhenSelected(category.Name);

            Category addedCategory = await _categoryRepository.AddAsync(category);
            return addedCategory;
        }

        public async Task<Category> DeleteAsync(Category category, bool permanent = false)
        {
            await _categoryBusinessRules.CategoryShouldBeInsertWhenSelected(category.Id);

            Category deletedCategory = await _categoryRepository.DeleteAsync(category,permanent);
            return deletedCategory;
        }

        public async Task<Category?> GetAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            Category? category = await _categoryRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
            return category;
        }

        public async Task<IPaginate<Category>?> GetListAsync(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IPaginate<Category>? categories = await _categoryRepository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
            return categories;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            await _categoryBusinessRules.CategoryShouldBeInsertWhenSelected(category.Id);

            Category updatedCategory = await _categoryRepository.UpdateAsync(category);
            return updatedCategory;
        }
    }
}
