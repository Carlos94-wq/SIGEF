using System.Web;
using System.Web.Optimization;

namespace SIGEF.Presentacion
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/Datatables/js/jquery.dataTables.js",
                        "~/Scripts/Datatables/js/dataTables.bootstrap4.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/UI/js").Include(
                    "~/Scripts/Ui.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Scripts/Datatables/css/datatables.bootstrap4.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/LayOutHome/css").Include(
                "~/Content/sidenav.css",
                "~/Content/LayOut.css"));
        }
    }
}
