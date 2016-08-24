using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineStoreMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Contact", url: "lien-he", defaults: new { controller = "Home", action = "Contact" }, namespaces: new[] { "OnlineStoreMVC.Controllers" });
            routes.MapRoute(name: "About", url: "gioi-thieu", defaults: new { controller = "Home", action = "About" }, namespaces: new[] { "OnlineStoreMVC.Controllers" });
            routes.MapRoute(name: "News", url: "tin-tuc", defaults: new { controller = "News", action = "Index", id = 1 });
            routes.MapRoute(name: "Blog", url: "blog", defaults: new { controller = "News", action = "Index", id = 5 });
            routes.MapRoute(name: "Login", url: "dang-nhap", defaults: new { controller = "Account", action = "Login" });
            routes.MapRoute(name: "Register", url: "dang-ky", defaults: new { controller = "Account", action = "Register" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineStoreMVC.Controllers" }
            );
        }
    }
}
