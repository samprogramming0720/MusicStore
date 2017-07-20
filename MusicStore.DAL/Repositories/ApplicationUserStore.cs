using Microsoft.AspNet.Identity.EntityFramework;
using MusicStore.DAL.Infrastructure;
using MusicStore.Entities;

namespace MusicStore.DAL.Repositories
{
    public class ApplicationUserStore : 
        UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, 
        IApplicationUserStore
    {
        public ApplicationUserStore(IDbFactory context) : base(context.Create())
        {
        }
        
    }
}
