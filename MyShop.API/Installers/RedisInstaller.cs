using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.API.Caching;
using MyShop.API.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Installers
{
    public class RedisInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = new RedisSettings();

            configuration.Bind(nameof(RedisSettings), redisSettings);
            services.AddSingleton(redisSettings);
            services.AddSingleton<ICacheService, CacheService>();

            services.AddSingleton<IConnectionMultiplexer>(builder => ConnectionMultiplexer.Connect(redisSettings.ConnectionString));
        }
    }
}
