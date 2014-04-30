using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Moves.Net;
using System.Linq;

namespace Moves.App.Helpers.ActionFilters {
	public class MovesExceptionAttribute : FilterAttribute, IExceptionFilter
	{
		public HttpStatusCode StatusCode { get; private set; }
		public string TargetAction { get; private set; }

		public MovesExceptionAttribute(HttpStatusCode statusCode, string targetAction) {
			this.StatusCode = statusCode;
			this.TargetAction = targetAction;
		}

		public void OnException(ExceptionContext filterContext) {
			if (filterContext.ExceptionHandled || !(filterContext.Exception is MovesException))
				return;

			var movesException = (MovesException)filterContext.Exception;
			if (movesException.StatusCode != this.StatusCode)
				return;


			var result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {
				{ "action", this.TargetAction },
				{ "controller", filterContext.RouteData.GetRequiredString("controller") }
			});

			filterContext.Result = result;			
			filterContext.ExceptionHandled = true;
			filterContext.HttpContext.Response.Clear();
			//filterContext.HttpContext.Response.End();
		}		
	}
}