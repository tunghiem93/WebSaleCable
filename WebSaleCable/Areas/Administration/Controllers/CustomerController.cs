using ProjectWebSaleLand.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared.Factory.CustomerFactory;
using WebSaleCable.Shared.Model.Customer;
using WebSaleCable.Web.App_Start;

namespace WebSaleCable.Areas.Administration.Controllers
{
    [NuAuth]
    public class CustomerController : BaseController
    {
        private CustomerFactory _factory = null;
        // GET: Administration/Customer
        public CustomerController()
        {
            _factory = new CustomerFactory();
        }

        public ActionResult Index()
        {
            CustomerViewModels model = new CustomerViewModels();
            return View(model);
        }

        public ActionResult Search(CustomerViewModels model)
        {
            try
            {
                var data = _factory.GetListCustomer();
                model.ListCus = data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListCustomer: ", e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        public CustomerModels GetDetail(string id)
        {
            try
            {
                CustomerModels model = _factory.GetDetailCustomer(id);
                model.Password = CommonHelper.Decrypt(model.Password);
                model.ConfirmPassword = model.Password;
                return model;
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetailCustomer: ", ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            CustomerModels model = new CustomerModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CustomerModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("Create", model);
                }
                string msg = "", cusId = "";
                model.CreatedUser = CurrentUser.UserId;
                model.Password = CommonHelper.Encrypt(model.Password);
                var result = _factory.InsertCustomer(model, ref msg, ref cusId);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Email", msg);
                    model.Password = CommonHelper.Decrypt(model.Password);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("Create", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("CustomerCreate: ", ex);
                ModelState.AddModelError("Email", "Có một lỗi xảy ra!");
                model.Password = CommonHelper.Decrypt(model.Password);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("Create", model);
            }
        }

        [HttpGet]
        public new PartialViewResult View(string id)
        {
            CustomerModels model = GetDetail(id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public PartialViewResult Edit(string id)
        {
            CustomerModels model = GetDetail(id);
            model.ConfirmPassword = model.Password;
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerModels model)
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
                model.Password = CommonHelper.Encrypt(model.Password);
                var result = _factory.UpdateCustomer(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    model = GetDetail(model.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        ModelState.AddModelError("Email", msg);
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Customer_Edit: ", ex);
                ModelState.AddModelError("Email", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public PartialViewResult Delete(string id)
        {
            CustomerModels model = GetDetail(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CustomerModels model)
        {
            try
            {
                string msg = "";
                var result = _factory.DeleteCustomer(model.ID, ref msg);
                if (!result)
                {
                    ModelState.AddModelError("Name", msg);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Xóa khách hàng: ", ex);
                ModelState.AddModelError("Name", ("Lỗi khi xóa thông tin khách hàng!"));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}