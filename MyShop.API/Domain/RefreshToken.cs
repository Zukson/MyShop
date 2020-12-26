using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Domain
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryTime { get; set; }

        public string  Used {get;set;}

        public string Invalidated{ get; set; }


        
        public IdentityUser User { get; set; }

    }
}
