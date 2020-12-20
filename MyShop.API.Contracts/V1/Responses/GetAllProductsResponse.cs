using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Contracts.V1.Responses
{
   public  class GetAllProductsResponse
    {
        public List<ProductResponse> Products { get; set; }


    }
}
