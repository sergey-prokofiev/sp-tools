using System;

namespace SpTools.Wrappers
{
    /// <summary>
    /// Wraps date time static members to be reused for testing.
    /// </summary>
    public interface IDateTimeProxy
    {
        /// <summary>
        /// return current date/time
        /// </summary>
        /// <returns></returns>
        DateTime Now();

        /// <summary>
        /// Returns current date/time as utc
        /// </summary>
        /// <returns></returns>
        DateTime UtcNow();

        /// <summary>
        /// Returns current offset of the current time zone from utc. Date lights settongs are considered.
        /// </summary>
        /// <returns></returns>
        TimeSpan CurrentOffset();
    }
}