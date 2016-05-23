namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSizeConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectItem", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.ProjectItem", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.User", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.User", "Major", c => c.String(maxLength: 20));
            AlterColumn("dbo.User", "Ucard", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Ucard", c => c.String());
            AlterColumn("dbo.User", "Major", c => c.String());
            AlterColumn("dbo.User", "Name", c => c.String());
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.ProjectItem", "Description", c => c.String());
            AlterColumn("dbo.ProjectItem", "Name", c => c.String());
        }
    }
}
