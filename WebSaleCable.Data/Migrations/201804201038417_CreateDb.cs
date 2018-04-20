namespace WebSaleCable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbCategories",
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
            
            CreateTable(
                "dbo.DbCustomers",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        Gender = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        MaritalStatus = c.Boolean(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        WebSite = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DbEmployees",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Address = c.String(),
                        MaritalStatus = c.Boolean(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        IsSupperAdmin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DbImages",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        ProductID = c.String(),
                        ImageUrL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DbProducts",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CategoryID = c.String(),
                        Type = c.Int(nullable: false),
                        Price = c.String(),
                        GuaranteePeriod = c.Double(nullable: false),
                        Description = c.String(),
                        Production = c.String(),
                        Code = c.String(),
                        State = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Color = c.String(),
                        Length = c.String(),
                        Width = c.String(),
                        Weight = c.String(),
                        MoreInformation = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DbProducts");
            DropTable("dbo.DbImages");
            DropTable("dbo.DbEmployees");
            DropTable("dbo.DbCustomers");
            DropTable("dbo.DbCategories");
        }
    }
}
