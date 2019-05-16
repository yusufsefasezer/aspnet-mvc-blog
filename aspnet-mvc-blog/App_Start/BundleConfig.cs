using System.Web;
using System.Web.Optimization;

namespace aspnet_mvc_blog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/admin").Include("~/Scripts/sb-admin.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Style.css", "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/admin").Include("~/Content/sb-admin.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
