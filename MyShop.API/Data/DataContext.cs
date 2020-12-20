using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.API.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.API.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<TagDTO> Tags { get; set; }
        public DbSet<ProductTagBridgeTable> PTBridges { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductTagBridgeTable>().HasNoKey();
          
            base.OnModelCreating(builder);
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
    }
}
