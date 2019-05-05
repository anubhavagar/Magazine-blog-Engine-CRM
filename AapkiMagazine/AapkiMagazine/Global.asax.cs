using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace AapkiMagazine
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahMVCErrorFilter());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "PostDetail",  // Route name
               "Post/{articleId}/{articleTitle}", // URL with parameters
               new { controller = "Post", action = "Index", articleTitle = UrlParameter.Optional }  // Parameter defaults
           );

            routes.MapRoute(
               "PostsByCategory",  // Route name
               "Section/{categoryType}", // URL with parameters
               new { controller = "Section", action = "Index"}  // Parameter defaults
           );

            routes.MapRoute(
               "PostsByTag",  // Route name
               "Tag/{tagged}", // URL with parameters
               new { controller = "Tag", action = "Index" }  // Parameter defaults
           );
          

            routes.MapRoute(
        "Admin_elmah",
        "Admin/elmah/{type}",
        new { action = "Index", controller = "Elmah", type = UrlParameter.Optional }
    );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

           

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //RegisterBundles(BundleTable.Bundles);
            //Enabling Bundling and Minification
            //BundleTable.EnableOptimizations = true;
        }

        //public static void RegisterBundles(BundleCollection bundles)
        //{
        //    bundles.UseCdn = true;   //enable CDN support

        //    //add link to jquery on the CDN
        //    var googleapisCdnPath = "http://fonts.googleapis.com/css?family=Droid+Sans:400,700|Lato:300,400,700,400italic,700italic|Droid+Serif";
        //    var jquerycdn = "//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js";
            
        //    bundles.Add(new StyleBundle("~/bundles/googlefont",
        //        googleapisCdnPath).Include(
        //        "~/Content/css/google_font.css"));

        //    //Creating bundle for css files

        //    bundles.Add(new StyleBundle("~/Content/css1_mainhuaamadmi").Include(
        //        "~/Scripts/js/jquery-1.10.1.min.js",
        //      "~/Content/vendor/bootstrap/css/bootstrap.min.css",
        //      "~/Content/vendor/font-awesome/css/font-awesome.min.css",
        //      "~/Content/vendor/prettyPhoto/css/prettyPhoto.css"
        //  ));


        //    bundles.Add(new StyleBundle("~/Content/css2_mainhuaamadmi").Include(
        //        "~/Content/css/base.css",
        //        "~/Content/css/components.css",
        //        "~/Content/colorschemes/default.css"

        //    ));

        //    bundles.Add(new ScriptBundle("~/Content/scripts1_maihunaamaadmi").Include(               
        //       "~/Content/vendor/modernizr-2.6.2-respond-1.1.0.min.js",               
        //       "~/Scripts/js/jquery-1.10.1.min.js"               
        //   ));


        //    bundles.Add(new ScriptBundle("~/bundles/jqueryjs",
        //        jquerycdn).Include(
        //        "~/Scripts/js/jquery-1.10.1.min.js"));


        //    //Creating bundle for js files
        //    bundles.Add(new ScriptBundle("~/Content/scripts2_maihunaamaadmi").Include(
        //        "~/Content/vendor/modernizr-2.6.2-respond-1.1.0.min.js",
        //        "~/Scripts/js/bootstrap.min.js",
        //        "~/Content/vendor/prettyPhoto/jquery.prettyPhoto.js",
        //        "~/Content/vendor/jquery.unveil.min.js",
        //        "~/Scripts/js/jquery.ticker.min.js",
        //        "~/Scripts/js/main.js"
        //    ));
        //}
    }


}