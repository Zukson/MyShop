﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
       public TimeSpan TokenLifeTime { get; set; }        
    }
}
