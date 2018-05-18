using System.Web;
using System.Web.Optimization;

namespace EniProjetMvc
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/AdminLTE.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/_all-skins.min.css",
                      "~/Content/morris.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/ckeditor.css",
                      "~/Content/bootstrap-clockpicker.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                       "~/Scripts/jquery-ui.min.js",
                       "~/Scripts/jquery.knob.min.js",
                       "~/Scripts/moment.min.js",
                       "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/adminlte.js",
                      "~/Scripts/bootstrap-clockpicker.min.js",
                      "~/Scripts/notify.min.js",
                      "~/Scripts/custom.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                       "~/Scripts/jquery.ui.widget.js",
                       "~/Scripts/jquery.iframe-transport.js",
                       "~/Scripts/jquery.fileupload.js"
                      ));
        }
    }
}
