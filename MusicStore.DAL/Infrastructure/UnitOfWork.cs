using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private ApplicationDbContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public ApplicationDbContext DbContext
        {
            get { return _dbContext ?? ( _dbContext = _dbFactory.Create());  }
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}
