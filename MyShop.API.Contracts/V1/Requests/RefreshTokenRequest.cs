using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Contracts.V1.Requests
{
  public  class RefreshTokenRequest
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
