using System.Web.Mvc;

namespace OnlineStoreMVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(name: "Admin", url: "admin", defaults: new { controller = "Home", action = "Index" });
            context.MapRoute(name: "Admin_CMSCategory", url: "admin/danh-muc-tin-tuc", defaults: new { controller = "CMSCategory", action = "Index" });
            context.MapRoute(name: "Admin_CMSNews", url: "admin/tin-tuc", defaults: new { controller = "CMSNews", action = "Index" });
            context.MapRoute(name: "Admin_Category", url: "admin/danh-muc-san-pham", defaults: new { controller = "Category", action = "Index" });
            context.MapRoute(name: "Admin_Brand", url: "admin/thuong-hieu", defaults: new { controller = "Brand", action = "Index" });
            context.MapRoute(name: "Admin_Product", url: "admin/san-pham", defaults: new { controller = "Product", action = "Index" });
            context.MapRoute(name: "Admin_Profile", url: "admin/nguoi-dung", defaults: new { controller = "Profile", action = "Index" });
            context.MapRoute(name: "Admin_Banner", url: "admin/banner", defaults: new { controller = "Banner", action = "Index" });
            context.MapRoute(name: "Admin_ProductGroup", url: "admin/nhom-san-pham", defaults: new { controller = "ProductGroup", action = "Index" });
            
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineStoreMVC.Areas.Admin.Controllers" }
            );
        }
    }
}