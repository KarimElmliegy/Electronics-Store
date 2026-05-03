using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace E_commerce.Infrastructure.Data
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
       
        public void AddProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product); 
        }

        public async Task<IReadOnlyCollection<Product>> GetAllProductsAsync(string? brand, string? type, string? sort)
        {
            var query = context.Products.AsQueryable(); 
            
            if(! string.IsNullOrWhiteSpace(brand) )
                query = query.Where(b => b.Brand == brand);
            if(! string.IsNullOrWhiteSpace(type) )
                query = query.Where(b=>b.Type == type);

            query = sort switch
            {
                "priceAsc" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),
                _ => query.OrderBy(_ => _.Name)
            };

          return  await query.ToListAsync(); 
        }

        public async Task<IReadOnlyCollection<string>> GetBrandsAsynch()
        {
            return await context.Products.Select(x => x.Brand).Distinct().ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id) ;
        }

        public async Task<IReadOnlyCollection<string>> GetTypesAsynch()
        {
            return await context.Products.Select(x => x.Type).Distinct().ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return context.Products.Any(x=> x.Id == id ); 
        }

        public async Task<bool> SaveChangesAsynch()
        {
            return await context.SaveChangesAsync() > 0 ; 
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }
    }
}
