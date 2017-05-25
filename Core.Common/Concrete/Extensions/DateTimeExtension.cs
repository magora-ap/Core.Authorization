using System;

namespace Core.Authorization.Common.Concrete.Extensions
{
    public static class DateTimeExtension
    {
        public static long UnixDateTime(this DateTime date)
        {
            return (long)Math.Floor((date - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        }

        public static TimeSpan? AsTimeSpan(this long? time)
        {
            if (time.HasValue)
                return new TimeSpan(0, 0, (int)time);
            return null;
        }
    }
}
