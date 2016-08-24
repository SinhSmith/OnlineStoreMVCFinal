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
using OnlineStore.Model.MessageModel;
using OnlineStore.Model.Mapper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.ViewModel;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class ProductGroupController : BaseManagementController
    {
        #region Properties

        private OnlineStoreMVCEntities db = new OnlineStoreMVCEntities();
        private IProductGroupService service = new ProductGroupService(); 

        #endregion

        // GET: /Admin/ProductGroup/
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var groups = service.GetProductGroups(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            IPagedList<ecom_ProductGroups> pageGroups = new StaticPagedList<ecom_ProductGroups>(groups, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageGroups);
        }

        // GET: /Admin/ProductGroup/Create
        public ActionResult Create()
        {
            PopulateStatusDropDownList();
            return View();
        }

        // POST: /Admin/ProductGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(CreateProductGroupPostRequest group)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = service.AddProductGroup(group.ConvertToProductGroupModel());
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ServerError", "Add new brand fail!");
                }
            }

            return View(group);
        }

        // GET: /Admin/ProductGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ecom_ProductGroups group = service.GetProductGroupById((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            PopulateStatusDropDownList((Define.Status)group.Status);
            return View(group.ConvertToProductGroupViewModel());
        }

        // POST: /Admin/ProductGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(EditProductGroupManagementPostRequest group)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = service.UpdateProductGroup(group.ConvertToProductGroupViewModel());
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ServerError", "Update group fail!");
                }
            }
            return View(group.ConvertToProductGroupViewModel());
        }

        // GET: /Admin/ProductGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ecom_ProductGroups ecom_productgroups = db.ecom_ProductGroups.Find(id);
            if (ecom_productgroups == null)
            {
                return HttpNotFound();
            }
            return View(ecom_productgroups);
        }

        // POST: /Admin/ProductGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isSuccess = service.DeleteProductGroup(id);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete group fail!");
            }
            return Redirect("Index");
        }

        #region Release resources

        /// <summary>
        /// Dispose database connection
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
