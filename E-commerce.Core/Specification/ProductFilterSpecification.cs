using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_commerce.Core.Specification
{
    public class ProductFilterSpecification : BaseSpecification<Product>
    {

        public ProductFilterSpecification(string? brand, string? type, string? Sort) : base(c =>
    (string.IsNullOrWhiteSpace(brand) || c.Brand == brand) &&
    (string.IsNullOrWhiteSpace(type) || c.Type == type))
        {
            switch (Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;

                case "priceDesc":
                    AddOrderByDesc(x => x.Price);
                    break;

                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
            // AddIncludes(x => x.Price);
            // AddIncludes(x => x.Price);

        }
    }
}
