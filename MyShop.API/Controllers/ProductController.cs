using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.API.Contracts.V1;
using MyShop.API.Contracts.V1.Requests;
using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Domain;
using MyShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static MyShop.API.Contracts.V1.ApiRoutes;

namespace MyShop.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            return  Ok(_mapper.Map<List<ProductResponse>>(await _productService.GetAllProductsAsync()));
        }
        [HttpGet(ApiRoutes.Products.GetProductById)]
        public async  Task<IActionResult> Get([FromRoute] Guid productId)
        {
          

                var output =_mapper.Map<ProductResponse>(await _productService.GetProductByIdAsync(productId));
                if (output is null)
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
            try
            {
                var success = await _productService.CreateProductAsync(product);
                if (!success)
                {
                    BadRequest("Something went wrong");

                }
                return Created(_uriService.GetPostUri(product.ProductId.ToString()), _mapper.Map<ProductResponse>(product));
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

            
               
            
        }

    }
}
