namespace WebSaleCable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbCategories", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.DbLocations", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbLocations", "Type");
            DropColumn("dbo.DbCategories", "Type");
        }
    }
}
