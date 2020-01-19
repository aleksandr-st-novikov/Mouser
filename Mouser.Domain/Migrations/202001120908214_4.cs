namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manufacturers", "NumberOfResult", c => c.Int(nullable: false));
            AddColumn("dbo.Manufacturers", "StartingRecord", c => c.Int(nullable: false));
            DropColumn("dbo.ApiSearchSessions", "NumberOfResult");
            DropColumn("dbo.ApiSearchSessions", "StartingRecord");
            DropColumn("dbo.ApiSearchSessions", "MachineName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApiSearchSessions", "MachineName", c => c.String());
            AddColumn("dbo.ApiSearchSessions", "StartingRecord", c => c.Int(nullable: false));
            AddColumn("dbo.ApiSearchSessions", "NumberOfResult", c => c.Int(nullable: false));
            DropColumn("dbo.Manufacturers", "StartingRecord");
            DropColumn("dbo.Manufacturers", "NumberOfResult");
        }
    }
}
