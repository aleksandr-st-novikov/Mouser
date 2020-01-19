namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiRegInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartnerId = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApiRegInfoes");
        }
    }
}
