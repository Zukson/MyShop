using AutoMapper;
using MyShop.API.Domain;
using MyShop.API.DTO;
using MyShop.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.MappingProfiles
{
    public class DomainToDTO : Profile
    {
       

        public DomainToDTO()
        {
            
            CreateMap<Tag, TagDTO>();
            CreateMap<Product, ProductDTO>();

        }
    }
}
