namespace WebSaleCable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb2304 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbLocations",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.DbProducts", "LocationID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbProducts", "LocationID");
            DropTable("dbo.DbLocations");
        }
    }
}
