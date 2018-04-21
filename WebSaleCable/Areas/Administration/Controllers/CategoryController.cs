using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Administration/Category
        public ActionResult Index()
        {
            return View();
        }
    }
}