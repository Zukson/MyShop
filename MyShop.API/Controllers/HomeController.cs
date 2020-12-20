using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("Siemanko")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
