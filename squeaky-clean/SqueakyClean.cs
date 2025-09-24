using System;
using System.Text;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        var result = new StringBuilder();
        bool capitalizeNext = false;

        foreach (char c in identifier)
        {
            if (c == ' ')
            {
                result.Append('_');
                capitalizeNext = false;
            }
            else if (char.IsControl(c))
            {
                result.Append("CTRL");
                capitalizeNext = false;
            }
            else if (c == '-')
            {
                capitalizeNext = true;
            }
            else if (!char.IsLetter(c))
            {
                // ข้ามตัวที่ไม่ใช่ตัวอักษร
                capitalizeNext = false;
            }
            else if (c >= 'α' && c <= 'ω')
            {
                // ข้าม greek lowercase
                capitalizeNext = false;
            }
            else
            {
                if (capitalizeNext)
                {
                    result.Append(char.ToUpper(c));
                    capitalizeNext = false;
                }
                else
                {
                    result.Append(c);
                }
            }
        }

        return result.ToString();
    }
}
