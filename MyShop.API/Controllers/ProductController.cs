using Microsoft.AspNetCore.Mvc;
using MyShop.API.Contracts.V1;
using MyShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(ApiRoutes.Products.GetAllProducts)]
        public async Task<IActionResult> Get()
        {
            return  Ok(await _productService.GetAllProducts());
        }
        [HttpGet(ApiRoutes.Products.GetProductById)]
        public IActionResult Get([FromRoute] Guid productId)
        {
         var output=   _productService.GetProductById(productId);
            if(output is null)
            {
                return BadRequest("Product not found");
            }
            else
            {
                return Ok(output);
            }
        }

    }
}
