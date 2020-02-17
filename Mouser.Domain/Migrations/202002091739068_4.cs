namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoodDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Response = c.String(),
                        Good_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.Good_Id)
                .Index(t => t.Good_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoodDatas", "Good_Id", "dbo.Goods");
            DropIndex("dbo.GoodDatas", new[] { "Good_Id" });
            DropTable("dbo.GoodDatas");
        }
    }
}
