using AutoMapper;
using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.MappingProfiles
{
    public class DomainToResponseProfile : Profile

    {
        public DomainToResponseProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(productResponse=>productResponse.
                Tags,opt=>opt.MapFrom(product=>product.Tags.
                Select(tag=>new TagResponse { Name = tag.Name })));

        }
    }
}
