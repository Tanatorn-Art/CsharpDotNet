public static class SquareRoot
{
    public static int Root(int number)
    {
        if (number < 0) throw new ArgumentException("Number must be positive");

        if (number == 0 || number == 1 ) return number;

        int low = 1;
        int high = number;
        int result = 0;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            int midSquared = mid  * mid;

            if (midSquared == number)
            {
                return mid;
            }
            else if (midSquared < number)
            {
                low  = mid + 1;
                result = mid;
            }
            else
            {
                high = mid - 1;
            }
        }
        return result;
    }
}
