using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_commerce.Infrastructure.Data
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
    {

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity); 
        }

        public bool Exists(int id)
        {
            return context.Set<T>().Any(i=>i.Id==id) ;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsynch()
        {
            return await context.Set<T>().ToListAsync(); 
        }

        public async Task<T?> GetByIdAsynch(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync() ;
        }

        public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync() ;
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync(); 
        }

        public async Task<IReadOnlyCollection<T>> ListWithSpecificationAsynch(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync(); 
        }

        public async Task<bool> SaveAsynch()
        {
            return await context.SaveChangesAsync() > 0 ; 
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
           return SpecificationEvaluator<T>.GetQuery(context.Set<T>(),spec);
        }

        private IQueryable<Tresult> ApplySpecification<Tresult>(ISpecification<T, Tresult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }
    }
}
