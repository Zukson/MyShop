﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Contracts.V1.Requests
{
 public    class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
