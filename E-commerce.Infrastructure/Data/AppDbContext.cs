using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using E_commerce.Core.Entities;

namespace E_commerce.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // Set global conventions for string properties and decimal properties
            configurationBuilder.Properties<string>().HaveMaxLength(100);
            configurationBuilder.Properties<decimal>().HaveColumnType("decimal(18,2)");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the assembly Override Global Configurations 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);









        }
    }
}
