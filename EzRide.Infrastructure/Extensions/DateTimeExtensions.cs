using System;

namespace EzRide.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixEpochTime(this DateTime dateTime)
        {
            TimeSpan span = new TimeSpan(DateTime.UnixEpoch.Ticks);
            DateTime time = dateTime.Subtract(span);

            return time.Ticks / 10000;
        }
    }
}