using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moves.Net;
using Moves.App.Helpers;
using Moves.App.Helpers.ActionFilters;

namespace Moves.App.Controllers
{
    public class AppController : Controller
    {
		[MovesAuthenticate(new[] { "activity", "location"})]
        public ActionResult Start()
        {
			var places = MovesApplication.MovesService.Places.GetByMonth(2014, 4);

			return View(places);
		}
    }
}
