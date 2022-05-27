using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TechApp
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbSet<T> dbSet;
        protected DbContext dbContext;
        protected IDbContextTransaction Transaction;

        public GenericRepository(DbContext dataContext)
        {
            this.dbSet = dataContext.Set<T>();
            this.dbContext = dataContext;
        }

        public virtual T Create(T entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Added;
            this.dbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<T> Create(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.dbContext.Entry(entity).State = EntityState.Added;
            }
            this.dbContext.SaveChanges();
            return entities;
        }

        public IQueryable<T> Get()
        {
            return this.dbSet;
        }

        public T Update(T entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.dbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.dbContext.Entry(entity).State = EntityState.Modified;
            }
            this.dbContext.SaveChanges();
            return entities;
        }


        public virtual void Delete(T entity)
        {
            this.dbContext.Remove(entity);
            this.dbContext.SaveChanges();
        }
    }
}
