namespace WebSaleCable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable10072304 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DbCategories", "Type");
            DropColumn("dbo.DbLocations", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbLocations", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.DbCategories", "Type", c => c.Int(nullable: false));
        }
    }
}
