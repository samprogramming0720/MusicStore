using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Infrastructure
{
    public class DbFactory:Disposable, IDbFactory
    {
        ApplicationDbContext _dbContext;

        public ApplicationDbContext Create()
        {
            return _dbContext ?? (_dbContext = new ApplicationDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
