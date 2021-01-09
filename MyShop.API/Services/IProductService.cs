using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
  public   interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(string productId, Product product);
      

        
    }
}
