namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Manufacturers", "GoodsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Manufacturers", "GoodsCount", c => c.Int(nullable: false));
        }
    }
}
