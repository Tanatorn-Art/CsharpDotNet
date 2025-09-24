using System.Text.RegularExpressions;
using System.Linq;

public class LogParser
{
    /// <summary>
    /// Checks if a log line is valid by matching it against a set of predefined log levels.
    /// </summary>
    /// <param name="text">The log line to validate.</param>
    /// <returns>True if the line starts with a valid log level, otherwise false.</returns>
    public bool IsValidLine(string text)
    {
        return Regex.IsMatch(text, @"^\[(TRC|DBG|INF|WRN|ERR|FTL)\]");
    }

    /// <summary>
    /// Splits a log line into an array of strings based on a custom separator.
    /// </summary>
    /// <param name="text">The log line to split.</param>
    /// <returns>An array of strings containing the fields of the log line.</returns>
    public string[] SplitLogLine(string text)
    {
        return Regex.Split(text, @"<[\*\^=-]*>");
    }

    /// <summary>
    /// Counts the number of log lines that contain the word "password" enclosed in quotation marks, case-insensitively.
    /// </summary>
    /// <param name="lines">A string containing multiple log lines.</param>
    /// <returns>The count of lines that match the criteria.</returns>
    public int CountQuotedPasswords(string lines)
    {
        return Regex.Matches(lines, @"""[^""]*password[^""]*""", RegexOptions.IgnoreCase).Count;
    }

    /// <summary>
    /// Removes the "end-of-line" text followed by a number from a log line.
    /// </summary>
    /// <param name="line">The log line to clean.</param>
    /// <returns>The log line with the specified artifacts removed.</returns>
    public string RemoveEndOfLineText(string line)
    {
        return Regex.Replace(line, @"end-of-line\d+", "");
    }

    /// <summary>
    /// Lists log lines containing extremely weak passwords (those starting with "password").
    /// Each line is formatted with a prefix indicating the password found or a default prefix.
    /// </summary>
    /// <param name="lines">An array of log lines.</param>
    /// <returns>A new array of formatted strings.</returns>
    public string[] ListLinesWithPasswords(string[] lines)
    {
        return lines.Select(line => {
            // The regex is updated to correctly match words starting with "password" that are not just "password" itself.
            // This is based on the specific behavior of the provided test cases.
            var match = Regex.Match(line, @"\b(password\w+)\b", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return $"{match.Groups[1].Value}: {line}";
            }
            else
            {
                return $"--------: {line}";
            }
        }).ToArray();
    }
}