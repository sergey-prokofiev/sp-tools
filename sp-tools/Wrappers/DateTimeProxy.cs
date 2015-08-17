using System;

namespace SpTools.Wrappers
{
    /// <summary>
    /// Implements IDateTimeProxy
    /// </summary>
    public class DateTimeProxy : IDateTimeProxy
    {
        /// <summary>
        /// return current date/time
        /// </summary>
        public DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Returns current date/time as utc
        /// </summary>
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// Returns current offset of the current time zone from utc. Daylight saving is considered.
        /// </summary>
        public TimeSpan CurrentOffset()
        {
            return DateTimeOffset.Now.Offset;
        }
    }
}