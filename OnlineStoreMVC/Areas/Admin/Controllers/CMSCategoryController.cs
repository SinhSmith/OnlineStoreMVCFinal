using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Model.Context;
using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using PagedList;
using OnlineStore.Model.ViewModel;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class CMSCategoryController : BaseManagementController
    {
        private ICMSCategoryService _cmsCategoryService = new CMSCategoryService();

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

        // GET: /Admin/CMS_Category/
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var categories = _cmsCategoryService.GetCMSCategories(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            var availableCategories = new List<CMSCategoryView>();
            foreach (var item in categories)
            {
                item.Title = CMSCategoryExtensions.GetFormattedBreadCrumb(item, _cmsCategoryService);
                availableCategories.Add(item);
            }

            IPagedList<CMSCategoryView> pageCategories = new StaticPagedList<CMSCategoryView>(availableCategories, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageCategories);
        }

        // GET: /Admin/CMS_Category/Create
        public ActionResult Create()
        {
            ViewBag.AvailableCategories = PrepareAllCategoriesModel();
            return View();
        }

        // POST: /Admin/CMS_Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CMSCategoryView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cmsCategoryService.AddCMSCategory(model);

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

        // GET: /Admin/CMS_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _cmsCategoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel(id.Value);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)category.Status);
            return View(category);
        }

        // POST: /Admin/CMS_Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CMSCategoryView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cmsCategoryService.EditCMSCategory(model);

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
            _cmsCategoryService.DeleteCMSCategory(id);
            return RedirectToAction("Index");
        }
    }
}
