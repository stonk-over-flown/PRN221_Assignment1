using BO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        public List<Product> GetAllAccounts();
        public Product GetProductById(int id);
        public bool AddProduct(Product product);
        public bool UpdateProduct(Product product);

        public bool DeleteProduct(int id);
    }
}
