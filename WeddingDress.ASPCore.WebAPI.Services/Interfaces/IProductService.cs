using System.Collections.Generic;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}
