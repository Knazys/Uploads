namespace Uploads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Uploads");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Size = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
