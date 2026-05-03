using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;

namespace E_commerce.Core.Specification
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T> where T : BaseEntity
    {

        protected BaseSpecification():this(null) 
        {

        }
        public Expression<Func<T, bool>> Criteria => criteria ;

        public Expression<Func<T, object>>? OrderBy { get; private set;  }

        public Expression<Func<T, object>>? OrderByDesc { get; private set;  }

        public List<Expression<Func<T, object>>> Includes { get;  } = new List<Expression<Func<T, object>>>();

        public bool IsDistinct { get; set; }

        protected  void AddOrderBy(Expression<Func<T,object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> OrderByExpression)
        {
            OrderByDesc= OrderByExpression;
        }
        protected void AddIncludes(Expression<Func<T, object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }
        protected void ApplayDistinct()
        {
            IsDistinct = true;
        }
    }
    public class BaseSpecification<T, Tentity>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>, ISpecification<T, Tentity> where T : BaseEntity
    {
        protected BaseSpecification() : this(null) {}
        public Expression<Func<T, Tentity>>? Select { get; private set;  }

        protected void AddSelect(Expression<Func<T, Tentity>>? selectExp)
        {
            Select = selectExp; 
        }
    }
}
