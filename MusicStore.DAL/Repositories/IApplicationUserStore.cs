using Microsoft.AspNet.Identity;
using MusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public interface IApplicationUserStore:IUserStore<ApplicationUser, int>
    {

    }
}
