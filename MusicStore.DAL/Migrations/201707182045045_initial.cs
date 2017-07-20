namespace MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MusicStore.Albums",
                c => new
                    {
                        AlbumID = c.Int(nullable: false, identity: true),
                        AlbumTitle = c.String(nullable: false, maxLength: 100),
                        AlbumCover = c.String(),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumID)
                .ForeignKey("MusicStore.Artists", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "MusicStore.Artists",
                c => new
                    {
                        ArtistID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Image = c.String(),
                        Description = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.ArtistID);
            
            CreateTable(
                "MusicStore.Music",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        MusicVideoURI = c.String(maxLength: 200),
                        Genre = c.String(maxLength: 50),
                        AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("MusicStore.Albums", t => t.AlbumID, cascadeDelete: true)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "MusicStore.PlayLists",
                c => new
                    {
                        PlayListID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayListID)
                .ForeignKey("MusicStore.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "MusicStore.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "MusicStore.UserClaim",
                c => new
                    {
                        UserClaimID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(nullable: false, maxLength: 50),
                        ClaimValue = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserClaimID)
                .ForeignKey("MusicStore.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "MusicStore.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("MusicStore.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "MusicStore.UserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("MusicStore.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("MusicStore.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "MusicStore.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "MusicStore.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "MusicStore.PlayListMusic",
                c => new
                    {
                        PlayListID = c.Int(nullable: false),
                        MusicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayListID, t.MusicID })
                .ForeignKey("MusicStore.Music", t => t.PlayListID, cascadeDelete: true)
                .ForeignKey("MusicStore.PlayLists", t => t.MusicID, cascadeDelete: true)
                .Index(t => t.PlayListID)
                .Index(t => t.MusicID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("MusicStore.UserRole", "RoleId", "MusicStore.Role");
            DropForeignKey("MusicStore.Music", "AlbumID", "MusicStore.Albums");
            DropForeignKey("MusicStore.PlayListMusic", "MusicID", "MusicStore.PlayLists");
            DropForeignKey("MusicStore.PlayListMusic", "PlayListID", "MusicStore.Music");
            DropForeignKey("MusicStore.UserRole", "UserId", "MusicStore.User");
            DropForeignKey("MusicStore.PlayLists", "UserID", "MusicStore.User");
            DropForeignKey("MusicStore.UserLogin", "UserId", "MusicStore.User");
            DropForeignKey("MusicStore.UserClaim", "UserId", "MusicStore.User");
            DropForeignKey("MusicStore.Albums", "ArtistID", "MusicStore.Artists");
            DropIndex("MusicStore.PlayListMusic", new[] { "MusicID" });
            DropIndex("MusicStore.PlayListMusic", new[] { "PlayListID" });
            DropIndex("MusicStore.UserRole", new[] { "RoleId" });
            DropIndex("MusicStore.UserRole", new[] { "UserId" });
            DropIndex("MusicStore.UserLogin", new[] { "UserId" });
            DropIndex("MusicStore.UserClaim", new[] { "UserId" });
            DropIndex("MusicStore.PlayLists", new[] { "UserID" });
            DropIndex("MusicStore.Music", new[] { "AlbumID" });
            DropIndex("MusicStore.Albums", new[] { "ArtistID" });
            DropTable("MusicStore.PlayListMusic");
            DropTable("MusicStore.Role");
            DropTable("MusicStore.Errors");
            DropTable("MusicStore.UserRole");
            DropTable("MusicStore.UserLogin");
            DropTable("MusicStore.UserClaim");
            DropTable("MusicStore.User");
            DropTable("MusicStore.PlayLists");
            DropTable("MusicStore.Music");
            DropTable("MusicStore.Artists");
            DropTable("MusicStore.Albums");
        }
    }
}
