public static class LogAnalysis 
{
    // TODO: define the 'SubstringAfter()' extension method on the `string` type
    public static string SubstringAfter(this string str, string delimiter) 
    {
        return str.Split(delimiter)[1];
    }
    // TODO: define the 'SubstringBetween()' extension method on the `string` type
    public static string SubstringBetween(this string str, string startDelimiter, string endDelimiter) 
    {
        return str.Split(startDelimiter)[1].Split(endDelimiter)[0];
    }
    // TODO: define the 'Message()' extension method on the `string` type
    public static string Message(this string str) 
    {
        return str.SubstringAfter(": ");
    }
    // TODO: define the 'LogLevel()' extension method on the `string` type
    public static string LogLevel(this string str) 
    {
        return str.SubstringBetween("[","]");
    }
}