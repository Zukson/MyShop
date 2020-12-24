using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Domain
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
