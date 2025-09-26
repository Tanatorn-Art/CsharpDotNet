public static class EliudsEggs
{
    public static int EggCount(int encodedCount)
    {
        int count = 0;

        while (encodedCount > 0)
        {
            if ((encodedCount & 1) == 1)
            {
                count++;
            }
            encodedCount >>= 1;
        }

        return count;
    }
}
