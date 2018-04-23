using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared.Model.Product;

namespace WebSaleCable.Areas.ClientSite.Controllers
{
    public class HomeController : HQController
    {
        // GET: ClientSite/Home
        public ActionResult Index()
        {
            ProductViewModels model = new ProductViewModels();
            model.ListLoca = getListLocation();
            model.ListCate = getListCategory();
            return View(model);
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