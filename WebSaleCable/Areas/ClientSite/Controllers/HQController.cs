using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebSaleCable.Shared.Factory.CategoryFactory;
using WebSaleCable.Shared.Factory.LocationFactory;
using WebSaleCable.Shared.Model;

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
        public List<SelectListItem> getListLocation()
        {
            var _lstLocation = new List<SelectListItem>();
            try
            {
                LocationFactory _factory = new LocationFactory();
                var data = _factory.GetListLocation();
                data = data.OrderBy(o => o.Name).ToList();
                foreach (var item in data)
                {
                    _lstLocation.Add(new SelectListItem
                    {
                        Value = item.ID,
                        Text = item.Name
                    });
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("getListLocation", ex);
            }
            return _lstLocation;
        }

        public List<SelectListItem> getListCategory()
        {
            var _lstCate = new List<SelectListItem>();
            try
            {
                CategoryFactory _factory = new CategoryFactory();
                var data = _factory.GetListCategory();
                data = data.OrderBy(o => o.Name).ToList();
                foreach (var item in data)
                {
                    _lstCate.Add(new SelectListItem
                    {
                        Value = item.ID,
                        Text = item.Name
                    });
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("getListCategory", ex);
            }
            return _lstCate;
        }
    }
}