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

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class ProductGroupController : Controller
    {
        #region Properties

        private OnlineStoreMVCEntities db = new OnlineStoreMVCEntities();
        private IProductGroupService service = new ProductGroupService(); 
        #endregion

        // GET: /Admin/ProductGroup/
        public ActionResult Index(string keyword, int page = 1)
        {
            //int totalItems = 0;
            //var banners = service.GetBanners(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            //IPagedList<BannerViewModel> pageBanners = new StaticPagedList<BannerViewModel>(banners, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            //return View(pageBanners);


            return View(db.ecom_ProductGroups.ToList());
        }

        // GET: /Admin/ProductGroup/Details/5
        public ActionResult Details(int? id)
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

        // GET: /Admin/ProductGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/ProductGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Description,Status")] ecom_ProductGroups ecom_productgroups)
        {
            if (ModelState.IsValid)
            {
                db.ecom_ProductGroups.Add(ecom_productgroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ecom_productgroups);
        }

        // GET: /Admin/ProductGroup/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: /Admin/ProductGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Description,Status")] ecom_ProductGroups ecom_productgroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ecom_productgroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ecom_productgroups);
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ecom_ProductGroups ecom_productgroups = db.ecom_ProductGroups.Find(id);
            db.ecom_ProductGroups.Remove(ecom_productgroups);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
