using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Contracts.V1.Requests
{
  public   class UpdateProductRequest
    {
        public string Name { get; set; }

        public List<TagRequest> Tags { get; set; }

        public int QuantityInStock { get; set; }

        public string Description { get; set; }
    }
}
