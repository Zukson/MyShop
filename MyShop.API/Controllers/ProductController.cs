using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyShop.API.Contracts.V1;
using MyShop.API.Contracts.V1.Requests;
using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Domain;
using MyShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MyShop.API.Contracts.V1.ApiRoutes;

namespace MyShop.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ProductController(IProductService productService,IMapper mapper,IUriService uriService)
        {
            _productService = productService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Products.GetAllProducts)]
        public async Task<IActionResult> Get()
        {
            return  Ok(await _productService.GetAllProductsAsync());
        }
        [HttpGet(ApiRoutes.Products.GetProductById)]
        public IActionResult Get([FromRoute] Guid productId)
        {
         var output=   _productService.GetProductByIdAsync(productId);
            if(output is null)
            {
                return BadRequest("Product not found");
            }
            else
            {
                return Ok(output);
            }
        }

        [HttpPost(ApiRoutes.Products.PostProduct)]
        public async Task<IActionResult> Post([FromBody]PostProductRequest productRequest)
        {
            var product = _mapper.Map<Product>(productRequest);
            product.ProductId = Guid.NewGuid();

            var success =  await _productService.CreateProductAsync(product);
            if(!success)
            {
                BadRequest();

            }

            
                return Created(_uriService.GetPostUri(product.ProductId.ToString()), _mapper.Map<ProductResponse>(product));
            
        }

    }
}
