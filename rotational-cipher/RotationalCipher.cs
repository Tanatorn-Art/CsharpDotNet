using System;
using System.Text;

public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        if (string.IsNullOrEmpty(text)) return text;

        shiftKey = shiftKey % 26;
        var result = new StringBuilder(text.Length);

        foreach (char c in text)
        {
            if (char.IsLower(c))
            {
                char rolated = (char)('a' + (c - 'a' + shiftKey) % 26);
                result.Append(rolated);
            }
            else if (char.IsUpper(c))
            {
                char rolated = (char)('A' + (c - 'A' + shiftKey) % 26);
                result.Append(rolated);
            }
            else
            {
                result.Append(c);
            }
        }
        return result.ToString();
    }
}