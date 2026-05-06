using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.Extension
{
    public static class DateTimeExtensions
    {
        public static string ToTimeAgo(this DateTime dateTime)
        {
            // Ensure we work with UTC to avoid timezone issues
            var ts = DateTime.UtcNow - dateTime.ToUniversalTime();

            if (ts.TotalSeconds < 0)
                return "in the future"; // Handles future dates

            if (ts.TotalSeconds < 60)
                return $"{ts.Seconds} second{(ts.Seconds != 1 ? "s" : "")} ago";

            if (ts.TotalMinutes < 60)
                return $"{ts.Minutes} minute{(ts.Minutes != 1 ? "s" : "")} ago";

            if (ts.TotalHours < 24)
                return $"{ts.Hours} hour{(ts.Hours != 1 ? "s" : "")} ago";

            if (ts.TotalDays < 7)
                return $"{ts.Days} day{(ts.Days != 1 ? "s" : "")} ago";

            if (ts.TotalDays < 30)
            {
                int weeks = (int)(ts.TotalDays / 7);
                return $"{weeks} week{(weeks != 1 ? "s" : "")} ago";
            }

            if (ts.TotalDays < 365)
            {
                int months = (int)(ts.TotalDays / 30);
                return $"{months} month{(months != 1 ? "s" : "")} ago";
            }

            int years = (int)(ts.TotalDays / 365);
            return $"{years} year{(years != 1 ? "s" : "")} ago";
        }
    }
}
