using System;
using System.Collections.Generic;

public static class Say
{
    private static readonly string[] UnitsMap =
    {
        "zero", "one", "two", "three", "four", "five", "six",
        "seven", "eight", "nine", "ten", "eleven", "twelve",
        "thirteen", "fourteen", "fifteen", "sixteen",
        "seventeen", "eighteen", "nineteen"
    };

    private static readonly string[] TensMap =
    {
        "", "", "twenty", "thirty", "forty", "fifty",
        "sixty", "seventy", "eighty", "ninety"
    };

    private static readonly string[] ScaleMap =
    {
        "", "thousand", "million", "billion"
    };

    public static string InEnglish(long number)
    {
        if (number < 0 || number > 999_999_999_999)
            throw new ArgumentOutOfRangeException(nameof(number));

        if (number == 0)
            return "zero";

        var parts = new List<string>();
        int scaleIndex = 0;

        while (number > 0)
        {
            int chunk = (int)(number % 1000);
            if (chunk != 0)
            {
                var chunkWords = ChunkToWords(chunk);
                if (!string.IsNullOrEmpty(ScaleMap[scaleIndex]))
                {
                    chunkWords += " " + ScaleMap[scaleIndex];
                }
                parts.Insert(0, chunkWords);
            }
            number /= 1000;
            scaleIndex++;
        }

        return string.Join(" ", parts);
    }

    private static string ChunkToWords(int number)
    {
        var parts = new List<string>();

        if (number >= 100)
        {
            parts.Add(UnitsMap[number / 100] + " hundred");
            number %= 100;
        }

        if (number >= 20)
        {
            parts.Add(TensMap[number / 10] + (number % 10 > 0 ? "-" + UnitsMap[number % 10] : ""));
        }
        else if (number > 0)
        {
            parts.Add(UnitsMap[number]);
        }

        return string.Join(" ", parts);
    }
}
