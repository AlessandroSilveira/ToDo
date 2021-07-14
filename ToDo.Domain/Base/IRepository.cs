using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDo.Domain.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Update(TEntity obj);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate);
        void DetachLocal(Func<TEntity, bool> predicate);
    }
}