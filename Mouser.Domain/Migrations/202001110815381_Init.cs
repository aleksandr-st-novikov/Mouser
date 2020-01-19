namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlternatePackagings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        APMfrPN = c.String(),
                        Good_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.Good_Id, cascadeDelete: true)
                .Index(t => t.Good_Id);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataSheetUrl = c.String(),
                        Description = c.String(),
                        ImagePath = c.String(),
                        ManufacturerPartNumber = c.String(),
                        Min = c.Int(nullable: false),
                        Mult = c.Int(nullable: false),
                        MouserPartNumber = c.String(),
                        ProductDetailUrl = c.String(),
                        Reeling = c.Boolean(nullable: false),
                        ROHSStatus = c.String(),
                        Category_Id = c.Int(),
                        Manufacturer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Manufacturer_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(nullable: false),
                        Manufacturer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id, cascadeDelete: true)
                .Index(t => t.Manufacturer_Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameAPI = c.String(),
                        NameAlt = c.String(),
                        MouserUri = c.String(),
                        MouserID = c.Long(nullable: false),
                        IsUse = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceBreaks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.String(),
                        Currency = c.String(),
                        Good_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.Good_Id, cascadeDelete: true)
                .Index(t => t.Good_Id);
            
            CreateTable(
                "dbo.ProductAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AttributeName = c.String(),
                        AttributeValue = c.String(),
                        Good_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.Good_Id, cascadeDelete: true)
                .Index(t => t.Good_Id);
            
            CreateTable(
                "dbo.ProductCompliances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplianceName = c.String(),
                        ComplianceValue = c.String(),
                        Good_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.Good_Id, cascadeDelete: true)
                .Index(t => t.Good_Id);
            
            CreateTable(
                "dbo.ApiSearchSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartnerId = c.String(),
                        Date = c.DateTime(nullable: false),
                        NumberOfResult = c.Int(nullable: false),
                        StartingRecord = c.Int(nullable: false),
                        IsBusy = c.Boolean(nullable: false),
                        CountOfRequests = c.Int(nullable: false),
                        Manufacturer_Id = c.Int(),
                        Proxy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id)
                .ForeignKey("dbo.Proxies", t => t.Proxy_Id)
                .Index(t => t.Manufacturer_Id)
                .Index(t => t.Proxy_Id);
            
            CreateTable(
                "dbo.Proxies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IPAddress = c.String(),
                        Port = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Queues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageNo = c.Int(nullable: false),
                        IsBusy = c.Boolean(nullable: false),
                        Manufacturer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id)
                .Index(t => t.Manufacturer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queues", "Manufacturer_Id", "dbo.Manufacturers");
            DropForeignKey("dbo.ApiSearchSessions", "Proxy_Id", "dbo.Proxies");
            DropForeignKey("dbo.ApiSearchSessions", "Manufacturer_Id", "dbo.Manufacturers");
            DropForeignKey("dbo.AlternatePackagings", "Good_Id", "dbo.Goods");
            DropForeignKey("dbo.ProductCompliances", "Good_Id", "dbo.Goods");
            DropForeignKey("dbo.ProductAttributes", "Good_Id", "dbo.Goods");
            DropForeignKey("dbo.PriceBreaks", "Good_Id", "dbo.Goods");
            DropForeignKey("dbo.Goods", "Manufacturer_Id", "dbo.Manufacturers");
            DropForeignKey("dbo.Goods", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Manufacturer_Id", "dbo.Manufacturers");
            DropIndex("dbo.Queues", new[] { "Manufacturer_Id" });
            DropIndex("dbo.ApiSearchSessions", new[] { "Proxy_Id" });
            DropIndex("dbo.ApiSearchSessions", new[] { "Manufacturer_Id" });
            DropIndex("dbo.ProductCompliances", new[] { "Good_Id" });
            DropIndex("dbo.ProductAttributes", new[] { "Good_Id" });
            DropIndex("dbo.PriceBreaks", new[] { "Good_Id" });
            DropIndex("dbo.Categories", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Goods", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Goods", new[] { "Category_Id" });
            DropIndex("dbo.AlternatePackagings", new[] { "Good_Id" });
            DropTable("dbo.Queues");
            DropTable("dbo.Proxies");
            DropTable("dbo.ApiSearchSessions");
            DropTable("dbo.ProductCompliances");
            DropTable("dbo.ProductAttributes");
            DropTable("dbo.PriceBreaks");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Categories");
            DropTable("dbo.Goods");
            DropTable("dbo.AlternatePackagings");
        }
    }
}
