using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Moves.Net;
using System.Linq;
using ScrumBoard.App_Start;
using Rsft.HttpCookieSecure;
using System.Web.Security;
using Moves.Net.Model;
using Newtonsoft.Json;

namespace Moves.App.Helpers {
	public class MovesApplication : HttpApplication {
		private const string MovesServiceKey = "MOVES_SERVICE";
		private const string AccessTokenKey = "ACCESS_TOKEN";
		
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BootstrapBundleConfig.RegisterBundles();
		}		

		protected void Application_BeginRequest(object sender, EventArgs e) {
			var accessToken = GetAccessToken();            
            var clientId = ConfigurationManager.AppSettings["ClientId"];
			var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            var service = new MovesClient(clientId, clientSecret, accessToken != null ? accessToken.AccessToken : null);

			var authorizationToken = HttpContext.Current.Request.QueryString["code"];
			if (!string.IsNullOrEmpty(authorizationToken)) {
                var redirectUrl = GetBaseUrl();
                var token = service.ReceiveAccessToken(authorizationToken, redirectUrl);
				SetAccessToken(token.Data);
                service = new MovesClient(clientId, clientSecret, token.Data.AccessToken);
			}
			HttpContext.Current.Items[MovesServiceKey] = service;
		}

		protected void Application_EndRequest(object sender, EventArgs e) {
			HttpContext.Current.Items.Remove(MovesServiceKey);
		}

		public static void Logoff() {
			var cookie = HttpContext.Current.Request.Cookies[AccessTokenKey];
			if (cookie != null) {
				HttpContext.Current.Response.Cookies[AccessTokenKey].Expires = DateTime.Today.AddDays(-1);
			}
			HttpContext.Current.Session.Abandon();
		}

		public static MovesClient Client {
			get {
                return (MovesClient)HttpContext.Current.Items[MovesServiceKey];
			}
		}

		public static bool HasToken {
			get {
                return Client.Credentials != null && !string.IsNullOrEmpty(Client.Credentials.AccessToken);
			}
		}

        public static string GetBaseUrl()
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

		private AccessTokenData GetAccessToken() {
			if (HttpContext.Current.Request.Cookies.AllKeys.Contains(AccessTokenKey)) {
				var cookie = HttpContext.Current.Request.Cookies[AccessTokenKey];
				var decrypted = CookieSecure.Decode(cookie, CookieProtection.Encryption);				
				return !string.IsNullOrEmpty(decrypted.Value) ? JsonConvert.DeserializeObject<AccessTokenData>(decrypted.Value) : null;				
			}
			return null;
		}

		private void SetAccessToken(AccessTokenData accessToken) {			
			var cookie = new HttpCookie(AccessTokenKey) { 				
				HttpOnly = true,
				Expires = DateTime.Today.AddSeconds(accessToken.ExpiresIn),
				Value = JsonConvert.SerializeObject(accessToken) 
			};						
			HttpContext.Current.Response.SetCookie(CookieSecure.Encode(cookie, CookieProtection.Encryption));
		}
	}
}