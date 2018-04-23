namespace WebSaleCable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateColumn2304 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DbProducts", "State", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DbProducts", "State", c => c.Boolean(nullable: false));
        }
    }
}
