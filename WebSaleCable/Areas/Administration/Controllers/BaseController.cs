﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared.Models;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class BaseController : HQController
    {
        // GET: Administration/Base
        public string List { get; set; }
        // GET: Base
        //public UserSession CurrentUser
        //{
        //    get
        //    {
        //        if (System.Web.HttpContext.Current.Session["User"] != null)
        //            return (UserSession)System.Web.HttpContext.Current.Session["User"];
        //        else
        //            return new UserSession();
        //    }
        //}

        public BaseController()
        {
            ViewBag.Location = getListLocation();
            ViewBag.Category = getListCategory();
            var user = System.Web.HttpContext.Current.Session["User"] as UserSession;
        }
    }
}