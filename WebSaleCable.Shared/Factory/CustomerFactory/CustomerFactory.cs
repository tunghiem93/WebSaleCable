using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaleCable.Data;
using WebSaleCable.Data.Entities;
using WebSaleCable.Shared.Model.Customer;

namespace WebSaleCable.Shared.Factory.CustomerFactory
{
    public class CustomerFactory
    {
        public List<CustomerModels> GetListCustomer()
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var lstResult = cxt.dbCustomer.Select(o => new CustomerModels()
                    {
                        ID = o.ID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,                        
                        Phone = o.Phone,
                        Email = o.Email,
                        Password = o.Password,
                        Country = o.Country,
                        Address = o.Address,
                        Gender = o.Gender,
                        BirthDate = o.BirthDate,
                        MaritalStatus = o.MaritalStatus,
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
                    NSLog.Logger.Error("GetListCustomer_Fail", ex);
                    return null;
                }
            }
        }

        public CustomerModels GetDetailCustomer(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var result = cxt.dbCustomer.Where(w => w.ID == id).Select(o => new CustomerModels()
                    {
                        ID = o.ID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Phone = o.Phone,
                        Email = o.Email,
                        Password = o.Password,
                        Country = o.Country,
                        Address = o.Address,
                        Gender = o.Gender,
                        BirthDate = o.BirthDate,
                        MaritalStatus = o.MaritalStatus,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDate,
                        CreatedUser = o.CreatedUser,
                        ModifiedDate = o.ModifiedDate,
                        ModifiedUser = o.ModifiedUser,
                    }).FirstOrDefault();
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetDetailCustomer_Fail", ex);
                    return null;
                }
            }
        }

        public bool InsertCustomer(CustomerModels model, ref string msg, ref string cusId)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                var _isExits = cxt.dbCustomer.Any(x => x.Email.Equals(model.Email) && x.IsActive);
                if (_isExits)
                {
                    msg = "Địa chỉ email đã tồn tại";
                    result = false;
                }
                else
                {
                    using (var transaction = cxt.Database.BeginTransaction())
                    {
                        try
                        {
                            DbCustomer item = new DbCustomer();
                            string id = Guid.NewGuid().ToString();
                            item.ID = id;
                            item.FirstName = model.FirstName;
                            item.LastName = model.LastName;
                            item.Name = model.Name;
                            item.Phone = model.Phone;
                            item.Email = model.Email;
                            item.Password = model.Password;
                            item.Country = model.Country;
                            item.Address = model.Address;
                            item.Gender = model.Gender;
                            item.BirthDate = model.BirthDate;
                            item.MaritalStatus = model.MaritalStatus;
                            item.IsActive = model.IsActive;
                            item.CreatedDate = DateTime.Now;
                            item.CreatedUser = model.CreatedUser;
                            item.ModifiedDate = DateTime.Now;
                            item.ModifiedUser = model.CreatedUser;

                            cxt.dbCustomer.Add(item);
                            cxt.SaveChanges();
                            transaction.Commit();
                            cusId = id;
                        }
                        catch (Exception ex)
                        {
                            NSLog.Logger.Error("Không thể thêm khách hàng. Làm ơn kiểm tra lại!", ex);
                            result = false;
                            msg = "Không thể thêm khách hàng. Làm ơn kiểm tra lại!";
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

        public bool DeleteCustomer(string id, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var item = cxt.dbCustomer.Where(x => x.ID == id && !x.IsActive && !x.IsAdmin).FirstOrDefault();
                    if (item != null)
                    {
                        cxt.dbCustomer.Remove(item);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Khách hàng này hiện vẫn đang hoạt động. Làm ơn kiểm tra lại!";
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

        public bool UpdateCustomer(CustomerModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                var _isExits = cxt.dbCustomer.Any(x => x.Email.Equals(model.Email) && x.IsActive && !x.ID.Equals(model.ID));
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
                            var itemUpdate = cxt.dbCustomer.Where(x => x.ID == model.ID).FirstOrDefault();
                            if (itemUpdate != null)
                            {
                                itemUpdate.FirstName = model.FirstName;
                                itemUpdate.LastName = model.LastName;
                                itemUpdate.Name = model.Name;
                                itemUpdate.Phone = model.Phone;
                                itemUpdate.Password = model.Password;
                                itemUpdate.Country = model.Country;
                                itemUpdate.Address = model.Address;
                                itemUpdate.Email = model.Email;
                                itemUpdate.Gender = model.Gender;
                                itemUpdate.BirthDate = model.BirthDate;
                                itemUpdate.MaritalStatus = model.MaritalStatus;
                                itemUpdate.IsActive = model.IsActive;
                                itemUpdate.ModifiedDate = DateTime.Now;
                                itemUpdate.ModifiedUser = model.CreatedUser;

                                cxt.SaveChanges();
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            NSLog.Logger.Error("Không thể cập nhập cho khách hàng này. Làm ơn kiểm tra lại!", ex);
                            result = false;
                            msg = "Không thể cập nhập cho khách hàng này. Làm ơn kiểm tra lại!";
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

        //public ClientLoginModel Login(ClientLoginModel model)
        //{
        //    try
        //    {
        //        using (var cxt = new DataContext())
        //        {
        //            var data = cxt.dbCustomer.Where(x => x.Email.Equals(model.Email)
        //                                                && x.Password.Equals(model.Password)
        //                                                && x.IsActive)
        //                                      .Select(x => new ClientLoginModel
        //                                      {
        //                                          Email = x.Email,
        //                                          DisplayName = x.Name,
        //                                          Password = x.Password,
        //                                          IsAdmin = x.IsAdmin,
        //                                      })
        //                                      .FirstOrDefault();
        //            return data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NSLog.Logger.Error("Login", ex);
        //    }
        //    return null;
        //}
    }
}
