using System;

namespace Core.Authorization.Common.Concrete.Helpers
{
    public static class Utils
    {
        public static string NormalizeString(string val)
        {
            if (string.IsNullOrEmpty(val)) return null;
            return val.Replace("'", "''");
        }

        public static TimeSpan ConvertOffset(TimeSpan? offset)
        {
            return offset ?? new TimeSpan();
        }
    }
}
