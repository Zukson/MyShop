using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyShop.API.Data;
using MyShop.API.Domain;
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
        public  async  Task<List<Product>> GetAllProducts()
        {

            var output = await _dataContext.Products.ToListAsync();
          output.ForEach(async  x => x.Tags = await _productTagsHelper.AddTagsToProduct(x.ProductId));
            return _mapper.Map<List<Product>>(output);

        }

        public  async Task<Product> GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
