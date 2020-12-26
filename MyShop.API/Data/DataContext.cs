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
        public DbSet<ProductTagBridgeDTO> PTBridges { get; set; }
        public DbSet<RefreshTokenDTO> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductTagBridgeDTO>().HasNoKey();
          
            base.OnModelCreating(builder);
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
    }
}
