namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoodDatas", "Location", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoodDatas", "Location");
        }
    }
}
