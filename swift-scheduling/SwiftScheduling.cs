using System;
using System.Text.RegularExpressions;

public static class SwiftScheduling
{
    public static DateTime DeliveryDate(DateTime meetingStart, string description)
    {
        description = description.ToUpper();

        // --- Fixed delivery descriptions ---
        switch (description)
        {
            case "NOW":
                return meetingStart.AddHours(2);

            case "ASAP":
                if (meetingStart.Hour < 13)
                    return meetingStart.Date.AddHours(17); // วันนี้ 17:00
                else
                    return meetingStart.Date.AddDays(1).AddHours(13); // พรุ่งนี้ 13:00

            case "EOW":
                if (meetingStart.DayOfWeek == DayOfWeek.Monday ||
                    meetingStart.DayOfWeek == DayOfWeek.Tuesday ||
                    meetingStart.DayOfWeek == DayOfWeek.Wednesday)
                {
                    // วันศุกร์ 17:00 ของสัปดาห์เดียวกัน
                    int daysToFriday = ((int)DayOfWeek.Friday - (int)meetingStart.DayOfWeek + 7) % 7;
                    return meetingStart.Date.AddDays(daysToFriday).AddHours(17);
                }
                else
                {
                    // วันอาทิตย์ 20:00 ของสัปดาห์เดียวกัน
                    int daysToSunday = ((int)DayOfWeek.Sunday - (int)meetingStart.DayOfWeek + 7) % 7;
                    return meetingStart.Date.AddDays(daysToSunday).AddHours(20);
                }
        }

        // --- Variable delivery descriptions ---
        // Pattern "<N>M"
        var monthMatch = Regex.Match(description, @"^(\d{1,2})M$");
        if (monthMatch.Success)
        {
            int month = int.Parse(monthMatch.Groups[1].Value);
            int year = meetingStart.Month < month ? meetingStart.Year : meetingStart.Year + 1;
            return FirstWorkdayOfMonth(year, month).AddHours(8);
        }

        // Pattern "Q<N>"
        var quarterMatch = Regex.Match(description, @"^Q(\d)$");
        if (quarterMatch.Success)
        {
            int quarter = int.Parse(quarterMatch.Groups[1].Value);
            int currentQuarter = ((meetingStart.Month - 1) / 3) + 1;
            int year = quarter >= currentQuarter ? meetingStart.Year : meetingStart.Year + 1;
            return LastWorkdayOfQuarter(year, quarter).AddHours(8);
        }

        throw new ArgumentException("Invalid delivery description");
    }

    // --- Helper functions ---
    private static DateTime FirstWorkdayOfMonth(int year, int month)
    {
        DateTime date = new DateTime(year, month, 1);
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            date = date.AddDays(1);
        }
        return date;
    }

    private static DateTime LastWorkdayOfQuarter(int year, int quarter)
    {
        int lastMonth = quarter * 3; // เดือนสุดท้ายของไตรมาส
        DateTime date = new DateTime(year, lastMonth, DateTime.DaysInMonth(year, lastMonth));
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            date = date.AddDays(-1);
        }
        return date;
    }
}
