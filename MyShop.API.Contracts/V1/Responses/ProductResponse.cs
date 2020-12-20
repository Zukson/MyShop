using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Contracts.V1.Responses
{
  public  class ProductResponse
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public List<TagResponse> Tags { get; set; }

        public int QuantityInStock { get; set; }

        public string Description { get; set; }


    }
}
