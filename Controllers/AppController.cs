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
			var places = MovesApplication.MovesService.Places.GetByMonth(2014, 4);
			throw new MovesException("a", HttpStatusCode.Unauthorized);
			return View(places);
		}


		public ActionResult Unauthorized() {
			return Content("Unauthorized");
		}
    }
}
