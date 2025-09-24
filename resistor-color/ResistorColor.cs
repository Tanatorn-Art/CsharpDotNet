public static class ResistorColor
{
    private static readonly string[] ColorsArray = new string[]
    {
        "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white"
    };

    /// <summary>
    /// This method returns the numerical value of a given color band.
    /// </summary>
    /// <param name="color">The color band as a string.</param>
    /// <returns>The numerical value (0-9).</returns>
    public static int ColorCode(string color)
    {
        for (int i = 0; i < ColorsArray.Length; i++)
        {
            if (ColorsArray[i] == color)
            {
                return i;
            }
        }
        return -1; // Or throw an exception for invalid input.
    }

    /// <summary>
    /// This method returns an array of all the resistor color bands.
    /// </summary>
    /// <returns>An array of color strings.</returns>
    public static string[] Colors()
    {
        return ColorsArray;
    }
}