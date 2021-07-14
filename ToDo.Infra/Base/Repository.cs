using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Base;
using ToDo.Infra.Context;

namespace ToDo.Infra.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DataContext Context;

        public Repository(DataContext context)
        {
            Context = context;
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            try
            {
                
           
                await Context.Set<TEntity>().AddAsync(obj);
                await Context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Inserir ", ex); 
            }
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToList();
        }

        public async Task<TEntity> Update(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Detached;
            Context.Entry(obj).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return obj;
        }

        public async Task Remove(Guid id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate);
        }

      

        public async Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public void DetachLocal(Func<TEntity, bool> predicate)
        {
            var local = Context.Set<TEntity>().Local.Where(predicate).FirstOrDefault();
            if (local !=null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}