using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.DTO
{
    public class TagDTO
    {
        [Key]
        public string Name { get; set; }


    }
}
