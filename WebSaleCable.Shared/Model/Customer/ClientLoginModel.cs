using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Shared.Model.Customer
{
    public class ClientLoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
       // [Display(Name = "Vui lòng nhập Username")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
      //  [Display(Name = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }

        public ClientLoginModel()
        {
            IsRemember = true;
        }
    }
}
