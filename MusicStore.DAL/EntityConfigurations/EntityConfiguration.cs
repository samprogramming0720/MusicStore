using Microsoft.AspNet.Identity.EntityFramework;
using MusicStore.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MusicStore.DAL.EntityConfigurations
{
    internal class AlbumConfiguration:EntityTypeConfiguration<Album>
    {
        internal AlbumConfiguration()
        {
            ToTable("Albums");
            Property(a => a.ID).HasColumnName("AlbumID");
            HasKey(a => a.ID);
            Property(a => a.AlbumTitle).IsRequired().HasMaxLength(100);
            HasMany(a => a.Music).WithRequired(m=>m.Album).HasForeignKey(m => m.AlbumID);
        }
    }

    internal class ArtistConfiguration : EntityTypeConfiguration<Artist>
    {
        internal ArtistConfiguration()
        {
            ToTable("Artists");
            Property(a => a.ID).HasColumnName("ArtistID");
            HasKey(a => a.ID);
            Property(a => a.Name).IsRequired().HasMaxLength(100);
            Property(a => a.Description).IsOptional().HasMaxLength(2000);
            HasMany(a => a.Albums).WithRequired(a => a.Artist).HasForeignKey(a => a.ArtistID);
            HasMany(a => a.Music).WithRequired(a => a.Artist).HasForeignKey(a => a.ArtistID).WillCascadeOnDelete(false);
        }
    }

    internal class MusicConfiguration : EntityTypeConfiguration<Music>
    {
        internal MusicConfiguration()
        {
            ToTable("Music");
            HasKey(m => m.ID);
            Property(m => m.Title).IsRequired().HasMaxLength(100);
            Property(m => m.Genre).IsOptional().HasMaxLength(50);
            Property(m => m.MusicVideoURI).HasMaxLength(200);
            HasMany(m => m.PlayLists).WithMany(pl => pl.Music).Map(mpl =>
            {
                mpl.MapLeftKey("PlayListID");
                mpl.MapRightKey("MusicID");
                mpl.ToTable("PlayListMusic");
            });
        }
    }

    internal class PlayListConfiguration: EntityTypeConfiguration<PlayList>
    {
        internal PlayListConfiguration()
        {
            ToTable("PlayLists");
            Property(pl => pl.ID).HasColumnName("PlayListID");
            HasKey(pl => pl.ID);
            Property(pl => pl.Name).IsRequired().HasMaxLength(100);
        }
    }
    
    internal class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser> 
    {
        internal ApplicationUserConfiguration()
        {
            ToTable("User");
            Property(u => u.Id).HasColumnName("UserID");
            HasKey(u => u.Id);
            Property(u => u.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Ignore(u => u.PhoneNumber);
            Ignore(u => u.PhoneNumberConfirmed);
            Property(u => u.UserName).IsRequired().HasMaxLength(100);
            Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            Property(u => u.LastName).IsRequired().HasMaxLength(100);
            Property(u => u.Email).IsRequired().HasMaxLength(100);
            HasMany(u => u.Logins).WithOptional().HasForeignKey(ul => ul.UserId);
            HasMany(u => u.Claims).WithOptional().HasForeignKey(c => c.UserId);
            HasMany(u => u.Roles).WithRequired().HasForeignKey(r => r.UserId);
            HasMany(u => u.PlayLists).WithRequired(p => p.User).HasForeignKey(p => p.UserID);
        }
    }

    internal class ApplicationUserRoleConfiguration : EntityTypeConfiguration<ApplicationUserRole>
    {
        internal ApplicationUserRoleConfiguration()
        {
            ToTable("UserRole");
            HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }

    internal class ApplicationRoleConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        internal ApplicationRoleConfiguration()
        {
            ToTable("Role");
            Property(r => r.Id).HasColumnName("RoleID");
            Property(r => r.Name).IsRequired().HasMaxLength(50);
            HasKey(r => r.Id);
            Property(r => r.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            HasMany(r => r.Users).WithRequired().HasForeignKey(u => u.RoleId);
        }
    }

    internal class ApplicationUserClaimConfiguration : EntityTypeConfiguration<ApplicationUserClaim>
    {
        internal ApplicationUserClaimConfiguration()
        {
            ToTable("UserClaim");
            Property(uc => uc.Id).HasColumnName("UserClaimID");
            Property(r => r.ClaimType).IsRequired().HasMaxLength(50);
            Property(r => r.ClaimValue).IsRequired().HasMaxLength(50);
            HasKey(uc => uc.Id);
            Property(uc => uc.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }

    internal class ApplicationUserLoginConfiguration: EntityTypeConfiguration<ApplicationUserLogin>
    {
        internal ApplicationUserLoginConfiguration()
        {
            ToTable("UserLogin");
            HasKey(ul => new { ul.LoginProvider, ul.ProviderKey, ul.UserId });
        }
    }
}
