namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApiSearchSessions", "MachineName", c => c.String());
            AddColumn("dbo.ApiSearchSessions", "ApiRegInfo_Id", c => c.Int());
            CreateIndex("dbo.ApiSearchSessions", "ApiRegInfo_Id");
            AddForeignKey("dbo.ApiSearchSessions", "ApiRegInfo_Id", "dbo.ApiRegInfoes", "Id");
            DropColumn("dbo.ApiSearchSessions", "PartnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApiSearchSessions", "PartnerId", c => c.String());
            DropForeignKey("dbo.ApiSearchSessions", "ApiRegInfo_Id", "dbo.ApiRegInfoes");
            DropIndex("dbo.ApiSearchSessions", new[] { "ApiRegInfo_Id" });
            DropColumn("dbo.ApiSearchSessions", "ApiRegInfo_Id");
            DropColumn("dbo.ApiSearchSessions", "MachineName");
        }
    }
}
