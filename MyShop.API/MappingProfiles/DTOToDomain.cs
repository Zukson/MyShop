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
    public class DTOToDomain : Profile
    {
       
        public DTOToDomain()
        {
          
           

            CreateMap<TagDTO, Tag>();

            CreateMap<ProductDTO, Product>();
                
        }
    }
}
