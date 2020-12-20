using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Installers
{
    interface IInstaller
    {

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration );   
    }
}
