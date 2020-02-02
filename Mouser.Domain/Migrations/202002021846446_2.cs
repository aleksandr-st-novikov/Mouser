namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "IsWebUpdated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Goods", "IsBusy", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "IsBusy");
            DropColumn("dbo.Goods", "IsWebUpdated");
        }
    }
}
