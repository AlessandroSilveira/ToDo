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
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Erro ao inserir", ex);
            }
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult<IEnumerable<TEntity>>(_context.Set<TEntity>().ToList());
        }

        public async Task<TEntity> Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Detached;
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Remove(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return  Task.FromResult<IEnumerable<TEntity>>(_context.Set<TEntity>().Where(predicate));
        }

        public Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(_context.Set<TEntity>().FirstOrDefault(predicate));
        }

        public void DetachLocal(Func<TEntity, bool> predicate)
        {
            var local = _context.Set<TEntity>().Local.FirstOrDefault(predicate);
            if (local !=null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}