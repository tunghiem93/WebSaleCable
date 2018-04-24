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
                model.ListProduct = _fac.GetListProduct();
                model.ListProduct.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                if (model.ListProduct != null && model.ListProduct.Any())
                {
                    model.TotalProduct = model.ListProduct.Count;
                    model.ListProduct = model.ListProduct.OrderByDescending(x => x.CreatedDate).Skip(0).Take(6).ToList();
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
            var modelView = new ProductViewModels();

            var data = _fac.GetListProduct()
                                    .Where(x => CommonHelper.RemoveUnicode(x.Name.ToLower()).Contains(CommonHelper.RemoveUnicode(Key.ToLower())))
                                    .OrderByDescending(x => x.CreatedDate)
                                                   .ToList();
            data.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + x.ImageURL;
            });
            modelView.ListProduct = data;
            return PartialView("_ListItem", modelView);
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

        [HttpGet]
        public new PartialViewResult LoadItem(string id)
        {
            ProductViewModels model = new ProductViewModels();
            if (!string.IsNullOrEmpty(id) && id.Equals("1"))
            {
                model.ListProduct = _fac.GetListProduct().ToList();
                model.ListProduct.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                if (model.ListProduct != null && model.ListProduct.Any())
                {
                    model.TotalProduct = model.ListProduct.Count;
                }
                model.IsAddMore = true;
            }
            else
            {
                
                model.ListProduct = _fac.GetListProduct().Where(x => x.CategoryID.Equals(id)).ToList();
                model.ListProduct.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                if(model.ListProduct != null && model.ListProduct.Any())
                {
                    model.TotalProduct = model.ListProduct.Count;
                    model.ListProduct = model.ListProduct.OrderByDescending(x => x.CreatedDate).Skip(0).Take(6).ToList();
                }
            }
            return PartialView("_ListItem", model);
        }        
    }
}