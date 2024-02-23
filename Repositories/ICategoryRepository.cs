using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategories();
        public Category GetCategoryById(int id);
        public bool AddCategory(Category category);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(int id);
    }
}
