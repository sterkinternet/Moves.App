using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moves.App.Helpers.Extensions
{
    public static class DateTimeExtensions
    {
        public static IEnumerable<DateTime> MonthsInRange(this DateTime start, DateTime end)
        {
            for (DateTime date = start; date <= end; date = date.AddMonths(1))
            {
                yield return date;
            }
        }
    }
}