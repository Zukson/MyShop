﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Contracts.V1.Requests
{ 

    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
