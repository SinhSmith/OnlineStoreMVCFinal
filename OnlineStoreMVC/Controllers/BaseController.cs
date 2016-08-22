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
        #region Properties

        public IDisplayProductService service = new DisplayProductService();

        #endregion
        
        #region Constructures

        public BaseController()
        {
            service = new DisplayProductService();
            //service.RefreshAll();
        }

        #endregion

        #region Private functions

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
        /// Create list products have high priority order in system
        /// </summary>
        protected void PopulateHighPriorityOrderProductList()
        {
            ViewBag.HighPriorityOrderProductList = service.GetListHighPriorityOrderProduct();
        }

        /// <summary>
        /// Create list classic style product using in top horizontal menu bar
        /// </summary>
        protected void PopulateClassicStyleProductList()
        {
            ViewBag.ClassicStyleProductList = service.GetListClassicStyleProduct();
        }

        /// <summary>
        /// Create list modern style product using in top horizontal menu bar
        /// </summary>
        protected void PopulateModernStyleProductList()
        {
            ViewBag.ModernStyleProductList = service.GetListModernStyleProduct();
        }

        /// <summary>
        /// Create list category using in top vertical menu bar
        /// </summary>
        protected void PopulateCategoryList()
        {
            //ViewBag.CategoryList = (new MenuService()).GetMenuByType((int)OnlineStore.Infractructure.Utility.Define.MenuEnum.User);
            ViewBag.CategoryList = service.GetTopCategories();
        }

        /// <summary>
        /// Create list category using in top horizontal menu bar
        /// </summary>
        protected void PopulateTopCategoryList()
        {
            ViewBag.TopCategoryList = (new MenuService()).GetMenuByType((int)OnlineStore.Infractructure.Utility.Define.MenuEnum.User).Take(6).ToList();
        }

        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            //service.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}