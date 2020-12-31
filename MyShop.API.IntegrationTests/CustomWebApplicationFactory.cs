using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyShop.API.Data;
using System;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace MyShop.API.IntegrationTests
{
    public class CustomWebApplicationFactory<IStartup> : WebApplicationFactory<IStartup> where IStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DataContext>));

                services.Remove(descriptor);
               

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(CreateSqlConnection());
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<Startup>>>();

                    db.Database.EnsureCreated();


                }
            });
            
        }
        private static DbConnection CreateSqlConnection()
        {
            var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyShopDBApi;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();

            return connection;
        }
    }
}
