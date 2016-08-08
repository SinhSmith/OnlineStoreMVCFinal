using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Controllers
{
    public class BaseController : Controller
    {
        #region properties

        public IDisplayProductService service = new DisplayProductService();

        public BaseController()
        {
            service.RefreshAll();
        }

        #endregion

        #region private functions

        /// <summary>
        /// Create list new products in system
        /// </summary>
        protected void PopulateNewProductList()
        {
            ViewBag.NewProductList = service.GetAllNewProduct();
        }

        /// <summary>
        /// Create list best sell products in system
        /// </summary>
        protected void PopulateBestSellProductList()
        {
            ViewBag.BestSellProductList = service.GetAllBestSellProduct();
        }

        /// <summary>
        /// Create list products have  high priority order in system
        /// </summary>
        protected void PopulateHighPriorityOrderProductList()
        {
            ViewBag.HighPriorityOrderProductList = service.GetListHighPriorityOrderProduct();
        }

        /// <summary>
        /// Create list category using in top vertical menu bar
        /// </summary>
        protected void PopulateCategoryList()
        {
            //ViewBag.TopCategoryList = service.GetTopCategories().Take(7);
            ViewBag.CategoryList = (new MenuService()).GetMenuByType((int)OnlineStore.Infractructure.Utility.Define.MenuEnum.User);
        }

        /// <summary>
        /// Create list category using in top horizontal menu bar
        /// </summary>
        protected void PopulateTopCategoryList()
        {
            //ViewBag.TopCategoryList = service.GetTopCategories().Take(7);
            ViewBag.TopCategoryList = (new MenuService()).GetMenuByType((int)OnlineStore.Infractructure.Utility.Define.MenuEnum.User).Take(6).ToList();
        }

        #endregion
    }
}