using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShop.API.Filters;
using MyShop.API.Services;
using MyShop.API.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.API.Installers
{
    public class MVCInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddMvc(options => { options.EnableEndpointRouting = false;
                options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(mvcConfiguration=> { mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>(); });
            var tokenValidationParameters= new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:50654",
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true

            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {

                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {

                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            }
             );
           
            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });
            services.AddScoped<IIdentityService, IdentityService>();
        }

        
    }
}
