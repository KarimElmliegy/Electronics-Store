using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E_commerce.Core.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; }
        public Expression<Func<T, object>>? OrderBy { get; }

        public Expression<Func<T, object>>? OrderByDesc { get; }

        bool IsDistinct { get; }

    }
    public interface ISpecification<T, Tentity> : ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, Tentity>>? Select { get; }
    }
}
