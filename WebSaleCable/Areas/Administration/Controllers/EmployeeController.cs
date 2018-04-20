using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Administration/Employee
        public ActionResult Index()
        {
            return View();
        }
    }
}