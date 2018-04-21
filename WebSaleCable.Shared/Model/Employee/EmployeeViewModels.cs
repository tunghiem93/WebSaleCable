using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Shared.Model.Employee
{
    public class EmployeeViewModels
    {
        public List<EmployeeModels> ListEmployee { get; set; }
        public EmployeeViewModels()
        {
            ListEmployee = new List<EmployeeModels>();
        }
    }
}
