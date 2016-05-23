namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        hours = c.Int(nullable: false),
                        approved = c.Boolean(nullable: false),
                        ProjectID = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectItem", t => t.ProjectID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        isAdmin = c.Boolean(nullable: false),
                        Name = c.String(),
                        Major = c.String(),
                        Ucard = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProjectItemUser",
                c => new
                    {
                        ProjectItemID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectItemID, t.UserID })
                .ForeignKey("dbo.ProjectItem", t => t.ProjectItemID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProjectItemID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectItem", "ProjectID", "dbo.ProjectItem");
            DropForeignKey("dbo.ProjectItemUser", "UserID", "dbo.User");
            DropForeignKey("dbo.ProjectItemUser", "ProjectItemID", "dbo.ProjectItem");
            DropIndex("dbo.ProjectItemUser", new[] { "UserID" });
            DropIndex("dbo.ProjectItemUser", new[] { "ProjectItemID" });
            DropIndex("dbo.ProjectItem", new[] { "ProjectID" });
            DropTable("dbo.ProjectItemUser");
            DropTable("dbo.User");
            DropTable("dbo.ProjectItem");
        }
    }
}
