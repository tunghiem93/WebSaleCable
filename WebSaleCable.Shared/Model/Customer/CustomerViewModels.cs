using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Shared.Model.Customer
{
    public class CustomerViewModels
    {
        public List<CustomerModels> ListCus { get; set; }
        public CustomerViewModels()
        {
            ListCus = new List<CustomerModels>();
        }
    }
}
