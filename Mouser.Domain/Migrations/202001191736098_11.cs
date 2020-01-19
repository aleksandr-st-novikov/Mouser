namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manufacturers", "SearchText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Manufacturers", "SearchText");
        }
    }
}
