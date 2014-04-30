using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moves.App.Helpers;
using Moves.Net.Model;

namespace Moves.App.Api
{
    public class ApiController : System.Web.Http.ApiController
    {
		[HttpGet]
		public IEnumerable<Day> Places(int year, int month) {
			var days = MovesApplication.MovesService.Places.GetByMonth(year, month);
			return days.Data;
		}
    }
}
