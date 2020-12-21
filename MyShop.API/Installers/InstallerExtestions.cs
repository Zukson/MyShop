using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Installers
{
    public static class InstallerExtestions
    {

        public static   void InstallServicesInAssembly(this IServiceCollection services,IConfiguration configuration)
        {
            
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && x.IsClass).Select(x => Activator.CreateInstance(x)).Cast<IInstaller>().ToList();
            installers.ForEach(x => x.ConfigureServices(services, configuration));


        }
    }
}
