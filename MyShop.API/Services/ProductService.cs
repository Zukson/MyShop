using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Data;
using MyShop.API.Domain;
using MyShop.API.DTO;
using MyShop.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IProductTagsHelper _productTagsHelper;
        public ProductService(DataContext dataContext,IMapper mapper,IProductTagsHelper productTagsHelper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _productTagsHelper = productTagsHelper;
        }
        public  async  Task<List<Product>> GetAllProductsAsync()
        {

            var output = await _dataContext.Products.ToListAsync();
             Parallel.ForEach(output,  x => x.Tags =  _productTagsHelper.AddTagsToProduct(x.ProductId).Result);
          
            return _mapper.Map<List<Product>>(output);

        }

        public  async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var output = await _dataContext.Products.FindAsync(productId);
           if(output==null)
            {
                throw new Exception("Product Not Found");
            }

            else
            {
                return _mapper.Map<Product>(output);
            }
            
                
            
        }

        public async Task<bool>CreateProductAsync(Product product)
        {
             await _dataContext.Products.AddAsync(_mapper.Map<ProductDTO>(product));
          var created=  await _dataContext.SaveChangesAsync();
            return created > 0;

        }

        public Task<ProductResponse> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
