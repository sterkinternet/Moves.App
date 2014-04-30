using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moves.Net;
using Moves.App.Helpers;
using Moves.App.Helpers.ActionFilters;
using System.Net;

namespace Moves.App.Controllers
{
	[MovesException(HttpStatusCode.Unauthorized, "Unauthorized")]
	public class AppController : Controller
    {
		[MovesAuthenticate(new[] { "activity", "location"})]
        public ActionResult Start()
        {			
			return View();
		}


		public ActionResult Unauthorized() {
			return Content("Unauthorized");
		}
    }
}
