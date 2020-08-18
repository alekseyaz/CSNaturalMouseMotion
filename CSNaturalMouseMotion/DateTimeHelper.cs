
using System;

internal static class DateTimeHelper
{
    public static DateTime Jan1st19701 { get; } = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

    public static long CurrentUnixTimeMillis()
    {
        return (long)(DateTime.UtcNow - Jan1st19701).TotalMilliseconds;
    }
}