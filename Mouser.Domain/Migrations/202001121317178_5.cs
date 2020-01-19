namespace Mouser.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "SuggestedReplacement", c => c.String());
            AddColumn("dbo.Goods", "MultiSimBlue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "MultiSimBlue");
            DropColumn("dbo.Goods", "SuggestedReplacement");
        }
    }
}
