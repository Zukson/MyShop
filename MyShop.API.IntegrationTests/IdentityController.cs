using MyShop.API.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyShop.API.IntegrationTests
{
    public class IdentityController : IntegrationTest
    {
        public IdentityController(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Login_WithEmailWhichDoesntExist_ShouldReturn400StatusCode()
        {
            var user = new UserLoginRequest { Email = "EmailDoesntexist@test.integration", Password = "Pwd1234#" };


     var    response  = await   Login(user);


            Assert.True(response.Errors.Any());

        }

        [Fact]
        public async Task Register_CreateCorrectUser_ShouldReturnTokens()
        {
            var email = Guid.NewGuid().ToString().Substring(0, 7) + "@" + "test.integration";
            var user = new UserRegistrationRequest { Email = email, Password = "Pwd1234#" };


            var response = await Register(user);


            Assert.NotNull(response.RefreshToken);
            Assert.NotNull(response.Token);

        }

    }
}
