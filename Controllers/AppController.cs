using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moves.Net;
using Moves.App.Helpers;
using Moves.App.Helpers.ActionFilters;
using System.Net;
using System.Globalization;
using Moves.App.Helpers.Extensions;
using System.Data.Entity.Spatial;

namespace Moves.App.Controllers
{
	[MovesException(HttpStatusCode.Unauthorized, "Unauthorized")]
	public class AppController : Controller
    {
		[MovesAuthenticate(new[] { "activity", "location"})]
        public ActionResult Start()
        {
			return RedirectToAction("Places");
		}

		public ActionResult Places() {
			return View();
		}

		public ActionResult Storyline() {
			return View();
		}

        public ActionResult Summary()
        {
            return View();
        }

		public ActionResult Logoff() {
			MovesApplication.Logoff();
			return RedirectToAction("Start");
		}

        public void Import()
        {
            var context = new MovesAppEntities();

            var user = Client.Profile.GetUser();
            var start = DateTime.ParseExact(user.Data.Profile.FirstDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            var months = start.MonthsInRange(DateTime.Today);
            
            foreach(var month in months) {
                var days = Client.Places.GetByMonth(month.Year, month.Month);
                if (days != null)
                {
                    var places = days.Data
                        .SelectMany(d => d.Segments)
                        .Select(s => s.Place)
                        .Where(p => p.Name != null && p.Location != null)
                        .GroupBy(p => p.Name)
                        .Select(p => p.First())
                        .Distinct()
                    ;
                    if (places != null)
                    {
                        foreach (var place in places)
                        {
                            var placeId = Int32.Parse(place.Id);
                            if (!context.Places.Any(p => p.PlaceId == placeId)) 
                            {
                                context.Places.Add(new Place
                                {
                                    PlaceId = placeId,
                                    Name = place.Name,
                                    Latitude = place.Location.Latitude,
                                    Longitude = place.Location.Longitude,
                                    Location = DbGeography.FromText(string.Format(CultureInfo.InvariantCulture, "POINT({0} {1})", place.Location.Latitude, place.Location.Longitude))
                                });
                                context.SaveChanges();
                            }
                        }
                    }
                }            
            }            
        }


		public ActionResult Unauthorized() {
			return Content("Unauthorized");
		}

        public MovesClient Client {
            get
            {
                return MovesApplication.Client;
            }
        }
    }
}
