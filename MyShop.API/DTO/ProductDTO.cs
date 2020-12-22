using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.DTO
{
    public class ProductDTO
    {
     [Key]
        
        public Guid ProductId { get; set; }

        public string Name { get; set; }

     

        public int QuantityInStock { get; set; }

        public string Description { get; set; }
    }
}
