namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApiSearchSessions", "MachineName", c => c.String());
            AddColumn("dbo.ApiSearchSessions", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApiSearchSessions", "Description");
            DropColumn("dbo.ApiSearchSessions", "MachineName");
        }
    }
}
