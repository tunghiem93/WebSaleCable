using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebSaleCable.Shared;
using WebSaleCable.Shared.Factory.CategoryFactory;
using WebSaleCable.Shared.Factory.LocationFactory;

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

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpCookie _LocationCookie = Request.Cookies["LocCookie"];
            if (_LocationCookie != null)
            {
                var input = Server.UrlDecode(_LocationCookie.Value);
                if(input != null && input.Length > 0)
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
                var _loc = _facLoc.GetListLocation().Select(x=> new LocationSession {
                        Id = x.ID,
                        Name = x.Name
                }).ToList();
                if(_loc != null && _loc.Any())
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
                    if (cateSession != null && HttpContext.Current.Session != null)
                    {
                        cateSession = cateSession.OrderBy(x=>x.Name).Skip(0).Take(8).ToList();
                        Session.Add("Catelogies", cateSession);
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
                }).OrderBy(x=>x.Name).Skip(0).Take(8).ToList();
                if (_cate != null && _cate.Any())
                {
                    string myObjectJson = JsonConvert.SerializeObject(_cate);  //new JavaScriptSerializer().Serialize(userSession);
                    HttpCookie cookie = new HttpCookie("CateCookie");
                    cookie.Expires = DateTime.Now.AddYears(10);
                    cookie.Value = Server.UrlEncode(myObjectJson);
                    Response.Cookies.Add(cookie);
                    Session.Add("Catelogies", _cate);
                }
            }
        }
    }
}
