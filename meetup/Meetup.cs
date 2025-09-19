public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}

public class Meetup
{
    private readonly int _month;
    private readonly int _year;

    public Meetup(int month, int year)
    {
        _month = month;
        _year = year;
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        switch (schedule)
        {
            case Schedule.Teenth:
                for (int day = 13; day <= 19; day++)
                {
                    var date = new DateTime(_year ,_month, day);
                    if (date.DayOfWeek == dayOfWeek)
                        return date;
                }
                break;
            case Schedule.First:
            case Schedule.Second:
            case Schedule.Third:
            case Schedule.Fourth:
                int count = 0;
                for (int day =1; day <= DateTime.DaysInMonth(_year, _month); day++)
                {
                    var date = new DateTime(_year, _month, day);
                    if (date.DayOfWeek == dayOfWeek)
                    {
                        count++;
                        if ((schedule == Schedule.First && count == 1) ||
                            (schedule == Schedule.Second && count == 2) ||
                            (schedule == Schedule.Third && count == 3) ||
                            (schedule == Schedule.Fourth && count == 4))
                        {
                            return date;
                        }
                    }
                }
                break;
            case Schedule.Last:
                DateTime lastDate = new DateTime(_year, _month, DateTime.DaysInMonth(_year, _month));
                while (lastDate.DayOfWeek != dayOfWeek)
                {
                    lastDate = lastDate.AddDays(-1);
                }
                return lastDate;
        }
        throw new ArgumentException("Invalid schedule or dayOfWeek");
    }
}