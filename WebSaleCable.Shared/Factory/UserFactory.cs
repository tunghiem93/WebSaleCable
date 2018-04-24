using WebSaleCable.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaleCable.Data;
using WebSaleCable.Shared.Models;

namespace WebSaleCable.Shared.Factory
{
    public class UserFactory
    {
        protected static UserFactory _instance;
        public static UserFactory Instance
        {
            get
            {
                _instance = _instance != null ? _instance : new UserFactory();
                return _instance;
            }
        }
        public LoginResponseModel Login(LoginRequestModel info)
        {
            NSLog.Logger.Info("Employee Login Start", info);
            LoginResponseModel user = null;
            try
            {
                using (DataContext _db = new DataContext())
                {
                    info.Password = CommonHelper.Encrypt(info.Password);
                    string serverImage = ConfigurationManager.AppSettings["PublicImages"];

                    var emp = _db.dbEmployee.Where(o => o.Email == info.Email.ToLower().Trim() && o.Password == info.Password).FirstOrDefault();
                    if (emp != null)
                    {
                        user = new LoginResponseModel()
                        {
                            EmployeeID = emp.ID,
                            EmployeeName = emp.Name,
                            EmployeeEmail = emp.Email,
                            //EmployeeImageURL = string.IsNullOrEmpty(emp.ImageURL) ? "" : serverImage + emp.ImageURL,
                            IsSupperAdmin = emp.IsSupperAdmin,
                        };
                    }
                    NSLog.Logger.Info("Employee Login Done", user);
                }
            }
            catch (Exception ex) { NSLog.Logger.Error("Employee Login Error", ex); }
            return user;
        }
    }
}
