using Microsoft.AspNetCore.Mvc.Testing;
using MyShop.API.Contracts.V1;
using MyShop.API.Contracts.V1.Requests;
using MyShop.API.Contracts.V1.Responses;
using MyShop.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyShop.API.IntegrationTests
{
 public   class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        protected readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup>
            _factory;

        public IntegrationTest(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });


        }
        protected async Task AuthenticateAsync()
        {
            UserRegistrationRequest user = new UserRegistrationRequest 
            { Email = Guid.NewGuid().ToString()+"@"+"integration.test", 
                Password = "Benek123#" };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(user));
        }
        private async Task<string> GetJwtAsync(UserRegistrationRequest user)
        {
            var response = await _client.PostAsJsonAsync(ApiRoutes.Identity.Register,user);

            var responseContent = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
            return responseContent.Token;
        }

        protected async Task<ProductResponse>CreateProduct(PostProductRequest productRequest)
        {

            var response = await _client.PostAsJsonAsync(ApiRoutes.Products.PostProduct,productRequest);

            var resposneContent = await response.Content.ReadFromJsonAsync<ProductResponse>();
            return resposneContent;
        }

        protected async Task<AuthFailedResponse>Login(UserLoginRequest user)
        {
         var response=   await _client.PostAsJsonAsync(ApiRoutes.Identity.Login, user);

            var content = await response.Content.ReadFromJsonAsync<AuthFailedResponse>();
            return content;
        }
        protected async Task<AuthSuccessResponse>Register(UserRegistrationRequest user)
        {
            var response = await _client.PostAsJsonAsync(ApiRoutes.Identity.Register, user);
            var content = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
            return content;
        }
    }
}
