namespace Pancakes.Extensions
{
    using System;

    /// <summary>
    /// Extension methods for the integer class.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// The days.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The System.TimeSpan.
        /// </returns>
        public static TimeSpan Days(this int i)
        {
            return new TimeSpan(i, 0, 0, 0);
        }

        /// <summary>
        /// The hours.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The System.TimeSpan.
        /// </returns>
        public static TimeSpan Hours(this int i)
        {
            return new TimeSpan(0, i, 0, 0);
        }

        /// <summary>
        /// The minutes.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The System.TimeSpan.
        /// </returns>
        public static TimeSpan Minutes(this int i)
        {
            return new TimeSpan(0, 0, i, 0);
        }

        /// <summary>
        /// The seconds.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The System.TimeSpan.
        /// </returns>
        public static TimeSpan Seconds(this int i)
        {
            return new TimeSpan(0, 0, 0, i);
        }

        /// <summary>
        /// The milliseconds.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The System.TimeSpan.
        /// </returns>
        public static TimeSpan Milliseconds(this int i)
        {
            return new TimeSpan(0, 0, 0, 0, i);
        }

        /// <summary>
        /// Will return the appropriate plural for a given int.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <param name="singular">
        /// The Singular form
        /// </param>
        /// <param name="plural">
        /// The Plural form.
        /// </param>
        /// <returns>
        /// The System.String.
        /// </returns>
        public static string Pluralize(this int i, string singular, string plural)
        {
            if (i == 1 || i == -1)
            {
                return singular;
            }

            return plural;
        }
    }
}
