using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSaleCable.Shared;
using WebSaleCable.Shared.Factory.ProductFactory;
using WebSaleCable.Shared.Model.Product;
using WebSaleCable.Shared.Utilities;

namespace WebSaleCable.Areas.ClientSite.Controllers
{
    public class HomeController : HQController
    {
        private ProductFactory _fac;
        public HomeController()
        {
            _fac = new ProductFactory();
        }
        // GET: ClientSite/Home
        public ActionResult Index()
        {
            try
            {
                ProductViewModels model = new ProductViewModels();
                var data = _fac.GetListProduct();
                data.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                if (data != null && data.Any())
                {
                   model.ListProduct = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(12).ToList();
                   model.TotalProduct = data.Count;
                    var pageIndex = 0;
                    if (data.Count % 12 == 0)
                        pageIndex = data.Count / 12;
                    else
                        pageIndex = Convert.ToInt16(data.Count / 12) + 1;

                    if (pageIndex > 1)
                        model.TotalPage = 2;
                    else
                        model.IsAddMore = true;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Index: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }            
        }

        [HttpPost]
        public ActionResult SearchKey(string Key = "")
        {
            var model = new ProductViewModels();

            var data = _fac.GetListProduct()
                                    .Where(x => CommonHelper.RemoveUnicode(x.Name.ToLower()).Contains(CommonHelper.RemoveUnicode(Key.ToLower())))
                                    .OrderByDescending(x => x.CreatedDate)
                                                   .ToList();
            
            if (data != null && data.Any())
            {
                data.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                model.Key = Key;
                model.ListProduct = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(12).ToList();
                model.TotalProduct = data.Count;
                var pageIndex = 0;
                if (data.Count % 12 == 0)
                    pageIndex = data.Count / 12;
                else
                    pageIndex = Convert.ToInt16(data.Count / 12) + 1;

                if (pageIndex > 1)
                    model.TotalPage = 2;
                else
                    model.IsAddMore = true;
            }
            return PartialView("_ListItem", model);
        }

        public ActionResult Detail(string id)
        {
            try
            {
                ProductDetailViewModels model = new ProductDetailViewModels();
                if (!string.IsNullOrEmpty(id))
                {

                    var data = _fac.GetListProduct();

                    var dataDetail = data.Where(x => x.ID.Equals(id)).FirstOrDefault();
                    if (dataDetail != null)
                    {
                        if (!string.IsNullOrEmpty(dataDetail.ImageURL))
                            dataDetail.ImageURL = Commons.HostImage + dataDetail.ImageURL;
                        else
                            dataDetail.ImageURL = Commons.Image400_250;
                        if (dataDetail.ListImg != null)
                        {
                            dataDetail.ListImg.ForEach(x =>
                            {
                                x.ImageURL = Commons.HostImage + x.ImageURL;
                            });
                            dataDetail.ListImg.Add(new ImageProduct
                            {
                                ImageURL = dataDetail.ImageURL
                            });
                        }
                        else
                        {
                            dataDetail.ListImg = new List<ImageProduct>()
                        {
                            new ImageProduct
                            {
                                ImageURL = dataDetail.ImageURL
                            }
                        };
                        }
                        dataDetail.ListImg = dataDetail.ListImg.OrderBy(x => x.ImageURL).Skip(0).Take(4).ToList();

                        var oldData = data.Where(x => !x.ID.Equals(id) && x.CategoryID.Equals(dataDetail.CategoryID)).OrderBy(x => x.CreatedDate).Skip(0).Take(5).ToList();
                        oldData.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.ImageURL))
                                x.ImageURL = Commons.HostImage + x.ImageURL;
                            else
                                x.ImageURL = Commons.Image400_250;
                        });
                        model.ListProduct = oldData;
                        model.Product = dataDetail;
                        return View(model);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetail: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }            
        }

        public ActionResult ProductDetail(string id)
        {
            try
            {
                ProductDetailViewModels model = new ProductDetailViewModels();
                if (!string.IsNullOrEmpty(id))
                {

                    var data = _fac.GetListProduct();

                    var dataDetail = data.Where(x => x.ID.Equals(id)).FirstOrDefault();
                    if (dataDetail != null)
                    {
                        if (!string.IsNullOrEmpty(dataDetail.ImageURL))
                            dataDetail.ImageURL = Commons.HostImage + dataDetail.ImageURL;
                        else
                            dataDetail.ImageURL = Commons.Image400_250;
                        if (dataDetail.ListImg != null)
                        {
                            dataDetail.ListImg.ForEach(x =>
                            {
                                x.ImageURL = Commons.HostImage + x.ImageURL;
                            });
                            dataDetail.ListImg.Add(new ImageProduct
                            {
                                ImageURL = dataDetail.ImageURL
                            });
                        }
                        else
                        {
                            dataDetail.ListImg = new List<ImageProduct>()
                        {
                            new ImageProduct
                            {
                                ImageURL = dataDetail.ImageURL
                            }
                        };
                        }
                        dataDetail.ListImg = dataDetail.ListImg.OrderBy(x => x.ImageURL).Skip(0).Take(4).ToList();

                        var oldData = data.Where(x => !x.ID.Equals(id) && x.CategoryID.Equals(dataDetail.CategoryID)).OrderBy(x => x.CreatedDate).Skip(0).Take(5).ToList();
                        oldData.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.ImageURL))
                                x.ImageURL = Commons.HostImage + x.ImageURL;
                            else
                                x.ImageURL = Commons.Image400_250;
                        });
                        model.ListProduct = oldData;
                        model.Product = dataDetail;
                        return View(model);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetail: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        [HttpGet]
        public PartialViewResult LoadItem(string id)
        {
            ProductViewModels model = new ProductViewModels();
            if (!string.IsNullOrEmpty(id))
            {
                model.CateID = id;
                var data = _fac.GetListProduct().Where(x => x.CategoryID.Equals(id)).ToList();
                
                if (data != null && data.Any())
                {
                    data.ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.ImageURL))
                            x.ImageURL = Commons.HostImage + x.ImageURL;
                    });

                    model.ListProduct = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(12).ToList();
                    model.TotalProduct = data.Count;
                    var pageIndex = 0;
                    if (data.Count % 12 == 0)
                        pageIndex = data.Count / 12;
                    else
                        pageIndex = Convert.ToInt16(data.Count / 12) + 1;

                    if (pageIndex > 1)
                        model.TotalPage = 2;
                    else
                        model.IsAddMore = true;
                }
            }
            return PartialView("_ListItem", model);
        }        

        public PartialViewResult LoadProductOther()
        {
            var model = new ProductViewModels();
            try
            {
                var ListCate = Session["Catelogies"] as List<CateSession>;
                if(ListCate != null && ListCate.Count < 9)
                {
                    var data = _fac.GetListProduct().OrderBy(x => x.Name).ToList();
                    
                    if (data != null && data.Any())
                    {
                        data.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.ImageURL))
                                x.ImageURL = Commons.HostImage + x.ImageURL;
                        });

                        model.ListProduct = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(12).ToList();
                        model.TotalProduct = data.Count;
                        var pageIndex = 0;
                        if (data.Count % 12 == 0)
                            pageIndex = data.Count / 12;
                        else
                            pageIndex = Convert.ToInt16(data.Count / 12) + 1;

                        if (pageIndex > 1)
                            model.TotalPage = 2;
                        else
                            model.IsAddMore = true;
                    }
                }
                else
                {
                    var data = _fac.GetListProduct().Where(x => ListCate.Select(z => z.Id).Contains(x.CategoryID)).OrderBy(x => x.Name).ToList();
                    
                    if (data != null && data.Any())
                    {
                        model.IsOrther = true;
                        data.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.ImageURL))
                                x.ImageURL = Commons.HostImage + x.ImageURL;
                        });

                        model.ListProduct = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(12).ToList();
                        model.TotalProduct = data.Count;
                        var pageIndex = 0;
                        if (data.Count % 12 == 0)
                            pageIndex = data.Count / 12;
                        else
                            pageIndex = Convert.ToInt16(data.Count / 12) + 1;

                        if (pageIndex > 1)
                            model.TotalPage = 2;
                        else
                            model.IsAddMore = true;
                    }
                }
                
            }catch(Exception ex)
            {
                NSLog.Logger.Error("LoadProductOther :", ex);
            }
            return PartialView("_ListItem", model);
        }

        public PartialViewResult LoadPagging(int pageIndex, string Id = "", string Key = "",bool isOrther = false)
        {
            ProductViewModels model = new ProductViewModels();
            var data = new List<ProductModels>();
            if(!isOrther)
            {
                data = _fac.GetListProduct().Where(z => (string.IsNullOrEmpty(Id) ? 1 == 1 : z.CategoryID.Equals(Id)) && (string.IsNullOrEmpty(Key) ? 1 == 1 : CommonHelper.RemoveUnicode(z.Name.ToLower()).Contains(CommonHelper.RemoveUnicode(Key.ToLower())))).ToList();
            }
            else
            {
                var ListCate = Session["Catelogies"] as List<CateSession>;
                if (ListCate != null && ListCate.Count < 9)
                {
                    data = _fac.GetListProduct().OrderBy(x => x.Name).ToList();
                }
                else
                {
                    data = _fac.GetListProduct().Where(x => ListCate.Select(z => z.Id).Contains(x.CategoryID)).OrderBy(x => x.Name).ToList();
                }
            }

            if (data != null && data.Any())
            {
                model.CateID = Id;
                model.Key = Key;
                model.IsOrther = isOrther;
                data.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                // model.ListProduct = model.ListProduct.OrderByDescending(x => x.CreatedDate).ToList();

                model.TotalProduct = data.Count;
                model.ListProduct = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(12 * pageIndex).ToList();
                var page = 0;
                if (data.Count % 12 == 0)
                    page = data.Count / 12;
                else
                    page = Convert.ToInt16(data.Count / 12) + 1;

                if (page > pageIndex)
                    model.TotalPage = pageIndex + 1;
                else
                {
                    model.IsAddMore = true;
                }
                    
            }
            return PartialView("_ListItem", model);
        }
    }
}