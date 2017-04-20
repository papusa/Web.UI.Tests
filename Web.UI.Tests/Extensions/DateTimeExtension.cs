using System;

namespace Web.UI.Tests.Extensions {
    static class DateTimeExtension {
        /// <summary>
        /// Generates time stamp based on current date time
        /// </summary>
        /// <returns>Time stamp of type string. 2009-06-15T13:45:30.6175400 -> 61754</returns>
        public static string GetTimeStamp(this DateTime dateTime) {
            return DateTime.Now.ToString("FFFFF"); 
        }
    }
}
