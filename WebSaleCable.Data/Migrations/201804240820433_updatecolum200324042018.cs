namespace WebSaleCable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecolum200324042018 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DbProducts", "GuaranteePeriod", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DbProducts", "GuaranteePeriod", c => c.Double(nullable: false));
        }
    }
}
