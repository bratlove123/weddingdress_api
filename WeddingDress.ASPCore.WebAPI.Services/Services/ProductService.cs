using System.Collections.Generic;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;

namespace WeddingDress.ASPCore.WebAPI.Services.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
