using System.Web.Optimization;

namespace MusicStore.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/vendors/modernizr-2.8.3.js"
                ));
            
            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/vendors/jquery-3.1.1.min.js",
                "~/Scripts/vendors/bootstrap.min.js",
                "~/Scripts/vendors/toastr.min.js",
                "~/Scripts/vendors/jquery.raty.js",
                "~/Scripts/vendors/respond.min.js",
                "~/Scripts/vendors/respond.matchmedia.min.js",
                "~/Scripts/vendors/angular.js",
                "~/Scripts/vendors/angular-route.min.js",
                "~/Scripts/vendors/angular-cookies.min.js",
                "~/Scripts/vendors/angular-validator.min.js",
                "~/Scripts/vendors/ui-bootstrap-tpls.min.js",
                "~/Scripts/vendors/underscore.min.js",
                "~/Scripts/vendors/raphael.js",
                "~/Scripts/vendors/morris.js",
                "~/Scripts/vendors/jquery.fancybox.js",
                "~/Scripts/vendors/jquery.fancybox-media.js",
                "~/Scripts/vendors/loading-bar.min.js",
                "~/Scripts/vendors/ng-file-upload.min.js",
                "~/Scripts/vendors/angular-animate.min.js",
                "~/Scripts/vendors/angular-aria.min.js",
                "~/Scripts/vendors/angular-material.min.js"
                ));
            
            
            bundles.Add(new ScriptBundle("~/bundles/spa/modules").IncludeDirectory("~/Scripts/spa/modules", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/spa/main").IncludeDirectory("~/Scripts/spa", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/spa/services").IncludeDirectory("~/Scripts/spa/services", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/spa/directives").IncludeDirectory("~/Scripts/spa/directives", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/spa/controllers").IncludeDirectory("~/Scripts/spa/controllers", "*.js"));

            bundles.Add(new StyleBundle("~/css").IncludeDirectory("~/Content/css", "*.css"));

            /*
            bundles.Add(new StyleBundle("~/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/bootstrap-theme.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/morris.css",
                "~/Content/css/toastr.min.css",
                "~/Content/css/jquery.fancybox.css",
                "~/Content/css/loading-bar.min.css",
                "~/Content/css/Site.css"
                ));
            */
            BundleTable.EnableOptimizations = false;
        }
    }
}
