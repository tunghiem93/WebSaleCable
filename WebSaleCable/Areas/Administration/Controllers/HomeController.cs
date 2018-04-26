using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSaleCable.Web.App_Start;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class HomeController : BaseController
    {
        [NuAuth]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                if (Session["User"] == null)
                    return RedirectToAction("Login", new { area = "Administration" });

                FormsAuthentication.SignOut();
                Session.Remove("User");

                return RedirectToAction("Index", "Home", new { area = "Administration" });
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Logout Error: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}