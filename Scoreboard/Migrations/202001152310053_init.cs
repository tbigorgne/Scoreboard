namespace Scoreboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Performances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Score = c.Int(nullable: false),
                        HomeViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HomeViewModels", t => t.HomeViewModel_Id)
                .Index(t => t.HomeViewModel_Id);
            
            CreateTable(
                "dbo.HomeViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListAscSorting = c.Boolean(),
                        SortingAlreadyInitialized = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Performances", "HomeViewModel_Id", "dbo.HomeViewModels");
            DropIndex("dbo.Performances", new[] { "HomeViewModel_Id" });
            DropTable("dbo.HomeViewModels");
            DropTable("dbo.Performances");
        }
    }
}
