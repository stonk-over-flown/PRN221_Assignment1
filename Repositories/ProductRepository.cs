using BO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public bool AddProduct(Product product) => ProductDAO.Instance.Add(product);

        public bool DeleteProduct(int id) => ProductDAO.Instance.Delete(GetProductById(id));

        public List<Product> GetAllAccounts() => ProductDAO.Instance.GetAll();

        public Product GetProductById(int id) => ProductDAO.Instance.GetAll().FirstOrDefault(x => x.ProductId == id);

        public bool UpdateProduct(Product product) => ProductDAO.Instance.Update(product, GetProductById(product.ProductId));
    }
}
