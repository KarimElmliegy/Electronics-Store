using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace E_commerce.Infrastructure.Data
{
    internal class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> Spec)
        {
            if (Spec.Criteria != null)
                query = query.Where(Spec.Criteria);

            if (Spec.OrderBy != null)
                query = query.OrderBy(Spec.OrderBy);

            if (Spec.OrderByDesc != null)
                query = query.OrderByDescending(Spec.OrderByDesc);

            if (Spec.Includes != null)
                query = Spec.Includes.Aggregate(query, (currectQuery, includeExc) => currectQuery.Include(includeExc));

            if (Spec.IsDistinct)
            {
                query.Distinct();
            }
            return query;
        }

        public static IQueryable<Tresult> GetQuery<Tresult>(IQueryable<T> query, ISpecification<T, Tresult> Spec)
        {
            if (Spec.Criteria != null)
                query = query.Where(Spec.Criteria);

            if (Spec.OrderBy != null)
                query = query.OrderBy(Spec.OrderBy);

            if (Spec.OrderByDesc != null)
                query = query.OrderByDescending(Spec.OrderByDesc);

            if (Spec.Includes != null)
                query = Spec.Includes.Aggregate(query, (currectQuery, includeExc) => currectQuery.Include(includeExc));

            var selectQuery = query as IQueryable<Tresult>;

            if (Spec.Select != null)
            {
                selectQuery = query.Select(Spec.Select);
            }

            if (Spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }
            return selectQuery ?? query.Cast<Tresult>();
        }


    }
}
