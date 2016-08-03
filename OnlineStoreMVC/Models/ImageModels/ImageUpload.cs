using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using OnlineStore.Infractructure.Utility;


namespace OnlineStoreMVC.Models.ImageModels
{
    public class ImageUpload
    {
        public static readonly string LoadPath = "/Content/Images/ProductImages/SmallImages/";
        public static readonly string LoadPathCMSNews = "/Content/Images/CMSNewsImages/";
        public static readonly string LoadPathBanners = "/Content/Images/Banners/";

        // set default size here
        public int Width { get; set; }
        public int Height { get; set; }
        public string SavePath { get; set; }
        // default = true
        public bool? IsScale { get; set; }

        public ImageUpload()
        {
            // Default path
            if (string.IsNullOrEmpty(SavePath))
            {
                SavePath = LoadPath;
            }
        }

        public ImageResult RenameUploadFile(HttpPostedFileBase file, Int32 counter = 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            string prepend = "item_";
            string finalFileName = prepend + ((counter).ToString()) + "_" + fileName;
            if (System.IO.File.Exists(HttpContext.Current.Request.MapPath("~" + SavePath + finalFileName)))
            {
                return RenameUploadFile(file, ++counter);
            }
            return UploadFile(file, finalFileName);
        }

        public ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };

            var path = Path.Combine(HttpContext.Current.Request.MapPath(SavePath), fileName);
            string extension = Path.GetExtension(file.FileName);

            //make sure the file is valid
            if (!ValidateExtension(extension))
            {
                imageResult.Success = false;
                imageResult.ErrorMessage = "Invalid Extension";
                return imageResult;
            }
            try
            {
                file.SaveAs(path);

                Image imgOriginal = Image.FromFile(path);
                //pass in whatever value you want
                if (IsScale == true || IsScale == null)
                {
                    Image imgActual = Scale(imgOriginal);
                    imgOriginal.Dispose();
                    imgActual.Save(path);
                    imgActual.Dispose();
                }

                imageResult.ImageName = Path.GetFileName(fileName);
                imageResult.ImagePath = Path.Combine(SavePath, fileName);
                return imageResult;
            }
            catch (Exception ex)
            {
                // you might NOT want to show the exception error for the user
                // this is generally logging or testing
                imageResult.Success = false;
                imageResult.ErrorMessage = ex.Message;
                return imageResult;
            }
        }

        public ImageResult UploadProductImage(HttpPostedFileBase file, string fileName, int size)
        {
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };

            var path = Path.Combine(HttpContext.Current.Request.MapPath(SavePath), fileName);
            string extension = Path.GetExtension(file.FileName);

            //make sure the file is valid
            if (!ValidateExtension(extension))
            {
                imageResult.Success = false;
                imageResult.ErrorMessage = "Invalid Extension";
                return imageResult;
            }
            try
            {
                file.SaveAs(path);

                Image imgOriginal = Image.FromFile(path);
                //pass in whatever value you want
                if (IsScale == true || IsScale == null)
                {
                    Image imgActual = CreateSquareImage(imgOriginal, size);
                    imgOriginal.Dispose();
                    imgActual.Save(path);
                    imgActual.Dispose();
                }

                imageResult.ImageName = Path.GetFileName(fileName);
                imageResult.ImagePath = Path.Combine(SavePath, fileName);
                return imageResult;
            }
            catch (Exception ex)
            {
                // you might NOT want to show the exception error for the user
                // this is generally logging or testing
                imageResult.Success = false;
                imageResult.ErrorMessage = ex.Message;
                return imageResult;
            }
        }

        private bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        private Image Scale(Image imgPhoto)
        {
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        private Image CreateSquareImage(Image img, int size)
        {
            float sourceWidth = img.Width;
            float sourceHeight = img.Height;
            float destHeight = size;
            float destWidth = size;
            int destX = 0;
            int destY = 0;

            // change size proportially depending on width or height
            if (sourceHeight > sourceWidth)
            {
                destWidth = (float)(size * sourceWidth) / sourceHeight;
                destHeight = size;
                destX = (int)(size - destWidth)/2;
            }
            else
            {
                destWidth = size;
                destHeight = (float)(sourceHeight * size / sourceWidth);
                destY = (int)(size - destHeight)/2;
            }

            //Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
            //                            PixelFormat.Format32bppPArgb);
            //bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            Bitmap bmPhoto = ResizeImage(img, (int)destWidth, (int)destHeight);

            Bitmap resultImage = new Bitmap(size, size, PixelFormat.Format32bppPArgb);
            resultImage.SetResolution(bmPhoto.HorizontalResolution, bmPhoto.VerticalResolution);
            Graphics g = System.Drawing.Graphics.FromImage(resultImage);
            g.Clear(Color.White);
            g.DrawImage(bmPhoto, new Point(destX, destY));
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.Dispose();

            return resultImage;
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
                    //graphics.DrawImage(image, destRect, new Rectangle(0, 0, (int)image.Width, (int)image.Height), GraphicsUnit.Pixel);
                }
            }

            return destImage;
        }
    }
}