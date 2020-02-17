namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GoodDatas", "Response", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GoodDatas", "Response", c => c.String(unicode: false, storeType: "text"));
        }
    }
}
