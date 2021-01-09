using AutoMapper;
using MyShop.API.Contracts.V1.Requests;
using MyShop.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<TagRequest, Tag>();
            CreateMap<PostProductRequest, Product>().ForMember(product => product.ProductId, opt => opt.Ignore())
                .ForMember(product => product.Tags, opt => opt.
                MapFrom(productRequest => productRequest.Tags.Select(tag=>new Tag { Name = tag.Name })));
            CreateMap<UpdateProductRequest, Product>().ForMember(product => product.ProductId, opt => opt.Ignore())
                .ForMember(product => product.Tags, opt => opt.
                MapFrom(productRequest => productRequest.Tags.Select(tag => new Tag { Name = tag.Name })));
        }
    }
}
