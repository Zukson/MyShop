using Microsoft.AspNetCore.Mvc;
using MyShop.API.Contracts.V1;
using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Domain;
using MyShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Controllers
{
    public class IdentityController  : Controller
    {
        IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest user)
        {
            var authResponse=  await _identityService.RegisterAsync(user.Email,user.Password);

            if (authResponse.IsSuccess is false)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                }
                     );
            }
            return Ok(new AuthSuccessResponse
            {

                Token = authResponse.Token
            });
        }
    }
}
