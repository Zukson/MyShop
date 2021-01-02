using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public CacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }
        public async Task<string> GetValueAsync(string key)
        {
          var db =  _connectionMultiplexer.GetDatabase();
            var value = await  db.StringGetAsync(key);
            return value;
        }
      public async   Task SetValueAsync(string key, object value,TimeSpan expiry)
        {
            if(value is null)
            {
                return;
            }
            var db = _connectionMultiplexer.GetDatabase();
          
            var jsonValue = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, jsonValue, expiry);
                

        }

    }
}
