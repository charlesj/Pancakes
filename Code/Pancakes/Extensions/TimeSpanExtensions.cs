namespace Pancakes.Extensions
{
    using System;
    using System.Text;

    /// <summary>
    /// The time span extensions.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// The in milliseconds.
        /// </summary>
        /// <param name="ts">
        /// The ts.
        /// </param>
        /// <returns>
        /// The System.Int32.
        /// </returns>
        public static int InMilliseconds(this TimeSpan ts)
        {
            return (int)ts.TotalMilliseconds;
        }

        /// <summary>
        /// The ago.
        /// </summary>
        /// <param name="ts">
        /// The ts.
        /// </param>
        /// <returns>
        /// The System.DateTime.
        /// </returns>
        public static DateTime Ago(this TimeSpan ts)
        {
            return DateTime.Now - ts;
        }

        /// <summary>
        /// The from now.
        /// </summary>
        /// <param name="ts">
        /// The ts.
        /// </param>
        /// <returns>
        /// The System.DateTime.
        /// </returns>
        public static DateTime FromNow(this TimeSpan ts)
        {
            return DateTime.Now + ts;
        }

        /// <summary>
        /// Generates a string following this format X days x hours x minutes x seconds, only including the components that are not zero.
        /// </summary>
        /// <param name="ts">
        /// The timespan to use
        /// </param>
        /// <returns>
        /// The System.String.
        /// </returns>
        public static string ToWords(this TimeSpan ts)
        {
            var sb = new StringBuilder();
            if (ts.Days != 0)
            {
                sb.Append(string.Format("{0} {1} ", ts.Days, ts.Days.Pluralize("day", "days")));
            }

            if (ts.Hours != 0)
            {
                sb.Append(string.Format("{0} {1} ", ts.Hours, ts.Hours.Pluralize("hour", "hours")));
            }

            if (ts.Minutes != 0)
            {
                sb.Append(string.Format("{0} {1} ", ts.Minutes, ts.Minutes.Pluralize("minute", "minutes")));
            }

            if (ts.Seconds != 0)
            {
                sb.Append(string.Format("{0} {1} ", ts.Seconds, ts.Seconds.Pluralize("second", "seconds")));
            }

            return sb.ToString();
        }
    }
}