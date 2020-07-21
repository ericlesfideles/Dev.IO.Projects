using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Interfaces
{
    public interface IRepository<TEntity>: IDisposable where TEntity: Entity
    {

        Task Create(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> ListAll();
        Task Edit(TEntity entity);
        Task Delete(Guid id);
        Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
