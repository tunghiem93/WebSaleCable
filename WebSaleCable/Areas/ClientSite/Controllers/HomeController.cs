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
            if(model.ListProduct != null && model.ListProduct.Any())
            {
                model.TotalProduct = model.ListProduct.Count;
                model.ListProduct = model.ListProduct.OrderByDescending(x => x.CreatedDate).Skip(0).Take(6).ToList();
            }
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
            if (!string.IsNullOrEmpty(id) && id.Equals("1"))
            {
                model.ListProduct = _fac.GetListProduct().ToList();
                model.ListProduct.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                if (model.ListProduct != null && model.ListProduct.Any())
                {
                    model.TotalProduct = model.ListProduct.Count;
                }
                model.IsAddMore = true;
            }
            else
            {
                
                model.ListProduct = _fac.GetListProduct().Where(x => x.CategoryID.Equals(id)).ToList();
                model.ListProduct.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                if(model.ListProduct != null && model.ListProduct.Any())
                {
                    model.TotalProduct = model.ListProduct.Count;
                    model.ListProduct = model.ListProduct.OrderByDescending(x => x.CreatedDate).Skip(0).Take(6).ToList();
                }
            }
            return PartialView("_ListItem", model);
        }
    }
}