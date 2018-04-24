using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared;
using WebSaleCable.Shared.Factory.CategoryFactory;
using WebSaleCable.Shared.Model.Category;
using WebSaleCable.Web.App_Start;

namespace WebSaleCable.Areas.Administration.Controllers
{
    [NuAuth]
    public class CategoryController : BaseController
    {
        private CategoryFactory _factory = null;
        // GET: Administration/Category
        public CategoryController()
        {
            _factory = new CategoryFactory();
        }

        public ActionResult Index()
        {
            CategoryViewModels model = new CategoryViewModels();
            return View(model);
        }

        public ActionResult Search(CategoryViewModels model)
        {
            try
            {
                var data = _factory.GetListCategory();
                model.ListCate = data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListCategory: ", e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        public CategoryModels GetDetail(string id)
        {
            try
            {
                CategoryModels model = _factory.GetDetailCategory(id);
                return model;
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetailCategory: ", ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryModels model = new CategoryModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("Create", model);
                }
                string msg = "";
                model.CreatedUser = CurrentUser.UserId;
                var result = _factory.InsertCategory(model, ref msg);
                if (result)
                {
                    HttpCookie _LocationCookie = Request.Cookies["CateCookie"];
                    //if (_LocationCookie == null)
                    //{
                    //    HttpCookie cookie = new HttpCookie("CateCookie");
                    //    cookie.Expires = DateTime.Now.AddYears(10);
                    //}
                    CategoryFactory _facLoc = new CategoryFactory();
                    var _loc = _facLoc.GetListCategory().Select(x => new CateSession
                    {
                        Id = x.ID,
                        Name = x.Name
                    }).ToList();
                    if (_loc != null && _loc.Any())
                    {
                        string myObjectJson = JsonConvert.SerializeObject(_loc);  //new JavaScriptSerializer().Serialize(userSession);
                        HttpCookie cookie = new HttpCookie("CateCookie");
                        cookie.Expires = DateTime.Now.AddYears(10);
                        cookie.Value = Server.UrlEncode(myObjectJson);
                        Response.Cookies.Add(cookie);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", msg);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("Create", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("CategoryCreate: ", ex);
                ModelState.AddModelError("Name", "Có một lỗi xảy ra!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("Create", model);
            }
        }

        [HttpGet]
        public new PartialViewResult View(string id)
        {
            CategoryModels model = GetDetail(id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public PartialViewResult Edit(string id)
        {
            CategoryModels model = GetDetail(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                string msg = "";
                model.CreatedUser = CurrentUser.UserId;
                var result = _factory.UpdateCategory(model, ref msg);
                if (result)
                {
                    HttpCookie _LocationCookie = Request.Cookies["CateCookie"];
                    //if (_LocationCookie == null)
                    //{
                    //    HttpCookie cookie = new HttpCookie("CateCookie");
                    //    cookie.Expires = DateTime.Now.AddYears(10);
                    //}
                    CategoryFactory _facLoc = new CategoryFactory();
                    var _loc = _facLoc.GetListCategory().Select(x => new CateSession
                    {
                        Id = x.ID,
                        Name = x.Name
                    }).ToList();
                    if (_loc != null && _loc.Any())
                    {
                        string myObjectJson = JsonConvert.SerializeObject(_loc);  //new JavaScriptSerializer().Serialize(userSession);
                        HttpCookie cookie = new HttpCookie("CateCookie");
                        cookie.Expires = DateTime.Now.AddYears(10);
                        cookie.Value = Server.UrlEncode(myObjectJson);
                        Response.Cookies.Add(cookie);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    model = GetDetail(model.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        ModelState.AddModelError("Name", msg);
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Category_Edit: ", ex);
                ModelState.AddModelError("Name", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public PartialViewResult Delete(string id)
        {
            CategoryModels model = GetDetail(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CategoryModels model)
        {
            try
            {
                string msg = "";
                var result = _factory.DeleteCategory(model.ID, ref msg);
                if (!result)
                {
                    ModelState.AddModelError("Name", msg);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                HttpCookie _LocationCookie = Request.Cookies["CateCookie"];
                CategoryFactory _facLoc = new CategoryFactory();
                var _loc = _facLoc.GetListCategory().Select(x => new CateSession
                {
                    Id = x.ID,
                    Name = x.Name
                }).ToList();
                if (_loc != null && _loc.Any())
                {
                    string myObjectJson = JsonConvert.SerializeObject(_loc);  //new JavaScriptSerializer().Serialize(userSession);
                    HttpCookie cookie = new HttpCookie("CateCookie");
                    cookie.Expires = DateTime.Now.AddYears(10);
                    cookie.Value = Server.UrlEncode(myObjectJson);
                    Response.Cookies.Add(cookie);
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Xóa thể loại: ", ex);
                ModelState.AddModelError("Name", ("Lỗi khi xóa thông tin thể loại!"));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}