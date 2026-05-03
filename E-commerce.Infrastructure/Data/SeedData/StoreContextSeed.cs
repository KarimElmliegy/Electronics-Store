using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace E_commerce.Infrastructure.Data.SeedData
{
    public static class StoreContextSeed
    {
        public static async  Task SeddAsynch(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                var productsData = await System.IO.File.ReadAllTextAsync(
    "../E-commerce.Infrastructure/Data/SeedData/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(productsData); 
                if(Products==null) { return ; }

                await context.AddRangeAsync(Products);

                await context.SaveChangesAsync();  
            }
        }
    }
}
