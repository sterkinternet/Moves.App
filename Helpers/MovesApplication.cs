using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Moves.Net;
using System.Linq;

namespace Moves.App.Helpers {
	public class MovesApplication : HttpApplication {
		private const string MovesServiceKey = "MOVES_SERVICE";
		private const string AccessTokenKey = "ACCESS_TOKEN";
		
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}		

		protected void Application_BeginRequest(object sender, EventArgs e) {
			var accessToken = GetAccessToken();
			var clientId = ConfigurationManager.AppSettings["ClientId"];
			var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];			
			var service = new MovesService(clientId, clientSecret, accessToken);

			var authorizationToken = HttpContext.Current.Request.QueryString["code"];
			if (!string.IsNullOrEmpty(authorizationToken)) {
				var token = service.Authentication.ReceiveAccessToken(authorizationToken, null);
				SetAccessToken(token.Data.AccessToken);
				service = new MovesService(clientId, clientSecret, token.Data.AccessToken);
			}
			HttpContext.Current.Items[MovesServiceKey] = service;
		}

		protected void Application_EndRequest(object sender, EventArgs e) {
			HttpContext.Current.Items.Remove(MovesServiceKey);
		}

		public static MovesService MovesService {
			get {
				return (MovesService)HttpContext.Current.Items[MovesServiceKey];
			}
		}

		public static bool HasToken {
			get {
				return MovesService.Credentials != null && !string.IsNullOrEmpty(MovesService.Credentials.AccessToken);
			}
		}

		private string GetAccessToken() {
			if (HttpContext.Current.Request.Cookies.AllKeys.Contains(AccessTokenKey)) {
				return HttpContext.Current.Request.Cookies[AccessTokenKey].Value;
			}
			return null;
		}

		private void SetAccessToken(string accessToken) {
			var cookie = new HttpCookie(AccessTokenKey) { 
				Value = accessToken 
			};			
			HttpContext.Current.Response.SetCookie(cookie);
		}
	}
}