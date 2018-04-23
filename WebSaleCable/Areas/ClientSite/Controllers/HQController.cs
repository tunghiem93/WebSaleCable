using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebSaleCable.Shared;
using WebSaleCable.Shared.Factory.CategoryFactory;
using WebSaleCable.Shared.Factory.LocationFactory;
using WebSaleCable.Shared.Model;
using WebSaleCable.Shared.Model.Product;

namespace WebSaleCable.Areas.ClientSite.Controllers
{
    public class HQController : Controller
    {
       public HQController()
       {
            if (System.Web.HttpContext.Current.Session["SliderSession"] == null)
            {
                var _Path = HostingEnvironment.MapPath("~/Images/silder/");
                var list = Directory.GetFiles(_Path).Select(x => Path.GetFileName(x)).ToList();
                var ListSlider = new List<SliderSession>();
                if (list != null && list.Count > 0)
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        ListSlider.Add(new SliderSession
                        {
                            ImageUrl = list[i]
                        });
                    }
                }
                System.Web.HttpContext.Current.Session["SliderSession"] = ListSlider;
            }
       }
        public List<ItemLocation> getListLocation()
        {
            List<ItemLocation> ListLoca = new List<ItemLocation>();
            try
            {
                ListLoca = new List<ItemLocation>() {
                    new ItemLocation(){ ID = Commons.ELocation.KD1.ToString("d"), Name = "KD 1", },
                    new ItemLocation(){ ID = Commons.ELocation.KD2.ToString("d"), Name = "KD 2", },
                    new ItemLocation(){ ID = Commons.ELocation.KD3.ToString("d"), Name = "KD 3", },
                };
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("getListLocation", ex);
            }
            return ListLoca;
        }

        public List<ItemCategory> getListCategory()
        {
            List<ItemCategory> ListCate = new List<ItemCategory>();
            try
            {
                ListCate = new List<ItemCategory>() {
                    new ItemCategory(){ ID = Commons.ECategory.CapSacLighting.ToString("d"), Name = "Cáp sạc lighting", },
                    new ItemCategory(){ ID = Commons.ECategory.CapSacMicroUSB.ToString("d"), Name = "Cáp sạc micro USB", },
                    new ItemCategory(){ ID = Commons.ECategory.CapSacNhanh3Trong1.ToString("d"), Name = "Cáp sạc nhanh 3 trong 1", },
                    new ItemCategory(){ ID = Commons.ECategory.CapSacTypeC.ToString("d"), Name = "Cáp sạc Type C", },
                    new ItemCategory(){ ID = Commons.ECategory.CuSacNhanh.ToString("d"), Name = "Củ sạc nhanh", },
                    new ItemCategory(){ ID = Commons.ECategory.CuSacThuong.ToString("d"), Name = "Củ sạc thường", },
                    new ItemCategory(){ ID = Commons.ECategory.PinDuPhong.ToString("d"), Name = "Pin dự phòng", },
                };
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("getListCategory", ex);
            }
            return ListCate;
        }
    }
}