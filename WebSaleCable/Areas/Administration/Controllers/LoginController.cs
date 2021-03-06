﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebSaleCable.Shared.Factory;
using WebSaleCable.Shared.Models;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class LoginController : Controller
    {
        // GET: Administration/Login
        private static string mController = "";
        public LoginController()
        {

        }

        // GET: Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(bool isAjax = false, string returnUrl = null)
        {
            if (Session["User"] != null)
                return RedirectToAction("Index", "Home", new { area = "Administration" });

            LoginRequestModel model = new LoginRequestModel();

            if (returnUrl != null)
            {
                var questionMarkIndex = returnUrl.IndexOf('?');
                string queryString = null;
                string url = returnUrl;
                if (questionMarkIndex != -1) // 
                {
                    url = returnUrl.Substring(0, questionMarkIndex);
                    queryString = returnUrl.Substring(questionMarkIndex + 1);
                }

                // Arranges
                var request = new HttpRequest(null, url, queryString);
                var response = new HttpResponse(new StringWriter());
                var httpContext = new HttpContext(request, response);

                var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

                // Extract the data    
                var values = routeData.Values;
                mController = values["controller"].ToString();
                var actionName = values["action"];
                var areaName = values["area"];
            }




            if (isAjax)
                //return PartialView("_Login", model);
                return RedirectToAction("Index", "Login", new { area = "Administration" });
            else
                return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Shared.Models.LoginRequestModel model, string returnUrl = null)
        {
            try
            {
                if (Session["User"] != null)
                    return RedirectToAction("Index", "Home", new { area = "Administration" });

                if (ModelState.IsValid)
                {
                    UserFactory factoy = new UserFactory();

                    LoginResponseModel User = UserFactory.Instance.Login(model);

                    bool isValid = (User != null && !string.IsNullOrEmpty(User.EmployeeID));
                    if (isValid)
                    {
                        UserSession userSession = new UserSession();
                        userSession.UserId = User.EmployeeID;
                        userSession.Email = User.EmployeeEmail;
                        userSession.UserName = User.EmployeeName;
                        userSession.IsAuthenticated = true;
                        userSession.ImageUrl = User.EmployeeImageURL;
                        userSession.IsSuperAdmin = User.IsSupperAdmin;
                        userSession.RememberMe = model.RememberMe;

                        Session.Add("User", userSession);
                        //
                        if (!string.IsNullOrEmpty(mController))
                            return RedirectToAction("Index", mController, new { area = "Administration" });
                        if (returnUrl == null)
                            return RedirectToAction("Index", "Home", new { area = "Administration" });
                        else
                            return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                        return View(model);
                    }
                }
                else
                {
                    return View(model);// Return Error page
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}