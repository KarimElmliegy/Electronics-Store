using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_commerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T?> GetByIdAsynch(int id);

        public Task<IReadOnlyCollection<T>> GetAllAsynch();

        public Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        public Task<IReadOnlyCollection<T>> ListWithSpecificationAsynch(ISpecification<T> spec);

        Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
        Task < IReadOnlyList < TResult >> ListAsync<TResult>(ISpecification < T, TResult > spec);


        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);

        Task<bool> SaveAsynch();

        bool Exists(int id);
    }
}
