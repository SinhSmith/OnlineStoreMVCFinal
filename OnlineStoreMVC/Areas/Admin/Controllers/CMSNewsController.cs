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
    public class CMSNewsController : BaseManagementController
    {
        private ICMSNewsService _cmsNewsService = new CMSNewsService();
        private ICMSCategoryService _cmsCategoryService = new CMSCategoryService();
        private IProductService _productService = new ProductService();
        
        [NonAction]
        protected virtual List<SelectListItem> PrepareAllCategoriesModel(int selectedItemId = 0)
        {
            var availableCategories = new List<SelectListItem>();
            int totalItems = 0;
            var categories = _cmsCategoryService.GetCMSCategories(1, int.MaxValue, out totalItems);
            foreach (var c in categories)
            {
                if (c.Id != selectedItemId)
                {
                    availableCategories.Add(new SelectListItem
                    {
                        Text = CMSCategoryExtensions.GetFormattedBreadCrumb(c, categories),
                        Value = c.Id.ToString()
                    });
                }
            }

            return availableCategories;
        }

        // GET: /Admin/CMSNews/
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var categories = _cmsNewsService.GetCMSNews(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            IPagedList<CMSNewsView> pageNews = new StaticPagedList<CMSNewsView>(categories, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageNews);
        }

        // GET: /Admin/CMSNews/Create
        public ActionResult Create()
        {
            ViewBag.AvailableCategories = PrepareAllCategoriesModel();
            return View();
        }

        // POST: /Admin/CMSNews/Create
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CMSNewsView model, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadFile != null && uploadFile.ContentLength > 0)
                    {
                        ImageUpload imageUpload = new ImageUpload { IsScale = false, SavePath = ImageUpload.LoadPathCMSNews };
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
                                model.CoverImageId = imageId.Value;
                            }
                        }
                        else
                        {
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }

                    _cmsNewsService.AddCMSNews(model);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel();
            return View(model);
        }

        // GET: /Admin/CMSNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _cmsNewsService.GetCMSNewsById(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel(id.Value);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)news.Status);
            return View(news);
        }

        // POST: /Admin/CMSNews/Edit/5
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CMSNewsView model, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadFile != null && uploadFile.ContentLength > 0)
                    {
                        ImageUpload imageUpload = new ImageUpload { IsScale = false, SavePath = ImageUpload.LoadPathCMSNews };
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
                                model.CoverImageId = imageId.Value;
                            }
                        }
                        else
                        {
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }

                    _cmsNewsService.EditCMSNews(model);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel(model.Id);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)model.Status);
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _cmsNewsService.DeleteCMSNews(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }
    }
}
