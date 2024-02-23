using BO;
using DAO;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository = null;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }
        public bool AddProduct(Product Product) => _productRepository.AddProduct(Product);

        public bool DeleteProduct(int id) => _productRepository.DeleteProduct(id);

        public List<Product> GetAllProducts() => _productRepository.GetAllAccounts();

        public Product GetProductById(int id) => _productRepository.GetProductById(id);

        public bool UpdateProduct(Product product) => _productRepository.UpdateProduct(product);
    }
}
