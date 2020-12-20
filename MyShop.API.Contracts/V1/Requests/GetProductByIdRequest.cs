using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Contracts.V1.Requests
{
   public  class GetProductByIdRequest
    {
        public Guid Id { get; set; }
    }
}
