using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebSaleCable.Shared.Model.Product
{
    public class ProductViewModels
    {
        public List<ProductModels> ListProduct { get; set; }
        public string CateID { get; set; }
        public List<SelectListItem> ListCate { get; set; }
        public int TotalProduct { get; set; }
        public ProductViewModels()
        {
            ListProduct = new List<ProductModels>();
        }
    }
}
