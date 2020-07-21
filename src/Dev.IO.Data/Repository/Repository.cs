using AppMvcBasic.Models;
using Dev.IO.Data.Context;
using DevIO.Bussiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MyDbContext _MyDbContext;

        protected readonly DbSet<TEntity> DbSet;
        public Repository(MyDbContext myDbContext)
        {
            _MyDbContext = myDbContext;
            DbSet = _MyDbContext.Set<TEntity>();        
        }

        public virtual async Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ListAll()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task Create(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();

        }

        public virtual async Task Edit(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }
        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _MyDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _MyDbContext?.Dispose();
        }
    }
}
