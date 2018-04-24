using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebSaleCable.Shared.Model.Product
{
    public class ProductModels : BaseModels
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập tên!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập chọn thể loại!")]
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập chọn khu vực!")]
        public string LocationID { get; set; }
        public string LocationName { get; set; }
        public int Type { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string GuaranteePeriod { get; set; }
        public string Description { get; set; }
        public string Production { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string Color { get; set; }
        public string Weight { get; set; }
        public string MoreInformation { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        //Image
        public List<string> ListImageUrl { get; set; }
        public List<ImageProduct> ListImg { get; set; }
        public string RawImageUrl { get; set; }
        public ProductModels()
        {
            IsActive = true;
            ListImageUrl = new List<string>();
            ListImg = new List<ImageProduct>();
        }
    }
    public class ImageProduct : BaseModels
    {
        public int OffSet { get; set; }
        public bool IsDelete { get; set; }
    }
    public class ItemCategory
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Selected { get; set; }

    }
    public class ItemLocation 
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Selected { get; set; }
    }
}
