using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.API.Data;
using MyShop.API.Helpers;
using MyShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Installers
{
    public class DataInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();
          
            services.AddTransient<IProductTagsHelper, ProductTagsHelper>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
