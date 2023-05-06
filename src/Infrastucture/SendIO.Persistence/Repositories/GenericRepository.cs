using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SendIO.Application.Interfaces;
using SendIO.Domain.Entities;
using SendIO.Persistence.Context;

namespace SendIO.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected SendIOContext Context;

        public GenericRepository(SendIOContext context)
        {
            Context = context;
        }

        public async Task<Guid> Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return 1;
        }

    }
}

