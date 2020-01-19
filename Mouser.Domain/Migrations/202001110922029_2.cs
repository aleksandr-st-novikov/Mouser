namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApiRegInfoes", "IsActive", c => c.Boolean(nullable: false));
            //DropColumn("dbo.ApiRegInfoes", "IaActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApiRegInfoes", "IaActive", c => c.Boolean(nullable: false));
            //DropColumn("dbo.ApiRegInfoes", "IsActive");
        }
    }
}
