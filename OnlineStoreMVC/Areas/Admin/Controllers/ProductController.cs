using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Model.Context;
using OnlineStore.Service.Interfaces;
using OnlineStore.Service.Implements;
using OnlineStore.Model.ViewModel;
using PagedList;
using OnlineStore.Model.MessageModel;
using System.IO;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Mapper;
using OnlineStoreMVC.Models.ImageModels;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class ProductController : BaseManagementController
    {

        #region Constructures

        public ProductController()
        {

        }

        #endregion

        #region Private functions

        /// <summary>
        /// Delete image from server
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [NonAction]
        private bool DeleteImageInFolder(string path)
        {

            string filePath = Server.MapPath("~/" + path);
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Create SelectList using as dataSource for dropdownlist
        /// </summary>
        /// <param name="selectedBrandId"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> PopulateListBrand(int? selectedBrandId = null)
        {
            IEnumerable<ecom_Brands> brands = service.GetListBrand();

            IEnumerable<SelectListItem> listBrands = brands.Select(b => new SelectListItem()
            {
                Text = b.Name,
                Value = b.Id.ToString(),
                Selected = b.Id == selectedBrandId
            });

            return listBrands;
        }

        /// <summary>
        /// Create product group SelectList using as dataSource for dropdownlist
        /// </summary>
        /// <param name="selectedBrandId"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> PopulateListProductGroup(int[] selectedProductGroups = null)
        {
            IEnumerable<ecom_ProductGroups> groups = service.GetListProductGroup();

            return new MultiSelectList(groups, "Id", "Name", selectedProductGroups);
        }

        /// <summary>
        /// Create SelectList using as dataSource for dropdownlist
        /// </summary>
        /// <param name="selectedBrandId"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> PopulateListCategory(int[] selectedCategories = null)
        {
            IEnumerable<ecom_Categories> categories = service.GetListCategory();

            return new MultiSelectList(categories, "Id", "Name", selectedCategories);
        }

        /// <summary>
        /// Upload product image with 2 size(large and small)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public bool UploadProductImages(HttpPostedFileBase file, out string largeFileName,Int32 counter = 0)
        {
            try
            {
                ImageUpload largeImage = new ImageUpload { SavePath = DisplayProductConstants.LargeProductImageFolderPath};
                ImageUpload smallImage = new ImageUpload { SavePath = DisplayProductConstants.SmallProductImageFolderPath };
                var fileName = Path.GetFileName(file.FileName);
                string finalFileName = "ProductImage_" + ((counter).ToString()) + "_" + fileName;
                if (System.IO.File.Exists(HttpContext.Request.MapPath("~" + DisplayProductConstants.LargeProductImageFolderPath + finalFileName)) || System.IO.File.Exists(HttpContext.Request.MapPath("~" + DisplayProductConstants.SmallProductImageFolderPath + finalFileName)))
                {
                    return UploadProductImages(file, out largeFileName, ++counter);
                }
                ImageResult uploadLargeImage = largeImage.UploadProductImage(file, finalFileName,1000);
                ImageResult uploadSmallImage = smallImage.UploadProductImage(file, finalFileName,700);
                largeFileName = uploadSmallImage.ImageName;
                return true;
            }
            catch (Exception)
            {
                largeFileName = null;
                return false;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Return view with list product
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var products = service.GetProducts(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            IPagedList<ProductSummaryViewModel> pageProducts = new StaticPagedList<ProductSummaryViewModel>(products, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageProducts);
        }

        /// <summary>
        /// Return Create view to let user input information of new product
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            PopulateStatusDropDownList();
            //ViewBag.ProductGroupId = PopulateListProductGroup();
            ViewBag.BrandId = PopulateListBrand();
            return View();
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="productRequest">information of new product</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(CreateProductPostRequest productRequest)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["coverImage"];
                //HttpPostedFileBase file = coverImage;
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentLength > 0)
                    {
                        // width + height will force size, care for distortion
                        //Exmaple: ImageUpload imageUpload = new ImageUpload { Width = 800, Height = 700 };

                        // height will increase the width proportionally
                        //Example: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                        // width will increase the height proportionally
                        ImageUpload imageUpload = new ImageUpload { Width = 600 };

                        // rename, resize, and upload
                        //return object that contains {bool Success,string ErrorMessage,string ImageName}
                        ImageResult imageResult = imageUpload.RenameUploadFile(file);
                        if (imageResult.Success)
                        {
                            // Add new image to database
                            var photo = new share_Images
                            {
                                ImageName = imageResult.ImageName,
                                ImagePath = Path.Combine(ImageUpload.LoadPath, imageResult.ImageName)
                            };
                            var imageId = service.AddImage(photo);
                            // Add product
                            productRequest.CoverImageId = imageId;
                            service.AddProduct(productRequest);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // use imageResult.ErrorMessage to show the error
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }
                }

            }
            PopulateStatusDropDownList();
            ViewBag.BrandId = PopulateListBrand(productRequest.BrandId);
            return View(productRequest);
        }

        /// <summary>
        /// Get information of product and return Edit View for user update data for product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ecom_Products product = service.GetProductById((int)id);
            int[] listCategory;
            int[] listProductGroup;

            if (product == null)
            {
                return HttpNotFound();
            }

            // Populate status dropdownlist
            if (product.Status != null)
            {
                var status = (Define.Status)product.Status;
                PopulateStatusDropDownList(status);
            }
            else
            {
                PopulateStatusDropDownList();
            }
            // Populate category dropdownlist
            if (product.ecom_Categories.Count > 0)
            {
                listCategory = product.ecom_Categories.Select(c => c.Id).ToArray();
            }
            else
            {
                listCategory = null;
            }
            // Populate product group dropdownlist
            if (product.ecom_ProductGroups.Count > 0)
            {
                listProductGroup = product.ecom_ProductGroups.Select(c => c.Id).ToArray();
            }
            else
            {
                listProductGroup = null;
            }
            ViewBag.BrandId = PopulateListBrand(product.BrandId);
            ViewBag.Categories = PopulateListCategory(listCategory);
            ViewBag.ProductGroups = PopulateListProductGroup(listProductGroup);
            return View(product.ConvertToProductFullView());
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">information of product need to updated</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ProductFullView product)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = service.UpdateProduct(product);
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            if (product.Status != null)
            {
                var status = (Define.Status)product.Status;
                PopulateStatusDropDownList(status);
            }
            else
            {
                PopulateStatusDropDownList();
            }
            ViewBag.BrandId = PopulateListBrand(product.BrandId);
            return View(product);
        }

        /// <summary>
        /// Upload image to server
        /// </summary>
        /// <param name="files"></param>
        /// <param name="IdProduct"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult UploadImage(IEnumerable<HttpPostedFileBase> files, int productId)
        {
            if (files != null)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file.ContentLength > 0)
                    {
                        string largeFileName = null;
                        bool isSuccess = UploadProductImages(file,out largeFileName);
                        if (isSuccess)
                        {
                            share_Images largeImage = new share_Images
                            {
                                ImageName = largeFileName,
                                ImagePath = Path.Combine(ImageUpload.LoadPath, largeFileName)
                            };

                            service.AddImageForProduct(productId, largeImage);
                        }
                        else
                        {
                            // use imageResult.ErrorMessage to show the error
                            ViewBag.Error = "Upload product image fail";
                        }
                    }
                }
            }
            return ListImageProduct(productId);
        }

        /// <summary>
        /// Remove a image from list images of product
        /// </summary>
        /// <param name="Id">product Id</param>
        /// <returns>updated view of Cart</returns>
        public ActionResult DeleteImage(int productId, int imageId)
        {
            try
            {
                share_Images deleteImages;
                bool isSuccess = service.DeleteImage(productId, imageId, out deleteImages);
                if (isSuccess)
                {
                    string largeImagePath = DisplayProductConstants.LargeProductImageFolderPath + deleteImages.ImageName;
                    DeleteImageInFolder(largeImagePath);
                    DeleteImageInFolder(deleteImages.ImagePath);
                }
                else
                {
                    ViewBag.Error = "Error when delete image";
                }
                return ListImageProduct(productId);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Set image as cover image of product
        /// </summary>
        /// <param name="productId">id of product</param>
        /// <param name="imageId">id of image</param>
        /// <returns></returns>
        public ActionResult SetAsCoverImage(int productId, int imageId)
        {
            bool isSuccess = service.SetAsCoverImage(productId, imageId);
            if (isSuccess)
            {
                //DeleteImageInFolder(imagePath);
            }
            else
            {
                ViewBag.Error = "Error when delete image";
            }
            return ListImageProduct(productId);
        }

        /// <summary>
        /// Delete product 
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isSuccess = service.DeleteProduct(id);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete product fail!");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// update image of product
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProductImage(UpdateProductImageRequest request)
        {
            if (request == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlineStore.Service.Messaging.UpdateProductImage imageInfor = new OnlineStore.Service.Messaging.UpdateProductImage()
            {
                ImageId = request.ImageId,
                Name = request.Name,
                IsActive = request.IsActive
            };
            bool isSuccess = service.UpdateProductImage(request.productId, imageInfor, request.IsCoverImage);
            if (!isSuccess)
            {
                ViewBag.Error = "Error when update image";
            }
            return ListImageProduct(request.productId);
        }

        /// <summary>
        /// Create list product image for return to client side
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult ListImageProduct(int productId)
        {
            ecom_Products product = service.GetProductById(productId);
            ListImageProductPartialViewModels listImageViewModels = new ListImageProductPartialViewModels()
            {
                ProductId = productId,
                Images = product.share_Images.ConvertToImageProductViewModels(),
                CoverImageId = product.CoverImageId
            };
            return PartialView("ListImageProduct", listImageViewModels);
        }

        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion
    }
}
