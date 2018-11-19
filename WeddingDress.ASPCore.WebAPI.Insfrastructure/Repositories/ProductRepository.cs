using System.Collections.Generic;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Ef;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private WeddingDressDataContext _context;

        public ProductRepository(WeddingDressDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }
    }
}
