using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaleCable.Data;
using WebSaleCable.Data.Entities;
using WebSaleCable.Shared.Model.Category;

namespace WebSaleCable.Shared.Factory.CategoryFactory
{
    public class CategoryFactory
    {
        public List<CategoryModels> GetListCategory()
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var lstResult = cxt.dbCategory.Select(o => new CategoryModels() { ID = o.ID, Name = o.Name }).ToList();
                    return lstResult;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListLocation_Fail", ex);
                    return null;
                }
            }
        }

        public CategoryModels GetDetailCategory(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var result = cxt.dbCategory.Where(w => w.ID == id).Select(o => new CategoryModels() { ID = o.ID, Name = o.Name }).FirstOrDefault();
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetDetailCategory_Fail", ex);
                    return null;
                }
            }
        }

        public bool InsertCategory(CategoryModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        DbCategory item = new DbCategory();
                        string id = Guid.NewGuid().ToString();
                        item.ID = id;
                        item.Name = model.Name;
                        item.IsActive = true;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedUser = model.CreatedUser;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedUser = model.CreatedUser;

                        cxt.dbCategory.Add(item);
                        cxt.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể thêm thể loại. Làm ơn kiểm tra lại!", ex);
                        result = false;
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (cxt != null)
                            cxt.Dispose();
                    }
                }
            }
            return result;
        }

        public bool DeleteCategory(string id, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var item = cxt.dbCategory.Where(x => x.ID == id).FirstOrDefault();
                    if (item != null)
                    {
                        cxt.dbCategory.Remove(item);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Thể loại này hiện đang sử dụng. Làm ơn kiểm tra lại!";
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

        public bool UpdateCategory(CategoryModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var itemUpdate = cxt.dbCategory.Where(x => x.ID == model.ID).FirstOrDefault();
                        if (itemUpdate != null)
                        {
                            itemUpdate.Name = model.Name;
                            itemUpdate.IsActive = itemUpdate.IsActive;
                            itemUpdate.CreatedUser = itemUpdate.CreatedUser;
                            itemUpdate.ModifiedDate = DateTime.Now;
                            itemUpdate.ModifiedUser = model.CreatedUser;

                            cxt.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể cập nhập cho thể loại này. Làm ơn kiểm tra lại!", ex);
                        result = false;
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (cxt != null)
                            cxt.Dispose();
                    }
                }
            }
            return result;
        }
    }
}
