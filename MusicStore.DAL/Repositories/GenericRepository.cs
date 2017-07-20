using MusicStore.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity>
        where TEntity:class
    {
        private ApplicationDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ApplicationDbContext DbContext
        {
            get
            {
                return _dbContext ?? (_dbContext = DbFactory.Create());
            }
        }

        public GenericRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<TEntity>();
        }
        
        public IQueryable<TEntity> IncludeAll(params Expression<Func<TEntity, object>>[] navProperties)
        {
            IQueryable<TEntity> query = DbSet;
            foreach(var navProperty in navProperties)
            {
                query = query.Include(navProperty);
            }
            return query;
        }

        public TEntity GetSingle(object id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Add(TEntity entityToAdd)
        {
            DbSet.Add(entityToAdd);
        }

        public void Delete(TEntity entityToDelete)
        {
            DbEntityEntry DbEntityEntry = DbContext.Entry(entityToDelete);
            DbEntityEntry.State = EntityState.Deleted;
        }

        public void Edit(TEntity entityToEdit)
        {
            DbEntityEntry DbEntityEntry = DbContext.Entry(entityToEdit);
            DbEntityEntry.State = EntityState.Modified;
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
    }
}
