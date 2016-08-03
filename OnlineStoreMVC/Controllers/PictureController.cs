// ***********************************************************************
// Assembly         : Sapphire.Web
// Warning          : This computer program is protected by copyright law
//                    and international treaties. Unauthorized reproduction
//                    or distribution of this program, or any part of it,
//                    may result in severe civil and criminal penalties, and
//                    will be prosecuted to the maximum extent possible under law.
// Created          : 19-Oct-2015
//
// Last Modified By : Connor
// Last Modified On : 27-Oct-2015
// ***********************************************************************
// <copyright file="PictureController.cs" company="Quantum Information Systems Solutions, Inc">
//     Copyright © 2015 Quantum Information Systems Solutions, Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Sapphire.Services.Media;
using Sapphire.Services.Security;
using Sapphire.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Sapphire.Web.Controllers
{
    /// <summary>
    /// Class PictureController.
    /// </summary>
    [AdminAuthorize]
    public class PictureController : AdminControllerBase
    {
        #region Private Fields

        /// <summary>
        /// The permission service
        /// </summary>
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService _pictureService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureController" /> class.
        /// </summary>
        /// <param name="pictureService">The picture service.</param>
        /// <param name="permissionService">The permission service.</param>
        public PictureController(IPictureService pictureService,
             IPermissionService permissionService)
        {
            this._pictureService = pictureService;
            this._permissionService = permissionService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Asynchronouses the delete icon.
        /// </summary>
        /// <param name="path">The icon path.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="System.ArgumentException">File not found exception</exception>
        public ActionResult AsyncDeleteIcon(string path)
        {
            // Var picture = _pictureService.GetPictureById(id);
            //_pictureService.DeletePicture(picture);
            var mappath = Server.MapPath(@Url.Content("~/" + path));
            try
            {
                if (System.IO.File.Exists(mappath))
                {
                    System.IO.File.Delete(mappath);
                    return Json(
                    new
                    {
                        result = true
                    },
                    "text/plain");
                }
                else
                {
                    return Json(
                    new
                    {
                        result = false
                    },
                    "text/plain");
                }
            }
            catch (Exception)
            {
                return Json(
                    new
                    {
                        result = false
                    },
                    "text/plain");
            }
        }

        /// <summary>
        /// Asynchronouses the delete input image.
        /// </summary>
        /// <param name="id">The input image id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult AsyncDeleteInputImage(int id)
        {
            var picture = _pictureService.GetPictureById(id);
            _pictureService.DeletePicture(picture);
            return null;
        }

        /// <summary>
        /// Asynchronouses the delete list icon.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="length">The icon list length.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="System.ArgumentException">File not found exception</exception>
        public ActionResult AsyncDeleteListIcon(List<String> values, int length)
        {
            for (int i = 0; i < length; i++)
            {
                var mappath = Server.MapPath(@Url.Content("~/" + values[i]));
                try
                {
                    if (System.IO.File.Exists(mappath))
                    {
                        System.IO.File.Delete(mappath);
                    }
                }
                catch (Exception)
                {
                }
            }

            return Json(
                new
                {
                    result = true
                },
                "text/plain");
        }

        /// <summary>
        /// Resize icon.
        /// </summary>
        /// <param name="path">The icon path.</param>
        /// <param name="toSize">The destination size.</param>
        /// <param name="type">The type of icon</param>
        /// <returns>ActionResult.</returns>
        public JsonResult AsyncResizeIcon(string path, string toSize, string type)
        {
            Image image = Image.FromFile(Server.MapPath(@Url.Content("~/" + path)));
            Image image2;
            int x = 0, y = 0;
            if (toSize == "S")
            {
                if (type == "Report")
                {
                    x = 84;
                    y = 102;
                }
                else
                {
                    x = 100;
                    y = 85;
                }
            }
            else if (toSize == "M")
            {
                if (type == "Report")
                {
                    x = 168;
                    y = 204;
                }
                else
                {
                    x = 200;
                    y = 170;
                }
            }

            image2 = ResizeImage(image, x, y);
            var newpath = Server.MapPath("~/Content/Images/Desktop/Site/Settings/ReportIcons");
            image2.Save(newpath + "/(" + x + "_" + y + ")" + Path.GetFileName(path), System.Drawing.Imaging.ImageFormat.Png);

            image.Dispose();
            image2.Dispose();

            return Json(
                   new
                   {
                       iconname = "/Content/Images/Desktop/Site/Settings/ReportIcons/(" + x + "_" + y + ")" + Path.GetFileName(path),
                       success = true
                   },
                   JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Merg the input image with base image and pull it to server.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>ActionResult.</returns>
        public JsonResult AsyncSave(int id, string name, string mode)
        {
            var picture = _pictureService.GetPictureById(id);
            int X = 0, Y = 0, x = 0, y = 0, x0 = 0, y0 = 0;
            var iconSize = _pictureService.GetPictureSize(picture);
            x = iconSize.Width;
            y = iconSize.Height;
            string baseimageurl = "";
            Stream stream = new MemoryStream(picture.PictureBinary);
            Image image = Image.FromStream(stream);

            switch (mode)
            {
                // Icon 1x folder
                case "1":
                    X = 100; Y = 85;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Report/icon_folderbase.png");
                    if (x > 80)
                    {
                        y = Convert.ToInt32((80 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 80, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (y > 44)
                    {
                        image = ResizeImage(image, Convert.ToInt32((44 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 44);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2;
                    y0 = Y - y - (Y - y) / 2 + 9;
                    break;
                // Icon 2x folder
                case "2":
                    X = 200; Y = 170;
                    if (x > 160)
                    {
                        y = Convert.ToInt32((160 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 160, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (y > 88)
                    {
                        image = ResizeImage(image, Convert.ToInt32((88 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 88);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2;
                    y0 = Y - y - (Y - y) / 2 + 18;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_folderbase@2x.png");
                    break;

                case "3":
                    // Icon 3x folder
                    X = 300; Y = 255;
                    if (x > 240)
                    {
                        y = Convert.ToInt32((240 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 240, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (y > 132)
                    {
                        image = ResizeImage(image, Convert.ToInt32((132 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 132);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2;
                    y0 = Y - y - (Y - y) / 2 + 27;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_folderbase@3x.png");
                    break;
                // Touch icon 1x folder
                case "4":
                    X = 100; Y = 85;
                    if (x > 80)
                    {
                        double a = x; a = (80 / a) * Convert.ToDouble(y);
                        y = Convert.ToInt32((80 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 80, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (y > 44)
                    {
                        image = ResizeImage(image, Convert.ToInt32((44 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 44);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2;
                    y0 = Y - y - (Y - y) / 2 + 9;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Report/icon_folderbase_touch.png");
                    break;
                // Touch icon 2x folder
                case "5":
                    X = 200; Y = 170;
                    if (x > 160)
                    {
                        y = Convert.ToInt32((160 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 160, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (y > 88)
                    {
                        image = ResizeImage(image, Convert.ToInt32((88 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 88);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2;
                    y0 = Y - y - (Y - y) / 2 + 18;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_folderbase_touch@2x.png");
                    break;
                // Touch icon 3x folder
                case "6":
                    X = 300; Y = 255;
                    if (x > 240)
                    {
                        y = Convert.ToInt32((240 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 240, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (y > 132)
                    {
                        image = ResizeImage(image, Convert.ToInt32((132 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 132);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2;
                    y0 = Y - y - (Y - y) / 2 + 27;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_folderbase_touch@3x.png");
                    break;
                // Icon 1x report
                case "7":
                    X = 82; Y = 102;
                    if (y > 72)
                    {
                        image = ResizeImage(image, Convert.ToInt32((72 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 72);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (x > 42)
                    {
                        y = Convert.ToInt32((42 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 42, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 7;
                    y0 = Y - y - (Y - y) / 2;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Report/icon_reportbase.png");
                    break;
                // Icon 2x report
                case "8":
                    X = 164; Y = 204;
                    if (y > 144)
                    {
                        image = ResizeImage(image, Convert.ToInt32((144 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 144);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 7;
                    y0 = Y - y - (Y - y) / 2;
                    if (x > 84)
                    {
                        y = Convert.ToInt32((84 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 84, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 14;
                    y0 = Y - y - (Y - y) / 2;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_reportbase@2x.png");
                    break;
                // Icon 3x report
                case "9":
                    X = 252; Y = 306;
                    if (y > 216)
                    {
                        image = ResizeImage(image, Convert.ToInt32((216 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 216);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 7;
                    y0 = Y - y - (Y - y) / 2;
                    if (x > 126)
                    {
                        y = Convert.ToInt32((126 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 126, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 21;
                    y0 = Y - y - (Y - y) / 2;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_reportbase@3x.png");
                    break;
                // Touch icon 1x report
                case "10":
                    X = 82; Y = 102;
                    if (y > 72)
                    {
                        image = ResizeImage(image, Convert.ToInt32((72 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 72);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (x > 42)
                    {
                        y = Convert.ToInt32((42 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 42, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 7;
                    y0 = Y - y - (Y - y) / 2;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Report/icon_reportbase_touch.png");
                    break;
                // Touch icon 2x report
                case "11":
                    X = 164; Y = 204;
                    if (y > 144)
                    {
                        image = ResizeImage(image, Convert.ToInt32((144 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 144);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (x > 84)
                    {
                        y = Convert.ToInt32((84 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 84, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 14;
                    y0 = Y - y - (Y - y) / 2;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_reportbase_touch@2x.png");
                    break;
                // Touch icon 3x report
                case "12":
                    X = 252; Y = 306;
                    if (y > 216)
                    {
                        image = ResizeImage(image, Convert.ToInt32((216 / Convert.ToDouble(y)) * Convert.ToDouble(x)), 216);
                        x = image.Width;
                        y = image.Height;
                    }
                    if (x > 126)
                    {
                        y = Convert.ToInt32((126 / Convert.ToDouble(x)) * Convert.ToDouble(y));
                        image = ResizeImage(image, 126, y);
                        x = image.Width;
                        y = image.Height;
                    }
                    x0 = X - x - (X - x) / 2 + 21;
                    y0 = Y - y - (Y - y) / 2;
                    baseimageurl = @Url.Content("~/Content/Images/Desktop/Site/Settings/ReportIcons/icon_reportbase_touch@3x.png");
                    break;

                default:
                    break;
            }
            if (baseimageurl == "")
            {
                return null;
            }

            Image image2 = Image.FromFile(Server.MapPath(baseimageurl));

            Bitmap image3 = new Bitmap(X, Y);
            Graphics g = Graphics.FromImage(image3);

            g.Clear(Color.Transparent);
            g.DrawImage(image2, new Point(0, 0));
            g.DrawImage(image, new Point(x0, y0));

            g.Dispose();
            image2.Dispose();
            image.Dispose();

            var path = Server.MapPath("~/Content/Images/Desktop/Site/Settings/ReportIcons");
            image3.Save(path + "/" + name, System.Drawing.Imaging.ImageFormat.Png);
            //_pictureService.DeletePicture(picture);
            // Image.Save(path + "/" + name, System.Drawing.Imaging.ImageFormat.Gif);
            return Json(
                   new
                   {
                       iconname = "/Content/Images/Desktop/Site/Settings/ReportIcons/" + name,
                       success = true
                   },
                   JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Asynchronouses the upload.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// <exception cref="System.ArgumentException">No file uploaded</exception>
        [HttpPost]
        public ActionResult AsyncUpload()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.UploadPictures))
                return Json(new { success = false, error = "You do not have required permissions" }, "text/plain");

            // We process it distinct ways based on a browser
            // Find more info here http://stackoverflow.com/questions/4884920/mvc3-valums-ajax-file-upload
            Stream stream = null;
            var fileName = string.Empty;
            var contentType = string.Empty;// int Iheight = 0; int Iwidth = 0;
            if (String.IsNullOrEmpty(Request["qqfile"]))
            {
                // IE
                HttpPostedFileBase httpPostedFile = Request.Files[0];
                if (httpPostedFile == null)
                    throw new ArgumentException("No file uploaded");
                stream = httpPostedFile.InputStream;
                fileName = Path.GetFileName(httpPostedFile.FileName);
                contentType = httpPostedFile.ContentType;
                // Iheight = System.Drawing.Image.FromStream(stream).Height;
                // Iwidth = System.Drawing.Image.FromStream(stream).Width;
            }
            else
            {
                // Webkit, Mozilla
                stream = Request.InputStream;
                fileName = Request["qqfile"];
                // Iheight = System.Drawing.Image.FromStream(stream).Height;
                // Iwidth = System.Drawing.Image.FromStream(stream).Width;
            }

            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);

            var fileExtension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            // ContentType is not always available
            // That's why we manually update it here
            // Http://www.sfsu.edu/training/mimetype.htm
            if (String.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = "image/bmp";
                        break;

                    case ".gif":
                        contentType = "image/gif";
                        break;

                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = "image/jpeg";
                        break;

                    case ".png":
                        contentType = "image/png";
                        break;

                    case ".tiff":
                    case ".tif":
                        contentType = "image/tiff";
                        break;

                    default:
                        break;
                }
            }

            var picture = _pictureService.InsertPicture(fileBinary, contentType, null, true);
            var iconSize = _pictureService.GetPictureSize(picture);

            // When returning JSON the mime-type must be set to text/plain
            // Otherwise some browsers will pop-up a "Save As" dialog.

            return Json(
                new
                {
                    iconname = fileName,
                    success = true,
                    pictureId = picture.Id,
                    imageUrl = _pictureService.GetPictureUrl(picture, 100),
                    type = picture.MimeType,
                    height = iconSize.Height,
                    width = iconSize.Width
                },
                "text/plain");
        }

        /// <summary>
        /// Resize input image.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="width">The destination width.</param>
        /// <param name="height">The destination height</param>
        /// <returns>The image resized</returns>
        public Bitmap ResizeImage(Image image, int width, int height)
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

        #endregion Public Methods
    }
}