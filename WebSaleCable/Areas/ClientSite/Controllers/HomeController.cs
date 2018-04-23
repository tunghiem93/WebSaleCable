using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared;
using WebSaleCable.Shared.Factory.ProductFactory;
using WebSaleCable.Shared.Model.Product;

namespace WebSaleCable.Areas.ClientSite.Controllers
{
    public class HomeController : HQController
    {
        private ProductFactory _fac;
        public HomeController()
        {
            _fac = new ProductFactory();
        }
        // GET: ClientSite/Home
        public ActionResult Index()
        {
            ProductViewModels model = new ProductViewModels();
            model.ListProduct = _fac.GetListProduct();
            model.ListProduct.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + x.ImageURL;
            });
            //model.ListLoca = getListLocation();
            //model.ListCate = getListCategory();
            return View(model);
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpGet]
        public new PartialViewResult LoadItem(string id)
        {
            ProductViewModels model = new ProductViewModels();
            model.ListProduct = _fac.GetListProduct().Where(x=>x.CategoryID.Equals(id)).ToList();
            model.ListProduct.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + x.ImageURL;
            });
            return PartialView("_ListItem",model.ListProduct);
        }
    }
}