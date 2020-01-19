namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApiSearchSessions", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApiSearchSessions", "CreateDate");
        }
    }
}
