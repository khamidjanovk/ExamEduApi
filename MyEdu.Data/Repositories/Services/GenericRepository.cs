﻿using MyEdu.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEdu.Data.Repositories.Services
{
#pragma warning disable
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal EducationCenterDbContext dbContext;
        internal DbSet<T> dbSet;
        private readonly ILogger logger;
        public GenericRepository(EducationCenterDbContext dbContext, ILogger logger)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
            this.logger = logger;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression is null ? dbSet : dbSet.Where(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = dbSet.Update(entity);

            return entry.Entity;
        }
    }
}
