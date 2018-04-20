using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaleCable.Data.Entities;

namespace WebSaleCable.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name = CompanyConnectionString")
        {

            // Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
        }

        public DbSet<DbEmployee> dbEmployee
        {
            get;
            set;
        }
        public DbSet<DbCustomer> dbCustomer
        {
            get;
            set;
        }
        public DbSet<DbCategory> dbCategory
        {
            get;
            set;
        }
        public DbSet<DbProduct> dbProduct
        {
            get;
            set;
        }
        public DbSet<DbImage> dbImage
        {
            get;
            set;
        }
    }
}
