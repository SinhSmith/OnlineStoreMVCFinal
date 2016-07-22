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

namespace OnlineStoreMVC.Controllers
{
    public class NewsController : Controller
    {
        private OnlineStoreMVCEntities db = new OnlineStoreMVCEntities();
        private ICMSNewsService _cmsNewsService = new CMSNewsService();
        private ICMSCategoryService _cmsCategoryService = new CMSCategoryService();

        private void PopulateCMSChildCategoriesByParentId(int parentId)
        {
            ViewBag.ChildCategories = _cmsCategoryService.GetChildCategoriesByParentId(parentId);
        }

        private void PopulateRecentNews()
        {
            ViewBag.RecentNews = _cmsNewsService.GetRecentCMSNews();
        }

        private void PopulateRelatedNews(int id)
        {
            ViewBag.RelatedNews = _cmsNewsService.GetRelatedCMSNews(id);
        }

        // GET: /News/
        public ActionResult Index(int? id, int page = 1)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PopulateCMSChildCategoriesByParentId(id.Value);
            PopulateRecentNews();

            int totalItems = 0;
            var news = _cmsNewsService.GetCMSNewsByCategoryId(id.Value, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);
            IPagedList<CMSNewsView> pageNews = new StaticPagedList<CMSNewsView>(news, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);

            return View(pageNews);
        }

        // GET: /News/Details/5
        public ActionResult Details(int? id)
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

            PopulateCMSChildCategoriesByParentId(news.CategoryId);
            PopulateRecentNews();
            PopulateRelatedNews(id.Value);

            return View(news);
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
