namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goods", "ManufacturerPartNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.Goods", "MouserPartNumber", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goods", "MouserPartNumber", c => c.String());
            AlterColumn("dbo.Goods", "ManufacturerPartNumber", c => c.String());
        }
    }
}
