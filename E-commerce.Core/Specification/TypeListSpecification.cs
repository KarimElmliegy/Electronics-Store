using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_commerce.Core.Specification
{
    public class TypeListSpecification : BaseSpecification<Product,string>
    {
        public TypeListSpecification() 
        {
            AddSelect(x=> x.Type);
            ApplayDistinct(); 
        }
    }
}
