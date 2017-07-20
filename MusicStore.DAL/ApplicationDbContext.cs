using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using MusicStore.Entities;
using MusicStore.DAL.EntityConfigurations;
using MusicStore.DAL.Migrations;

namespace MusicStore.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, 
        int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        #region EntitySets
        IDbSet<Album> Albums { get; set; }
        IDbSet<Artist> Artists { get; set; }
        IDbSet<Music> Music { get; set; }
        IDbSet<PlayList> PlayLists { get; set; }
        IDbSet<Error> Errors { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region EntityConfiguration
            modelBuilder.HasDefaultSchema("MusicStore");
            modelBuilder.Configurations.Add(new AlbumConfiguration());
            modelBuilder.Configurations.Add(new ArtistConfiguration());
            modelBuilder.Configurations.Add(new MusicConfiguration());
            modelBuilder.Configurations.Add(new PlayListConfiguration());
            //Identity Configuration
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserRoleConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserClaimConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserLoginConfiguration());
            modelBuilder.Configurations.Add(new ApplicationRoleConfiguration());
            #endregion
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>)();
            
        }

    }
}
