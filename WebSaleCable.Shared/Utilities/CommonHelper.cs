//using NSLog;
using NuWebForSaler.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebSaleCable.Shared.Utilities
{
    public class CommonHelper
    {
        public static string GetSHA512(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA512Managed hashString = new SHA512Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static string PublicImages = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PublicImages"]) ? "" : ConfigurationManager.AppSettings["PublicImages"];

        //public static string _UploadPath = System.Web.HttpContext.Current.Server.MapPath("~/Uploads");
        //public static string _TemplateExcelPath = System.Web.HttpContext.Current.Server.MapPath("~/ImportExportTemplate");
        //public static string _ExtractFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Uploads") + "/ExtractFolder";


        public static string _InnerException { get; set; }
        public static string _SheetName = "Sheet1";
        public static string _NeverDate = "30/12/9999";

        public static string GetUploadPath()
        {
            return System.Web.HttpContext.Current.Server.MapPath("~/Uploads");
        }
        public static string GetTemplateExcelPath()
        {
            return System.Web.HttpContext.Current.Server.MapPath("~/ImportExportTemplate");
        }
        public static string GetExtractFolderPath()
        {
            return System.Web.HttpContext.Current.Server.MapPath("~/Uploads") + "/ExtractFolder";
        }

        /* Generate the name of export file */
        public static string GetExportFileName(string name)
        {
            return string.Format("Export_{0}_{1}", name, DateTime.Now.ToString("ddMMyy"));
        }

        /*Resize image */
        public static Bitmap ResizeImage(Image image, int width = 150, int height = 150)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        /* Convert Image -> byte[] -> base64String */
        public static string ImageToBase64(Image image)
        {
            //ImageFormat format = new ImageFormat(Guid.NewGuid());
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // Convert Image to byte[]
                    image.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    ms.Close();
                    return base64String;

                }
                catch (Exception)
                {
                    ms.Close();
                    return "";
                }

            }
        }

        /* Extract ZipFile */
        public static void ExtractZipFile(string filePath, string serverZipExtractPath)
        {
            string zipToUnpack = filePath;
            string unpackDirectory = serverZipExtractPath;
            using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(zipToUnpack))
            {
                foreach (Ionic.Zip.ZipEntry e in zip1)
                {
                    e.Extract(unpackDirectory, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                }
            }

        }

        public static bool IsDirectoryEmpty(string path)
        {
            IEnumerable<string> items = Directory.EnumerateFileSystemEntries(path);
            using (IEnumerator<string> en = items.GetEnumerator())
            {
                return !en.MoveNext();
            }
        }

        public static FileInfo[] GetListFileFromZip(HttpPostedFileBase zipFile, out string imageZipPath)
        {
            FileInfo[] listFiles = new FileInfo[] { }; //list image file in folder after extract
            imageZipPath = GetFilePath(zipFile);
            try
            {

                //upload file to server
                if (System.IO.File.Exists(imageZipPath))
                    System.IO.File.Delete(imageZipPath);
                zipFile.SaveAs(imageZipPath);

                //extract file
                //ExtractZipFile(filePath, _serverZipExtractPath);
                ExtractZipFile(imageZipPath, /*_ExtractFolderPath*/GetExtractFolderPath());
                //delete zip file after extract
                if (System.IO.File.Exists(imageZipPath))
                    System.IO.File.Delete(imageZipPath);

                if (!IsDirectoryEmpty(/*_ExtractFolderPath*/GetExtractFolderPath()))
                {

                    string[] extensions = new[] { ".jpg", ".jpeg", ".png" };
                    DirectoryInfo dInfo = new DirectoryInfo(/*_ExtractFolderPath*/GetExtractFolderPath()); //Assuming Test is your Folder
                    //Getting Text files
                    listFiles =
                        dInfo.EnumerateFiles()
                             .Where(f => extensions.Contains(f.Extension.ToLower()))
                             .ToArray();
                }
                return listFiles; ;
            }
            catch (Exception e)
            {
                _InnerException = "Error occur when unzip folder image. " + e.Message;
                return null;
            }
        }

        public static string GetFilePath(HttpPostedFileBase file)
        {
            return string.Format("{0}/{1}", /*_UploadPath*/GetUploadPath(), Path.GetFileName(file.FileName));
        }

        public static bool SaveFileExcelToServer(HttpPostedFileBase excelFile, out string filePath)
        {
            try
            {
                filePath = GetFilePath(excelFile);

                //upload file to server
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                excelFile.SaveAs(filePath);

                return true;
            }
            catch (Exception e)
            {
                _InnerException = "Error occur when save file excel to server. " + e.Message;
                filePath = "";
                return false;
            }

        }

        public static bool DeleteFileFromServer(string path, bool isImageZipFile = false)
        {
            try
            {
                //delete file excel after insert to database
                if (!isImageZipFile)
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
                else
                {
                    //delete folder extract after get file.
                    if (string.IsNullOrEmpty(path))
                        path = /*_ExtractFolderPath*/GetExtractFolderPath();
                    if (System.IO.Directory.Exists(path))
                        System.IO.Directory.Delete(path, true);
                }

                return true;
            }
            catch (Exception e)
            {
                _InnerException = e.Message;
                return false;
            }
        }
        
        /* Convert FileInfo Object To Image was resized, then to base64String for saving to Database  */
        public static string ConvertFileInfoToBase64String(FileInfo fileInfo)
        {
            try
            {
                //convert to image
                byte[] bytes = new byte[fileInfo.Length];
                Stream stream = fileInfo.OpenRead();
                Image imgage = Image.FromStream(stream, true, true);
                Bitmap bitmap = ResizeImage(imgage);
                stream.Close();
                return ImageToBase64(bitmap);
            }
            catch (Exception e)
            {
                string error = e.ToString();
                //logger.Error("Convert FileInfo To Byte Array Fail. Please check in Data Processing Function!" + e.Message);
                return "";
            }
        }

        /* Trim spaces */
        public static string Trim(string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (regex.IsMatch(email))
            {
                return true;
            }
            return false;
        }
        public static bool IsNumber(string snumber)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(snumber))
            {
                return true;
            }
            return false;
        }
        //update kind of datetime       

        public static List<SelectListItem> GetListQRCodeSize = new List<SelectListItem>()
        {
            new SelectListItem(){Value ="1", Text="100 x 100"},
            new SelectListItem(){Value ="2", Text="200 x 200"},
            new SelectListItem(){Value ="3", Text="300 x 300"},
            new SelectListItem(){Value ="4", Text="400 x 400"},
            new SelectListItem(){Value ="5", Text="500 x 500"}

        };

        public static string ReturnQRCodeSize(string value)
        {
            switch (value)
            {
                case "1":
                    return "100x100";
                case "2":
                    return "200x200";
                case "3":
                    return "300x300";
                case "4":
                    return "400x400";
                case "5":
                    return "500x500";
            
                default:
                    return "200x200";
            }
        }

        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob00900X";
        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
