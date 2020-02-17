namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GoodDatas", "Response", c => c.String(unicode: true, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GoodDatas", "Response", c => c.String());
        }
    }
}
