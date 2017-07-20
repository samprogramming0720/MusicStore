namespace MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("MusicStore.Music", "ArtistID", c => c.Int(nullable: false));
            CreateIndex("MusicStore.Music", "ArtistID");
            AddForeignKey("MusicStore.Music", "ArtistID", "MusicStore.Artists", "ArtistID");
        }
        
        public override void Down()
        {
            DropForeignKey("MusicStore.Music", "ArtistID", "MusicStore.Artists");
            DropIndex("MusicStore.Music", new[] { "ArtistID" });
            DropColumn("MusicStore.Music", "ArtistID");
        }
    }
}
