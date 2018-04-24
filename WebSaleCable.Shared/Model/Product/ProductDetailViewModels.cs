using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Shared.Model.Product
{
    public class ProductDetailViewModels
    {
        public ProductModels Product { get; set; }
        public List<ProductModels> ListProduct { get; set; }
        public string KeyWord { get; set; }
    }
}
