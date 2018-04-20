using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class ProductController : Controller
    {
        // GET: Administration/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}