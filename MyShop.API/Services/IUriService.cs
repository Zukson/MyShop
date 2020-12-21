using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public interface IUriService
    {
        Uri GetPostUri(string productId);

    }
}
