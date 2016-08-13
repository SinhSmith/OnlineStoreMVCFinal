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
using OnlineStore.Model.ViewModel;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.MessageModel;
using OnlineStore.Model.Mapper;
using OnlineStore.Infractructure.Helper;
using PagedList;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class BrandController : BaseManagementController
    {
        #region Properties

        private IBrandManagementService brandService = new BrandManagementService();

        #endregion

        #region Constructures

        public BrandController()
        {
            brandService = new BrandManagementService();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Return list of brand
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var brands = brandService.GetBrands(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            IPagedList<ecom_Brands> pageBrands = new StaticPagedList<ecom_Brands>(brands, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageBrands);
        }
        /// <summary>
        /// Details of brand
        /// </summary>
        /// <param name="id">id of brand</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsBrandManagementView viewModel = brandService.GetDetailBrand((int)id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }
        /// <summary>
        /// Create GUI for Creat a new brand
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            PopulateStatusDropDownList();
            return View();
        }
        /// <summary>
        /// Create a new brand 
        /// </summary>
        /// <param name="brand">information of new brand</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateBrandPostRequest brand)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = brandService.AddBrand(brand.ConvertToBrandModel());
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ServerError", "Add new brand fail!");
                }
            }

            return View(brand);
        }
        /// <summary>
        /// Create GUI for edit a brand
        /// </summary>
        /// <param name="id">id of brand, which need to update</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ecom_Brands brand = brandService.GetBrandById((int)id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            PopulateStatusDropDownList((Define.Status)brand.Status);
            return View(brand.ConvertToEditBrandManagementGetResponse());
        }
        /// <summary>
        /// Update selected brand
        /// </summary>
        /// <param name="brand">information need to updated</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditBrandManagementPostRequest brand)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = brandService.UpdateBrand(brand.ConvertToBrandModel());
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ServerError", "Update brand fail!");
                }
            }
            return View(brand);
        }
        /// <summary>
        /// Delete selected brand
        /// </summary>
        /// <param name="id">id of needed delete brand</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isSuccess = brandService.DeleteBrand(id);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete brand fail!");
            }
            return Redirect("Index");
        }

        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            brandService.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
