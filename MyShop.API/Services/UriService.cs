using MyShop.API.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPostUri(string productId)
        {
            return new Uri(_baseUri + ApiRoutes.Products.GetProductById.Replace("{productId}", productId));
            
        }
    }
}
