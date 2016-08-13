using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BaseManagementController : Controller
    {
        #region Properties

        protected IProductService service = new ProductService();

        #endregion

        #region Constructures

        public BaseManagementController()
        {
            service = new ProductService();
            //service.RefreshAll();
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get list options for Status dropdownlist and assign to Variable in ViewBag of view
        /// </summary>
        /// <param name="status"></param>
        protected virtual void PopulateStatusDropDownList(Define.Status status = Define.Status.Active)
        {
            IEnumerable<Define.Status> values = Enum.GetValues(typeof(Define.Status)).Cast<Define.Status>();
            IEnumerable<SelectListItem> items = from value in values
                                                where value != Define.Status.Delete
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.Status)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == status,
                                                };

            ViewBag.Status = items;
        }

        /// <summary>
        /// Get list options for banner types dropdownlist and assign to Variable in ViewBag of view
        /// </summary>
        /// <param name="status"></param>
        protected virtual void PopulateBannerTypesDropDownList(Define.BannerTypes banner = Define.BannerTypes.SpringSeason)
        {
            IEnumerable<Define.BannerTypes> values = Enum.GetValues(typeof(Define.BannerTypes)).Cast<Define.BannerTypes>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.BannerTypes)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == banner,
                                                };

            ViewBag.Type = items;
        }

        #endregion

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