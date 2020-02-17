namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoodDataErrors", "Url", c => c.String());
            AddColumn("dbo.GoodDatas", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoodDatas", "Url");
            DropColumn("dbo.GoodDataErrors", "Url");
        }
    }
}
