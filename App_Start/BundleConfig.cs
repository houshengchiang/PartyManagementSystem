using System.Web;
using System.Web.Optimization;

namespace PartyManagementSystem
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert2").Include(
               "~/Scripts/sweetalert2.min.js"));

            // MDB5
            bundles.Add(new ScriptBundle("~/bundles/mdb5").Include(
               "~/Scripts/mdb/mdb.min.js"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/sweetalert2").Include(
                "~/Content/sweetalert2.min.css"));


            // MDB
            bundles.Add(new StyleBundle("~/Content/mdb5").Include(
                "~/Content/mdb/mdb.min.css"));
        }
    }
}
