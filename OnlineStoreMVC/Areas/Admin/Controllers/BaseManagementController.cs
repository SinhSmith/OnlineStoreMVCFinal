using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
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
    }
}