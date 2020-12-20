using MyShop.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
  public   interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid productId);
        
    }
}
