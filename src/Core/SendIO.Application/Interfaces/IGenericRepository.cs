using System;
using System.Linq.Expressions;
using SendIO.Domain.Entities;

namespace SendIO.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        Task<Guid> Add(TEntity entity);

        Task<int> Update(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetById(Guid id);

        Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    }
}

