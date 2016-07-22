using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            PopulateNewProductList();
            PopulateBestSellProductList();            
            PopulateHighPriorityOrderProductList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _HeaderPartial()
        {
            PopulateCategoryList();
            return PartialView();
        }

        public ActionResult BannerPartial()
        {
            IBannerService _bannerService = new BannerService();
            return PartialView(_bannerService.GetBannersForHomePage());
        }

        public ActionResult BlogPartial()
        {
            ICMSNewsService _cmsNewsService = new CMSNewsService();
            return PartialView(_cmsNewsService.GetCMSNewsForHomePage());
        }
    }
}