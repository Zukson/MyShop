using MyShop.API.Data;
using MyShop.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Helpers
{
    public class ProductTagsHelper : IProductTagsHelper
    {
        private readonly DataContext _dataContext;
        public ProductTagsHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Task<ProductDTO> AddTagsToProduct(ProductDTO product)
        {
            var output = _dataContext.PTBridges.
                Where(x => x.ProductId == product.ProductId).
                Select(x => new TagDTO { Name = x.TagName }).ToList();
            product.Tags = output;
            return Task.FromResult(product);

        }
    }
}
