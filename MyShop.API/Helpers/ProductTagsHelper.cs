using Microsoft.EntityFrameworkCore;
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
        public  async Task<List<TagDTO>> AddTagsToProduct(Guid productId)
        {
            var output = await _dataContext.PTBridges.Where(x => x.ProductId == productId).Select(x => new TagDTO { Name = x.TagName }).ToListAsync();
            return output;

        }
    }
}
