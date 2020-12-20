using MyShop.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public List<Tag> Tags { get; set; }

        public int QuantityInStock { get; set; }

        public string Description { get; set; }
    }
}
