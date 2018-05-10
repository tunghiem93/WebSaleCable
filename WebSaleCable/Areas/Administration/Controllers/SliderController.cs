using NuWebForSaler.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebSaleCable.Shared;
using WebSaleCable.Shared.Model;
using WebSaleCable.Shared.Model.Product;

namespace WebSaleCable.Areas.Administration.Controllers
{
    public class SliderController : Controller
    {
        // GET: Administration/Slider
        public ActionResult Index()
        {
            var model = new SliderSession();
            try
            {
                var _Path = HostingEnvironment.MapPath("~/Images/silder/");
                var list = Directory.GetFiles(_Path).Select(x => Path.GetFileName(x)).ToList();
               
                if (list != null && list.Count > 0)
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        model.ListImg.Add(new ImageProduct
                        {
                            ImageURL = "~/Images/silder/" + list[i],
                            OffSet = i
                        });
                    }
                }
                else
                {
                    model.ListImg.Add(new ImageProduct
                    {
                        ImageURL = Commons.Image200_100,
                        OffSet = 0
                    });
                    model.ListImg.Add(new ImageProduct
                    {
                        ImageURL = Commons.Image200_100,
                        OffSet = 1
                    });
                    model.ListImg.Add(new ImageProduct
                    {
                        ImageURL = Commons.Image200_100,
                        OffSet = 2
                    });
                }
            }
            catch(Exception ex) {
                NSLog.Logger.Error("Index Slider : ", ex);
            }
            return View(model);
        }

        public ActionResult UpdateSlider(SliderSession model)
        {
            try
            {
                if(model.ListImg != null && model.ListImg.Any())
                {
                    foreach(var item in model.ListImg)
                    {
                        if (item.PictureUpload != null && item.PictureUpload.ContentLength > 0)
                        {
                            //Delete image 
                            if(System.IO.File.Exists(Server.MapPath(item.ImageURL)))
                            {
                                ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath(item.ImageURL));
                            }
                            Byte[] imgByte = new Byte[item.PictureUpload.ContentLength];
                            item.PictureUpload.InputStream.Read(imgByte, 0, item.PictureUpload.ContentLength);
                            item.PictureByte = imgByte;
                            item.ImageURL = Guid.NewGuid() + Path.GetExtension(item.PictureUpload.FileName);
                            item.PictureUpload = null;

                            if (!string.IsNullOrEmpty(item.ImageURL) && item.PictureByte != null)
                            {
                                var path = Server.MapPath("~/Images/silder/" + item.ImageURL);
                                MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
                                ms.Write(imgByte, 0, imgByte.Length);
                                System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                                byte[] photoByte = null;
                                ImageHelper.Me.SaveCroppedImageSilder(imageTmp, path, item.ImageURL, ref photoByte);
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                NSLog.Logger.Error("UpdateSlider :", ex);
            }
            return RedirectToAction("Index");
        }
    }
}