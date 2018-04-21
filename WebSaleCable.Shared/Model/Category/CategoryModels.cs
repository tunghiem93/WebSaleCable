using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Shared.Model.Category
{
    public class CategoryModels
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập thông tin!")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public int Total { get; set; }
        public CategoryModels()
        {
            IsActive = true;
        }
    }
}
