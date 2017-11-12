using System;

namespace Salary.Core.Helper
{
    public static class UnixEpochExtension
    {
        private static DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// This method allows you to convert the time from Unix Time Stamp to Windows DateTime format
        /// </summary>
        /// <param name="unixTimeStamp">Unix Time Stamp Date and time</param>
        /// <returns>Windows DateTime format</returns>
        public static DateTime ToDateTime(this int unixTimeStamp)
        {
            return origin.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        /// <summary>
        /// This method allows you to convert the time from Windows DateTime format to  Unix Time Stamp
        /// </summary>
        /// <param name="dateTime">Windows DateTime</param>
        /// <returns>Unix Time Stamp format</returns>
        public static int ToUnixTimeStamp(this DateTime dateTime)
        {
            TimeSpan diff = dateTime.ToUniversalTime() - origin;
            return (int)Math.Floor(diff.TotalSeconds);
        }
    }
}
