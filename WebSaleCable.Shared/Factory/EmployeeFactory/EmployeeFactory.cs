using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaleCable.Data;
using WebSaleCable.Data.Entities;
using WebSaleCable.Shared.Model.Employee;

namespace WebSaleCable.Shared.Factory.EmployeeFactory
{
    public class EmployeeFactory
    {
        public List<EmployeeModels> GetListEmployee()
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var lstResult = cxt.dbEmployee.Select(o => new EmployeeModels()
                    {
                        ID = o.ID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        // Name = o.Name,
                        Phone = o.Phone,
                        Email = o.Email,
                        Password = o.Password,
                        Country = o.Country,
                        BirthDate = o.BirthDate,
                        Address = o.Address,
                        Gender = o.Gender,
                        MaritalStatus = o.MaritalStatus,
                        IsSupperAdmin = o.IsSupperAdmin,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDate,
                        CreatedUser = o.CreatedUser,
                        ModifiedDate = o.ModifiedDate,
                        ModifiedUser = o.ModifiedUser,
                    }).ToList();
                    return lstResult;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListEmployee_Fail", ex);
                    return null;
                }
            }
        }

        public EmployeeModels GetDetailEmployee(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var result = cxt.dbEmployee.Where(w => w.ID == id).Select(o => new EmployeeModels()
                    {
                        ID = o.ID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        //  Name = o.Name,
                        Phone = o.Phone,
                        Email = o.Email,
                        Password = o.Password,
                        Country = o.Country,
                        BirthDate = o.BirthDate,
                        Address = o.Address,
                        Gender = o.Gender,
                        MaritalStatus = o.MaritalStatus,
                        IsSupperAdmin = o.IsSupperAdmin,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDate,
                        CreatedUser = o.CreatedUser,
                        ModifiedDate = o.ModifiedDate,
                        ModifiedUser = o.ModifiedUser,
                        ConfirmPassword = o.Password
                    }).FirstOrDefault();
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetDetailEmployee_Fail", ex);
                    return null;
                }
            }
        }

        public bool InsertEmployee(EmployeeModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                var _isExits = cxt.dbEmployee.Any(x => x.Email.Equals(model.Email) && x.IsActive);
                if (_isExits)
                {
                    result = false;
                    msg = "Địa chỉ email đã tồn tại";
                }
                else
                {
                    using (var transaction = cxt.Database.BeginTransaction())
                    {
                        try
                        {
                            DbEmployee item = new DbEmployee();
                            string id = Guid.NewGuid().ToString();
                            item.ID = id;
                            item.FirstName = model.FirstName;
                            item.LastName = model.LastName;
                            item.Name = model.Name;
                            item.Phone = model.Phone;
                            item.Email = model.Email;
                            item.Password = model.Password;
                            item.Country = model.Country;
                            item.BirthDate = model.BirthDate;
                            item.Address = model.Address;
                            item.Gender = model.Gender;
                            item.MaritalStatus = model.MaritalStatus;
                            item.IsActive = model.IsActive;
                            item.IsSupperAdmin = model.IsSupperAdmin;
                            item.CreatedDate = DateTime.Now;
                            item.CreatedUser = model.CreatedUser;
                            item.ModifiedDate = DateTime.Now;
                            item.ModifiedUser = model.CreatedUser;
                            cxt.dbEmployee.Add(item);
                            cxt.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            NSLog.Logger.Error("Không thể thêm nhân viên. Làm ơn kiểm tra lại!", ex);
                            result = false;
                            msg = "Không thể cập nhập cho nhân viên này. Làm ơn kiểm tra lại!";
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (cxt != null)
                                cxt.Dispose();
                        }
                    }
                }
            }
            return result;
        }

        public bool DeleteEmployee(string id, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var item = cxt.dbEmployee.Where(x => x.ID == id && !x.IsActive && !x.IsSupperAdmin).FirstOrDefault();
                    if (item != null)
                    {
                        cxt.dbEmployee.Remove(item);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Nhân viên này hiện đang trong hợp đồng. Làm ơn kiểm tra lại!";
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error(msg, ex);
                    result = false;
                }
                finally
                {
                    if (cxt != null)
                        cxt.Dispose();
                }
            }
            return result;
        }

        public bool UpdateEmployee(EmployeeModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                var _isExits = cxt.dbEmployee.Any(x => x.Email.Equals(model.Email) && x.IsActive && !x.ID.Equals(model.ID));
                if (_isExits)
                {
                    result = false;
                    msg = "Địa chỉ email đã tồn tại";
                }
                else
                {
                    using (var transaction = cxt.Database.BeginTransaction())
                    {
                        try
                        {
                            var itemUpdate = cxt.dbEmployee.Where(x => x.ID == model.ID).FirstOrDefault();
                            if (itemUpdate != null)
                            {
                                itemUpdate.FirstName = model.FirstName;
                                itemUpdate.LastName = model.LastName;
                                itemUpdate.Name = model.Name;
                                itemUpdate.Phone = model.Phone;
                                itemUpdate.Email = model.Email;
                                itemUpdate.Password = model.Password;
                                itemUpdate.Country = model.Country;
                                itemUpdate.BirthDate = model.BirthDate;
                                itemUpdate.Address = model.Address;
                                itemUpdate.Gender = model.Gender;
                                itemUpdate.MaritalStatus = model.MaritalStatus;
                                itemUpdate.IsActive = model.IsActive;
                                itemUpdate.IsSupperAdmin = model.IsSupperAdmin;
                                itemUpdate.ModifiedDate = DateTime.Now;
                                itemUpdate.ModifiedUser = model.CreatedUser;
                                cxt.SaveChanges();
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            NSLog.Logger.Error("Không thể cập nhập cho nhân viên này. Làm ơn kiểm tra lại!", ex);
                            result = false;
                            msg = "Không thể cập nhập cho nhân viên này. Làm ơn kiểm tra lại!";
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (cxt != null)
                                cxt.Dispose();
                        }
                    }
                }

            }
            return result;
        }
    }
}
