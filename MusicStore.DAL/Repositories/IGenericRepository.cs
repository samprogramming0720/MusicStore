using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> IncludeAll(params Expression<Func<TEntity, object>>[] navProperties);
        IQueryable<TEntity> GetAll();
        TEntity GetSingle(object id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entityToAdd);
        void Delete(TEntity entityToDelete);
        void Edit(TEntity entityToEdit);
    }
}
