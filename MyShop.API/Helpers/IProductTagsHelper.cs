using MyShop.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Helpers
{
    public interface IProductTagsHelper
    {
        public Task<List<TagDTO>> AddTagsToProduct(Guid productId);
    }
}
