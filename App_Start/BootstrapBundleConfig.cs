using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(ScrumBoard.App_Start.BootstrapBundleConfig), "RegisterBundles")]

namespace ScrumBoard.App_Start
{
	public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{	
	        // Bootstrap
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js")
            );            

            //Bootstrap            
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css").Include("~/Content/app.css"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include("~/Content/bootstrap-theme.css"));              

            //Jquery
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-1.9.1.min.js")
                .Include("~/Scripts/jquery.validate.min.js")
            );

            //Angular
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/angular")                
                .Include("~/Scripts/underscore-min.js")
                .Include("~/Scripts/angular.min.js")
                .Include("~/Scripts/angular-route.min.js")
                .Include("~/Scripts/angular-google-maps.min.js")
                .Include("~/Scripts/moment-with-langs.min.js")
            );                                    
            
            //App
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/app")                
                .IncludeDirectory("~/App/Libraries", "*.js")
                .IncludeDirectory("~/App/Modules", "*.js")                
                .Include("~/App/app.js")
                .IncludeDirectory("~/App/Controllers", "*.js")
                .IncludeDirectory("~/App/Directives", "*.js")
            );           
		}
	}
}
