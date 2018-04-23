using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebSaleCable.Shared.Model.Product;

namespace WebSaleCable.Shared.Model
{
    public class SliderSession
    {
        public string ImageUrl { get; set; }
        public List<ImageProduct> ListImg { get; set; }
        public SliderSession()
        {
            ListImg = new List<ImageProduct>();
        }
    }
}
