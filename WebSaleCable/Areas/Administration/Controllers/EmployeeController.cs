using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared.Factory.EmployeeFactory;
using WebSaleCable.Shared.Model.Employee;
using WebSaleCable.Web.App_Start;

namespace WebSaleCable.Areas.Administration.Controllers
{
    [NuAuth]
    public class EmployeeController : BaseController
    {
        private EmployeeFactory _factory = null;
        // GET: Administration/Employee
        public EmployeeController()
        {
            _factory = new EmployeeFactory();
        }

        public ActionResult Index()
        {
            EmployeeViewModels model = new EmployeeViewModels();
            return View(model);
        }

        public ActionResult Search(EmployeeViewModels model)
        {
            try
            {
                var data = _factory.GetListEmployee();
                model.ListEmployee = data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListEmployee: ", e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        public EmployeeModels GetDetail(string id)
        {
            try
            {
                EmployeeModels model = _factory.GetDetailEmployee(id);
                model.Password = CommonHelper.Decrypt(model.Password);
                model.ConfirmPassword = CommonHelper.Decrypt(model.ConfirmPassword);
                return model;
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetailEmployee: ", ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            EmployeeModels model = new EmployeeModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EmployeeModels model)
        {
            try
            {
                if (!model.Password.Trim().ToLower().Equals(model.ConfirmPassword.ToString().Trim().ToLower()))
                {
                    ModelState.AddModelError("ConfirmPassword", "Làm ơn xác nhận lại mật khẩu!");
                }
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("Create", model);
                }
                string msg = "";
                model.CreatedUser = CurrentUser.UserId;
                model.Password = CommonHelper.Encrypt(model.Password);
                var result = _factory.InsertEmployee(model, ref msg);
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
                NSLog.Logger.Error("EmployeeCreate: ", ex);
                ModelState.AddModelError("Email", "Có một lỗi xảy ra!");
                model.Password = CommonHelper.Decrypt(model.Password);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("Create", model);
            }
        }

        [HttpGet]
        public new PartialViewResult View(string id)
        {
            EmployeeModels model = GetDetail(id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public PartialViewResult Edit(string id)
        {
            EmployeeModels model = GetDetail(id);
            model.ConfirmPassword = model.Password;
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModels model)
        {
            try
            {
                if (!model.Password.Trim().ToLower().Equals(model.ConfirmPassword.ToString().Trim().ToLower()))
                {
                    ModelState.AddModelError("ConfirmPassword", "Làm ơn xác nhận lại mật khẩu!");
                }
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                string msg = "";
                model.CreatedUser = CurrentUser.UserId;
                model.Password = CommonHelper.Encrypt(model.Password);
                var result = _factory.UpdateEmployee(model, ref msg);
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
                NSLog.Logger.Error("Employee_Edit: ", ex);
                ModelState.AddModelError("Email", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public PartialViewResult Delete(string id)
        {
            EmployeeModels model = GetDetail(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(EmployeeModels model)
        {
            try
            {
                string msg = "";
                var result = _factory.DeleteEmployee(model.ID, ref msg);
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
                NSLog.Logger.Error("Xóa nhân viên: ", ex);
                ModelState.AddModelError("Name", ("Lỗi khi xóa thông tin nhân viên!"));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}