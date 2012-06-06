using System;
using System.Collections.Generic;

using System.Text;
using System.Globalization;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Helper class to parse strings received in Hoiio API calls
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// Convert a date to a format recognized by Hoiio API
        /// </summary>
        public static string dateToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Convert a datestring returned by Hoiio API to the correcct DateTime type
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime stringToDate(string date)
        {
            DateTime myDateVar;

            if (!DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out myDateVar))
                DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss.f",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out myDateVar);
                
            return myDateVar;
        }

    }
}
