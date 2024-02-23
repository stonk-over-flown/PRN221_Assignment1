using BO;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = null;
        public CategoryService() 
        {
            _categoryRepository = new CategoryRepository();
        }
        public bool AddCategory(Category category) => _categoryRepository.AddCategory(category);

        public bool DeleteCategory(int id) => _categoryRepository.DeleteCategory(id);

        public List<Category> GetAllCategories() => _categoryRepository.GetAllCategories();

        public Category GetCategoryById(int id) => _categoryRepository.GetCategoryById(id);

        public bool UpdateCategory(Category category) => _categoryRepository.UpdateCategory(category);
    }
}
