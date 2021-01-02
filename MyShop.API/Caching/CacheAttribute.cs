using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.API.Caching
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _seconds;
        public CacheAttribute(int seconds)
        {
            _seconds = seconds;
        }
        public async  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var redisSettings = context.HttpContext.RequestServices.GetService(typeof(RedisSettings)) as RedisSettings;
            if( redisSettings.Enabled == false)

            {

                await next();
                return;
            }
            else
            {
                var cacheService = context.HttpContext.RequestServices.GetService(typeof(ICacheService)) as ICacheService;
                var key = GenerateCacheKeyFromRequset(context.HttpContext.Request);
                var cachedResponse = await cacheService.GetValueAsync(key);
             if ( !string.IsNullOrEmpty(cachedResponse))
                {
                    context.Result = new ContentResult
                    {
                        Content = cachedResponse,
                        StatusCode = 200,
                        ContentType = "application / json"

                    };
                }

             else
                {

                    var executedContext = await next();
                    if(executedContext.Result is OkObjectResult ok)
                    {
                        await cacheService.SetValueAsync(key, ok.Value, TimeSpan.FromSeconds(_seconds));
                    }
                    return;
                }

            }

            
        
        }
        private static string GenerateCacheKeyFromRequset(HttpRequest httpRequest)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{httpRequest.Path}");
            foreach(var (key,value) in httpRequest.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();

        }
    }
}
