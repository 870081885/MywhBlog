using System.Web;
using System.Web.Optimization;

namespace mywhBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/mywh/js").Include(
                 "~/Scripts/jquery.min.js",
                 "~/Scripts/custom.js",
                 "~/Scripts/offcanvas.min.js"
            ));

            bundles.Add(new StyleBundle("~/mywh/css").Include(
                "~/assets/bootstrap/css/bootstrap.min.css",
                "~/fonts/css/font-awesome.min.css",
                "~/Content/css/offcanvas.min.css",
                "~/Content/css/style.css",
                "~/Content/css/index.css"
            ));
        }
    }
}
