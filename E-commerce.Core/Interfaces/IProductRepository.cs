using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_commerce.Core.Interfaces
{
    public interface IProductRepository
    {
        public Task<IReadOnlyCollection<Product>> GetAllProductsAsync(string? brand , string? type, string? sort);

        public Task<Product?> GetProductByIdAsync(int id); 

        public Task<IReadOnlyCollection<string>> GetBrandsAsynch();

        public Task<IReadOnlyCollection<string>> GetTypesAsynch();

        public void AddProduct(Product product);

        public void UpdateProduct(Product product);

        public void DeleteProduct(Product product);

        bool ProductExists(int id);

        public Task<bool> SaveChangesAsynch(); 
    }
}
