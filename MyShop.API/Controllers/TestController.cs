using Microsoft.AspNetCore.Mvc;
using MyShop.API.Contracts.V1;
using MyShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Controllers
{
    public class TestController : Controller
    {
       ITestEntity test;
        public TestController(ITestEntity test)
        {
            this.test = test;
        }
        [HttpGet(ApiRoutes.Products.GetAllProducts)]
        public IActionResult Get()
        {
            test.Test();
            
            return View();
        }
    }
}
