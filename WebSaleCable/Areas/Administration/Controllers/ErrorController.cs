﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared.Models;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Administration/Error
        public ActionResult Index()
        {
            ErrorModel model = new ErrorModel();
            Exception e = Server.GetLastError();
            if (e.GetType() == typeof(HttpException))
            {
                model.StatusCode = ((HttpException)e).GetHttpCode();
            }
            else
            {
                // Not an HTTP related error so this is a problem in our code, set status to
                // 500 (internal server error)
                model.StatusCode = 500;
            }
            model.Message = e.Message;

            return View(model);
        }

        public ActionResult Unauthorised()
        {
            return PartialView();
        }

        public ViewResult NotFound()
        {
            //Response.StatusCode = 404;  //you may want to set this to 200
            return View();
        }

        public ViewResult TimeOutSession()
        {
            return View();
        }
    }
}