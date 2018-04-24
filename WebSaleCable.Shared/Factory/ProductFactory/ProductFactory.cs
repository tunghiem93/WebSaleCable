using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSaleCable.Data;
using WebSaleCable.Data.Entities;
using WebSaleCable.Shared.Model.Product;

namespace WebSaleCable.Shared.Factory.ProductFactory
{
    public class ProductFactory
    {
        public List<ProductModels> GetListProduct()
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var lstResult = (from pro in cxt.dbProduct
                                     from cat in cxt.dbCategory
                                     from loc in cxt.dbLocation
                                     where (pro.CategoryID == cat.ID && pro.LocationID == loc.ID)
                                     orderby pro.CreatedDate descending
                                     select new ProductModels()
                                     {
                                         ID = pro.ID,
                                         Name = pro.Name,
                                         LocationID = pro.LocationID,
                                         LocationName = loc.Name,
                                         CategoryID = pro.CategoryID,
                                         CategoryName = cat.Name,
                                         Type = pro.Type,
                                         Length = pro.Length,
                                         Width = pro.Width,
                                         GuaranteePeriod = pro.GuaranteePeriod,
                                         Description = pro.Description,
                                         Production = pro.Production,
                                         Code = pro.Code,
                                         State = pro.State,
                                         Quantity = pro.Quantity,
                                         Price = pro.Price,
                                         Color = pro.Color,
                                         Weight = pro.Weight,
                                         MoreInformation = pro.MoreInformation,
                                         IsActive = pro.IsActive,
                                         CreatedDate = pro.CreatedDate,
                                         CreatedUser = pro.CreatedUser,
                                         ModifiedDate = pro.ModifiedDate,
                                         ModifiedUser = pro.ModifiedUser,
                                     }).ToList();

                    var _images = cxt.dbImage.Select(x => new
                    {
                        ID = x.ID,
                        ProductID = x.ProductID,
                        ImageUrL = x.ImageUrL
                    }).ToList();

                    lstResult.ForEach(x =>
                    {
                        if (_images != null && _images.Any())
                        {
                            var defaultImage = _images.Where(z => z.ProductID.Equals(x.ID)).FirstOrDefault();
                            if (defaultImage != null)
                            {
                                x.ImageURL = defaultImage.ImageUrL;
                                /// add list image
                                var _temp = _images.Where(z => z.ProductID.Equals(x.ID) && z.ID != defaultImage.ID)
                                                     .Select(m => new ImageProduct
                                                     {
                                                         ImageURL = m.ImageUrL,
                                                         IsDelete = true
                                                     }).ToList();
                                var _offSet = 0;
                                _temp.ForEach(k =>
                                {
                                    k.OffSet = _offSet;
                                    _offSet++;
                                });
                                if (_temp != null && _temp.Any())
                                {
                                    x.ListImg.AddRange(_temp);
                                }
                            }

                        }
                    });
                    return lstResult;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListProduct_Fail", ex);
                    return null;
                }
            }
        }

        public ProductModels GetDetailProduct(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var result = (from pro in cxt.dbProduct
                                  from cat in cxt.dbCategory
                                  from loc in cxt.dbLocation
                                  where (pro.ID == id && pro.CategoryID == cat.ID && pro.LocationID == loc.ID)
                                  orderby pro.CreatedDate descending
                                  select new ProductModels()
                                  {
                                      ID = pro.ID,
                                      Name = pro.Name,
                                      LocationID = pro.LocationID,
                                      LocationName = loc.Name,
                                      CategoryID = pro.CategoryID,
                                      CategoryName = cat.Name,
                                      Type = pro.Type,
                                      Length = pro.Length,
                                      Width = pro.Width,
                                      GuaranteePeriod = pro.GuaranteePeriod,
                                      Description = pro.Description,
                                      Production = pro.Production,
                                      Code = pro.Code,
                                      State = pro.State,
                                      Quantity = pro.Quantity,
                                      Price = pro.Price,
                                      Color = pro.Color,
                                      Weight = pro.Weight,
                                      MoreInformation = pro.MoreInformation,
                                      IsActive = pro.IsActive,
                                      CreatedDate = pro.CreatedDate,
                                      CreatedUser = pro.CreatedUser,
                                      ModifiedDate = pro.ModifiedDate,
                                      ModifiedUser = pro.ModifiedUser,
                                  }).FirstOrDefault();

                    var _images = cxt.dbImage.Select(x => new
                    {
                        ID = x.ID,
                        ProductID = x.ProductID,
                        ImageUrL = x.ImageUrL
                    }).Where(z => z.ProductID == id).ToList();

                    if (_images != null && _images.Any())
                    {
                        var defaultImage = _images[0];
                        result.ImageURL = defaultImage.ImageUrL;
                        /// add list image
                        var _temp = _images.Where(z => z.ID != defaultImage.ID)
                                             .Select(m => new ImageProduct
                                             {
                                                 ImageURL = m.ImageUrL,
                                                 IsDelete = true
                                             }).ToList();
                        var _offSet = 0;
                        _temp.ForEach(k =>
                        {
                            k.OffSet = _offSet;
                            _offSet++;
                        });
                        if (_temp != null && _temp.Any())
                        {
                            result.ListImg.AddRange(_temp);
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListProduct_Fail", ex);
                    return null;
                }
            }
        }

        public bool InsertProduct(ProductModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        DbProduct item = new DbProduct();
                        string id = Guid.NewGuid().ToString();
                        item.ID = id;
                        item.Name = model.Name;
                        item.LocationID = model.LocationID;
                        item.CategoryID = model.CategoryID;
                        item.Type = model.Type;
                        item.Length = model.Length;
                        item.Width = model.Width;
                        item.GuaranteePeriod = model.GuaranteePeriod;
                        item.Description = model.Description;
                        item.Production = model.Production;
                        item.Code = model.Code;
                        item.State = model.State;
                        item.Quantity = model.Quantity;
                        item.Price = model.Price;
                        item.Color = model.Color;
                        item.Weight = model.Weight;
                        item.MoreInformation = model.MoreInformation;
                        item.IsActive = model.IsActive;                        
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        item.CreatedUser = model.CreatedUser;
                        item.ModifiedUser = model.CreatedUser;
                        cxt.dbProduct.Add(item);

                        //////////////////////////////////// Save table Image
                        var lstEImage = new List<DbImage>();
                        model.ListImageUrl.ForEach(x =>
                        {
                            lstEImage.Add(new DbImage
                            {
                                ID = Guid.NewGuid().ToString(),
                                ImageUrL = x,
                                ProductID = id
                            });
                        });
                        cxt.dbImage.AddRange(lstEImage);
                        cxt.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể thêm sản phẩm!", ex);
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

        public bool DeleteProduct(string id, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var tran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var _image = cxt.dbImage.Where(x => x.ProductID == id).ToList();
                        if (_image != null)
                            cxt.dbImage.RemoveRange(_image);

                        var item = cxt.dbProduct.Where(x => x.ID == id).FirstOrDefault();
                        if (item != null)
                        {
                            cxt.dbProduct.Remove(item);
                            cxt.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {
                            msg = "Sản phẩm này hiện đang sử dụng. Làm ơn kiểm tra lại!";
                            result = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error(msg, ex);
                        tran.Rollback();
                        result = false;
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

        public bool UpdateProduct(ProductModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var itemUpdate = cxt.dbProduct.Where(x => x.ID == model.ID).FirstOrDefault();
                        if (itemUpdate != null)
                        {
                            //itemUpdate.ID = model.ID;
                            itemUpdate.Name = model.Name;
                            itemUpdate.LocationID = model.LocationID;
                            itemUpdate.CategoryID = model.CategoryID;
                            itemUpdate.Type = model.Type;
                            itemUpdate.Length = model.Length;
                            itemUpdate.Width = model.Width;
                            itemUpdate.GuaranteePeriod = model.GuaranteePeriod;
                            itemUpdate.Description = model.Description;
                            itemUpdate.Production = model.Production;
                            itemUpdate.Code = model.Code;
                            itemUpdate.State = model.State;
                            itemUpdate.Quantity = model.Quantity;
                            itemUpdate.Price = model.Price;
                            itemUpdate.Color = model.Color;
                            itemUpdate.Weight = model.Weight;
                            itemUpdate.MoreInformation = model.MoreInformation;
                            itemUpdate.IsActive = model.IsActive;
                            itemUpdate.ModifiedDate = DateTime.Now;
                            itemUpdate.ModifiedUser = model.CreatedUser;
                            ///// update image
                            if (model.ListImageUrl != null && model.ListImageUrl.Count > 0)
                            {
                                var images = cxt.dbImage.Where(x => x.ProductID == model.ID).ToList();
                                if (images != null)
                                    cxt.dbImage.RemoveRange(images);


                                var lstEImage = new List<DbImage>();
                                model.ListImageUrl.ForEach(x =>
                                {
                                    lstEImage.Add(new DbImage
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        ImageUrL = x,
                                        ProductID = model.ID
                                    });
                                });
                                cxt.dbImage.AddRange(lstEImage);
                            }

                            cxt.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể cập nhập cho sản phẩm này. Làm ơn kiểm tra lại!", ex);
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
        public List<string> GetListImageProduct(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                List<string> result = new List<string>();
                try
                {
                    var _lstImages = cxt.dbImage.Where(w => w.ProductID == id).Select(x => new
                    {
                        ImageUrL = x.ImageUrL
                    }).ToList();
                    _lstImages.ForEach(o =>
                    {
                        if (!string.IsNullOrEmpty(o.ImageUrL))
                        {
                            result.Add(o.ImageUrL);
                        }
                    });
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListImagesProduct_Fail", ex);
                    return null;
                }
            }
        }
    }
}
