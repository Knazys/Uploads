namespace Uploads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uploads", "ContentType", c => c.String());
            AddColumn("dbo.Uploads", "Content", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uploads", "Content");
            DropColumn("dbo.Uploads", "ContentType");
        }
    }
}
