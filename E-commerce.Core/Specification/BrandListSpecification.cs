using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_commerce.Core.Specification
{
    public class BrandListSpecification : BaseSpecification<Product,string>
    {
        public BrandListSpecification()
        {
            AddSelect(x=>x.Brand);
            ApplayDistinct(); 
        }
    }
}
