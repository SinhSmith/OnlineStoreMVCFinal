using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Model.Context;
using OnlineStore.Service.Interfaces;
using OnlineStore.Service.Implements;
using PagedList;
using OnlineStore.Model.ViewModel;
using OnlineStoreMVC.Models.ImageModels;
using System.IO;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class BannerController : BaseManagementController
    {
        private IBannerService _bannerService = new BannerService();
        private IProductService _productService = new ProductService();

        // GET: /Admin/Banner/
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var banners = _bannerService.GetBanners(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            IPagedList<BannerViewModel> pageBanners = new StaticPagedList<BannerViewModel>(banners, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageBanners);
        }

        // GET: /Admin/Banner/Create
        public ActionResult Create()
        {
            PopulateBannerTypesDropDownList();
            return View();
        }

        // POST: /Admin/Banner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BannerViewModel model, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    ImageUpload imageUpload = new ImageUpload { IsScale = false, SavePath = ImageUpload.LoadPathBanners };
                    ImageResult imageResult = imageUpload.RenameUploadFile(uploadFile);

                    if (imageResult.Success)
                    {
                        // Add new image to database
                        var photo = new share_Images
                        {
                            ImageName = imageResult.ImageName,
                            ImagePath = imageResult.ImagePath
                        };
                        var imageId = _productService.AddImage(photo);
                        if (imageId != null)
                        {
                            // Add banner
                            model.ImageId = imageId.Value;
                            _bannerService.AddBanner(model);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ViewBag.Error = imageResult.ErrorMessage;
                    }
                }
            }

            PopulateBannerTypesDropDownList();
            return View(model);
        }

        // GET: /Admin/Banner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var banner = _bannerService.GetBannerById(id);
            if (banner == null)
            {
                return HttpNotFound();
            }

            PopulateBannerTypesDropDownList((OnlineStore.Infractructure.Utility.Define.BannerTypes)banner.Type);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)banner.Status);
            return View(banner);
        }

        // POST: /Admin/Banner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BannerViewModel model, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    ImageUpload imageUpload = new ImageUpload { IsScale = false, SavePath = ImageUpload.LoadPathBanners };
                    ImageResult imageResult = imageUpload.RenameUploadFile(uploadFile);

                    if (imageResult.Success)
                    {
                        // Add new image to database
                        var photo = new share_Images
                        {
                            ImageName = imageResult.ImageName,
                            ImagePath = imageResult.ImagePath
                        };
                        var imageId = _productService.AddImage(photo);
                        if (imageId != null)
                        {
                            // Add banner
                            model.ImageId = imageId.Value;
                        }
                    }
                    else
                    {
                        ViewBag.Error = imageResult.ErrorMessage;
                    }
                }

                _bannerService.EditBanner(model);
                return RedirectToAction("Index");
            }

            PopulateBannerTypesDropDownList((OnlineStore.Infractructure.Utility.Define.BannerTypes)model.Type);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)model.Status);
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _bannerService.DeleteBanner(id);
            return RedirectToAction("Index");
        }
    }
}
