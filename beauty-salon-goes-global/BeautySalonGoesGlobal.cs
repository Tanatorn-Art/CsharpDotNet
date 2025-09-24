using System;
using System.Globalization;
using System.Runtime.InteropServices;

public enum Location
{
    NewYork,
    London,
    Paris
}

public enum AlertLevel
{
    Early,
    Standard,
    Late
}

public static class Appointment
{
    // 1. Convert UTC time to local time
    public static DateTime ShowLocalTime(DateTime dtUtc)
    {
        return dtUtc.ToLocalTime();
    }

    // 2. Schedule appointments in different locations, converting to UTC
    public static DateTime Schedule(string appointmentDateDescription, Location location)
    {
        DateTime localDateTime = DateTime.Parse(appointmentDateDescription);
        TimeZoneInfo timeZone = GetTimeZoneInfo(location);
        return TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZone);
    }

    // 3. Get alert times based on alert level
    public static DateTime GetAlertTime(DateTime appointment, AlertLevel alertLevel)
    {
        return alertLevel switch
        {
            AlertLevel.Early => appointment.AddDays(-1),
            AlertLevel.Standard => appointment.AddHours(-1).AddMinutes(-45),
            AlertLevel.Late => appointment.AddMinutes(-30),
            _ => appointment
        };
    }

    // 4. Check if daylight saving has changed in the last 7 days
    public static bool HasDaylightSavingChanged(DateTime dt, Location location)
    {
        TimeZoneInfo timeZone = GetTimeZoneInfo(location);
        DateTime sevenDaysAgo = dt.AddDays(-7);

        bool currentDst = timeZone.IsDaylightSavingTime(dt);
        bool previousDst = timeZone.IsDaylightSavingTime(sevenDaysAgo);

        return currentDst != previousDst;
    }

    // 5. Parse date-time string using location-appropriate culture
    public static DateTime NormalizeDateTime(string dtStr, Location location)
    {
        try
        {
            CultureInfo culture = GetCultureInfo(location);
            string[] formats = GetDateTimeFormats(location);

            foreach (string format in formats)
            {
                if (DateTime.TryParseExact(dtStr, format, culture, DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            return new DateTime(1, 1, 1);
        }
        catch
        {
            return new DateTime(1, 1, 1);
        }
    }

    // Helper method to get TimeZoneInfo based on location and operating system
    private static TimeZoneInfo GetTimeZoneInfo(Location location)
    {
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        return location switch
        {
            Location.NewYork => TimeZoneInfo.FindSystemTimeZoneById(
                isWindows ? "Eastern Standard Time" : "America/New_York"),
            Location.London => TimeZoneInfo.FindSystemTimeZoneById(
                isWindows ? "GMT Standard Time" : "Europe/London"),
            Location.Paris => TimeZoneInfo.FindSystemTimeZoneById(
                isWindows ? "W. Europe Standard Time" : "Europe/Paris"),
            _ => TimeZoneInfo.Utc
        };
    }

    // Helper method to get CultureInfo based on location
    private static CultureInfo GetCultureInfo(Location location)
    {
        return location switch
        {
            Location.NewYork => new CultureInfo("en-US"),
            Location.London => new CultureInfo("en-GB"),
            Location.Paris => new CultureInfo("fr-FR"),
            _ => CultureInfo.InvariantCulture
        };
    }

    // Helper method to get valid date-time formats for each location
    private static string[] GetDateTimeFormats(Location location)
    {
        return location switch
        {
            Location.NewYork => new[] { "M/d/yyyy H:mm:ss", "MM/dd/yyyy HH:mm:ss", "M/d/yyyy HH:mm:ss", "MM/d/yyyy H:mm:ss" },
            Location.London => new[] { "dd/MM/yyyy HH:mm:ss", "d/M/yyyy H:mm:ss", "dd/M/yyyy HH:mm:ss", "d/MM/yyyy H:mm:ss" },
            Location.Paris => new[] { "dd/MM/yyyy HH:mm:ss", "d/M/yyyy H:mm:ss", "dd/M/yyyy HH:mm:ss", "d/MM/yyyy H:mm:ss" },
            _ => new[] { "yyyy-MM-dd HH:mm:ss" }
        };
    }
}