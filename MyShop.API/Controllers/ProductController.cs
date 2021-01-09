using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.API.Caching;
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
     ///   <summary>
     ///Returns all products
     ///</summary>
     ///<response code="200">Returns all products</response>
        [Cache(500)]
        [HttpGet(ApiRoutes.Products.GetAllProducts)]
        public async Task<IActionResult> Get()
        {
            return  Ok(_mapper.Map<List<ProductResponse>>(await _productService.GetAllProductsAsync()));
        }
        [Cache(500)]    
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
        ///   <summary>
        ///Create product
        ///</summary>
        ///<response code="200">Create a product</response>
        /// ///<response code="400">Product already exist </response>
        [HttpPost(ApiRoutes.Products.PostProduct)]
        public async Task<IActionResult> Post([FromBody]PostProductRequest productRequest)
        {
         
            var product = _mapper.Map<Product>(productRequest);
            product.ProductId = Guid.NewGuid();
           
                var success = await _productService.CreateProductAsync(product);
                if (!success)
                {
                    BadRequest("Something went wrong");

                }
                return Created(_uriService.GetPostUri(product.ProductId.ToString()), _mapper.Map<ProductResponse>(product));
         

          
        }
        [HttpPut(ApiRoutes.Products.UpdateProduct)]
        public async Task<IActionResult> Put([FromRoute] string productId, [FromBody] UpdateProductRequest productRequest)
        {
            var product = _mapper.Map<Product>(productRequest);
            var response = await  _productService.UpdateProductAsync(productId,product );

            if(!response)
            {
                return BadRequest("Unable to update product");
            }
            return Ok(_mapper.Map<ProductResponse>(product));


        }

    }
    
}
