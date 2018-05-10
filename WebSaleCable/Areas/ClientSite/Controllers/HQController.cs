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
}