namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsCategory", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsCategory");
        }
    }
}
