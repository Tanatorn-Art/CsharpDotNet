using System;
using System.Linq;
using System.Text;

public class RailFenceCipher
{
    private readonly int rails;

    public RailFenceCipher(int rails)
    {
        if (rails < 2)
            throw new ArgumentException("Rails must be at least 2.");
        this.rails = rails;
    }

    public string Encode(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        string text = RemoveSpaces(input);

        var railBuilders = new StringBuilder[rails];
        for (int i = 0; i < rails; i++)
            railBuilders[i] = new StringBuilder();

        int rail = 0;
        int direction = 1;

        foreach (char c in text)
        {
            railBuilders[rail].Append(c);

            if (rail == 0) direction = 1;
            else if (rail == rails - 1) direction = -1;

            rail += direction;
        }

        return string.Concat(railBuilders.Select(sb => sb.ToString()));
    }

    public string Decode(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        int len = input.Length;
        var pattern = new int[len];

        // สร้าง pattern rail index สำหรับแต่ละตัวอักษร
        int rail = 0;
        int direction = 1;
        for (int i = 0; i < len; i++)
        {
            pattern[i] = rail;

            if (rail == 0) direction = 1;
            else if (rail == rails - 1) direction = -1;

            rail += direction;
        }

        // นับว่ารางแต่ละอันมีอักษรกี่ตัว
        var counts = new int[rails];
        for (int i = 0; i < len; i++)
            counts[pattern[i]]++;

        // ตัด input ออกมาใส่รางแต่ละอัน
        var railsContent = new string[rails];
        int pos = 0;
        for (int r = 0; r < rails; r++)
        {
            railsContent[r] = input.Substring(pos, counts[r]);
            pos += counts[r];
        }

        // pointer per rail
        var indices = new int[rails];
        var result = new StringBuilder();

        for (int i = 0; i < len; i++)
        {
            int r = pattern[i];
            result.Append(railsContent[r][indices[r]]);
            indices[r]++;
        }

        return result.ToString();
    }

    private static string RemoveSpaces(string s)
    {
        return string.IsNullOrEmpty(s) ? string.Empty :
               new string(s.Where(ch => !char.IsWhiteSpace(ch)).ToArray());
    }
}
