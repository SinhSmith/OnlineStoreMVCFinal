using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class HomeController : BaseManagementController
    {
        #region Properties

        private IMenuService _menuService = new MenuService();

        #endregion

        #region Actions
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var menu = _menuService.GetMenuByType((int)OnlineStore.Infractructure.Utility.Define.MenuEnum.Admin);
            return PartialView(menu);
        }

        #endregion
    }
}
