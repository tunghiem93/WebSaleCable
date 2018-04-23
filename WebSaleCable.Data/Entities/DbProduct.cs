using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Data.Entities
{
    public class DbProduct
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CategoryID { get; set; }
        public string LocationID { get; set; }
        public int Type { get; set; }        
        public string Price { get; set; }
        public double GuaranteePeriod { get; set; }       
        public string Description { get; set; }    
        public string Production { get; set; }
        public string Code { get; set; }
        public bool State { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Weight { get; set; }
        public string MoreInformation { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
    }
}
