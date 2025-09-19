static class LogLine
{
    public static string Message(string logLine)
    {
        // ตัดตรง ": " แล้วเอาส่วนหลังมา
        return logLine.Split(": ", 2)[1].Trim();
    }

    public static string LogLevel(string logLine)
    {
        // เอาส่วนที่อยู่ระหว่าง [ และ ]
        string level = logLine.Split("]:", 2)[0]
                               .Trim('[', ']');
        return level.ToLower();
    }

    public static string Reformat(string logLine)
    {
        // "<message> (<loglevel>)"
        return $"{Message(logLine)} ({LogLevel(logLine)})";
    }
}
