using MyShop.API.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyShop.API.IntegrationTests
{
    public class ProductController : IntegrationTest
    {
        public ProductController(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task CreateProudct_IfProductDoesntExist_ShouldCreateNewProudct()
        {
          await   AuthenticateAsync();
            List<TagRequest> tags = new List<TagRequest>();
            tags.Add(new TagRequest { Name = "School" });
            var product = new PostProductRequest
            {
                Name = "Pencil",
                Description = "Yellow Pencil",
                QuantityInStock = 100,
                Tags=tags
                
            };
          
            
        var response=    await CreateProduct(product);
            Assert.Equal(product.Name, response.Name);
        }
    }
}
