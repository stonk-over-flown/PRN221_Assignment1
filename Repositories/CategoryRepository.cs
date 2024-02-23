using BO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public bool AddCategory(Category category) => CategoryDAO.Instance.Add(category);

        public bool DeleteCategory(int id) => CategoryDAO.Instance.Delete(GetCategoryById(id));

        public List<Category> GetAllCategories() => CategoryDAO.Instance.GetAll();

        public Category GetCategoryById(int id) => CategoryDAO.Instance.GetAll().FirstOrDefault(x => x.CategoryId == id);

        public bool UpdateCategory(Category category) => CategoryDAO.Instance.Update(category, GetCategoryById(category.CategoryId));
    }
}
