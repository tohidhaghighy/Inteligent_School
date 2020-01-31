using System.Web;
using System.Web.Optimization;

namespace SchoolIntelligentWeb
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
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"
                     ,  "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/charts").Include(
                      "~/Scripts/chartjs/Chart.Core.js",
                      "~/Scripts/chartjs/Chart.Bar.js",
                      "~/Scripts/chartjs/Chart.Doughnut.js",
                      "~/Scripts/chartjs/Chart.Line.js",
                      "~/Scripts/chartjs/Chart.PolarArea.js",
                      "~/Scripts/chartjs/Chart.Radar.js"));

            bundles.Add(new ScriptBundle("~/Content/Loginjs").Include(
                     "~/Scripts/jquery-1.12.1.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery-{version}.js",
                     "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/jquery.unobtrusive-ajax.js"
                     , "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new StyleBundle("~/Content/Logincss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/NavbarFirstpage.css",
                      "~/Content/footer-distributed-with-address-and-phones.css","~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/Content/Mainjs").Include(
                    "~/Scripts/jquery-1.10.2.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/CarouselFirstPage.js"));

             bundles.Add(new StyleBundle("~/Content/Maincss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/CarouselFirstPage.css",
                      "~/Content/footer-distributed-with-address-and-phones.css", "~/Content/NavbarFirstpage.css"
                      , "~/Content/timeline.css"));

            //---------------------------panel------------------------------
             bundles.Add(new ScriptBundle("~/Content/Paneljs").Include(
                     "~/Scripts/jquery-1.12.4.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/HeaderImage.js"));

             bundles.Add(new StyleBundle("~/Content/Panelcss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/PanelItems.css",
                      "~/Content/footer-distributed-with-address-and-phones.css", "~/Content/NavbarFirstpage.css"
                      , "~/Content/HeaderImage.css","~/Content/buttonmenu.css"
                      , "~/Content/Membership.css", "~/Content/Chat.css"));

            //---------------------------end panel -------------------------------

             bundles.Add(new StyleBundle("~/bundles/dropzone/css").Include(
                       "~/Scripts/dropzone/basic.css",
                       "~/Scripts/dropzone/dropzone.css"));

             bundles.Add(new ScriptBundle("~/bundles/js/dropzone").Include(
           "~/Scripts/dropzone/dropzone.js", "~/Scripts/Noty/jquery.noty.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
