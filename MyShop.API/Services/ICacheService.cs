using System;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public interface ICacheService
    {
        Task<string> GetValueAsync(string key);
        Task SetValueAsync(string key, object value, TimeSpan expiry);

    }
}