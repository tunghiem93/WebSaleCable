using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSaleCable.Areas.ClientSite.Controllers
{
    public class HomeController : HQController
    {
        // GET: ClientSite/Home
        public ActionResult Index()
        {
            ViewBag.Location = getListLocation();
            ViewBag.Category = getListCategory();
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpGet]
        public new PartialViewResult LoadItem(string id)
        {
            return PartialView("_ListItem");
        }
    }
}