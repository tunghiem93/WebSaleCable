using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebSaleCable.Shared;
using WebSaleCable.Shared.Factory.CategoryFactory;
using WebSaleCable.Shared.Factory.LocationFactory;
using WebSaleCable.Shared.Models;

namespace WebSaleCable
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpCookie _LocationCookie = Request.Cookies["LocCookie"];
            if (_LocationCookie != null)
            {
                var input = Server.UrlDecode(_LocationCookie.Value);
                if (input != null && input.Length > 0)
                {
                    List<LocationSession> locSession = JsonConvert.DeserializeObject<List<LocationSession>>(input); //new JavaScriptSerializer().Deserialize<UserSession>(input);
                    if (locSession != null && HttpContext.Current.Session != null)
                    {
                        Session.Add("Location", locSession);
                    }
                }

            }
            else
            {
                LocationFactory _facLoc = new LocationFactory();
                var _loc = _facLoc.GetListLocation().Select(x => new LocationSession
                {
                    Id = x.ID,
                    Name = x.Name
                }).ToList();
                if (_loc != null && _loc.Any())
                {
                    string myObjectJson = JsonConvert.SerializeObject(_loc);  //new JavaScriptSerializer().Serialize(userSession);
                    HttpCookie cookie = new HttpCookie("LocCookie");
                    cookie.Expires = DateTime.Now.AddYears(10);
                    cookie.Value = Server.UrlEncode(myObjectJson);
                    Response.Cookies.Add(cookie);
                    Session.Add("Location", _loc);
                }
            }


            HttpCookie _CateCookie = Request.Cookies["CateCookie"];
            if (_CateCookie != null)
            {
                var input = Server.UrlDecode(_CateCookie.Value);
                if (input != null && input.Length > 0)
                {
                    List<CateSession> cateSession = JsonConvert.DeserializeObject<List<CateSession>>(input); //new JavaScriptSerializer().Deserialize<UserSession>(input);
                    var ObjSession = new CateSession();
                    if (cateSession != null && HttpContext.Current.Session != null)
                    {
                        ObjSession.MainCate = cateSession.OrderBy(x => x.Name).Skip(0).Take(7).ToList();
                        ObjSession.OrtherCate = cateSession.OrderBy(x => x.Name).Skip(7).Take(cateSession.Count).ToList();
                        // Session.Add("Catelogies", cateSession);
                        Session.Add("Catelogies", ObjSession);
                    }
                }

            }
            else
            {
                CategoryFactory _facLoc = new CategoryFactory();
                var _cate = _facLoc.GetListCategory().Select(x => new CateSession
                {
                    Id = x.ID,
                    Name = x.Name
                }).ToList();//.OrderBy(x => x.Name).Skip(0).Take(7).ToList();
                if (_cate != null && _cate.Any())
                {
                    var ObjSession = new CateSession();
                    ObjSession.MainCate = _cate.OrderBy(x => x.Name).Skip(0).Take(7).ToList();
                    ObjSession.OrtherCate = _cate.OrderBy(x => x.Name).Skip(7).Take(_cate.Count).ToList();

                    string myObjectJson = JsonConvert.SerializeObject(_cate);  //new JavaScriptSerializer().Serialize(userSession);
                    HttpCookie cookie = new HttpCookie("CateCookie");
                    cookie.Expires = DateTime.Now.AddYears(10);
                    cookie.Value = Server.UrlEncode(myObjectJson);
                    Response.Cookies.Add(cookie);
                    //  Session.Add("Catelogies", _cate);
                    Session.Add("Catelogies", ObjSession);
                }
            }


            HttpCookie _UserClientCookie = Request.Cookies["UserClientCookie"];
            if (_UserClientCookie != null)
            {
                var input = Server.UrlDecode(_UserClientCookie.Value);
                UserSession userSession = JsonConvert.DeserializeObject<UserSession>(input); //new JavaScriptSerializer().Deserialize<UserSession>(input);
                if (userSession != null && HttpContext.Current.Session != null)
                {
                    Session.Add("UserClient", userSession);
                }
            }
        }
    }
}
