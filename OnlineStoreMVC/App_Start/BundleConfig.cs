using System.Web;
using System.Web.Optimization;

namespace OnlineStoreMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminScript").Include(
                       "~/Scripts/Utility/common.js",
                        "~/Content/chosen-library/chosen.jquery.min.js"
                         //"~/Content/custom-file-input/custom-file-input.js",
                         //"~/Content/custom-file-input/jquery.custom-file-input.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/others").Include(
                      "~/Content/dist/js/app.min.js",
                      "~/Content/respond/1.4.2/respond.min.js",
                      "~/Content/html5shiv/3.7.2/html5shiv.min.js"));

            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                      "~/Content/bootstrap.css",  
                      "~/Content/font-awesome/4.3.0/css/font-awesome.min.css",
                      "~/Content/ionicons/2.0.1/css/ionicons.min.css",
                      "~/Content/dist/css/AdminLTE.css",
                      "~/Content/dist/css/AdminLTE.addon.css",
                      "~/Content/dist/css/skins/skin-green.min.css",
                      "~/Content/common.css",
                      "~/Content/chosen-library/chosen.min.css"
                      //"~/Content/custom-file-input/component.css",
                      //"~/Content/custom-file-input/normalize.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/brandManagement").Include(
                       "~/Scripts/Admin/brand-management.js"
                      ));
        }
    }
}
