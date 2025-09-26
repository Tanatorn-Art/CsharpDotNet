using System;
using System.Text;

public static class RnaTranscription
{
    public static string ToRna(string strand)
    {
        if (strand == null) throw new ArgumentNullException(nameof(strand));

        var result = new StringBuilder();

        foreach (char nucleotide in strand)
        {
            switch (nucleotide)
            {
                case 'G':
                    result.Append('C');
                    break;
                case 'C':
                    result.Append('G');
                    break;
                case 'T':
                    result.Append('A');
                    break;
                case 'A':
                    result.Append('U');
                    break;
                default:
                    throw new ArgumentException($"Invalid nucleotide: {nucleotide}");
            }
        }

        return result.ToString();
    }
}
