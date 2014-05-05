using System.Web.Mvc;

namespace Moves.App.Helpers.ActionFilters {
	public class MovesAuthenticateAttribute : ActionFilterAttribute {
		public string[] Scopes { get; private set; }
		public MovesAuthenticateAttribute(string[] scopes) {
			this.Scopes = scopes;
		}
		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			if (!MovesApplication.HasToken) { 
				var url = MovesApplication.Client.GetAuthorizationUrl(this.Scopes, MovesApplication.GetBaseUrl());
				filterContext.RequestContext.HttpContext.Response.Redirect(url);
				filterContext.RequestContext.HttpContext.Response.End();
			}
		}		
	}
}