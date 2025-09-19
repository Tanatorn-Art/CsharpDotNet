public static class Darts
{
    public static int Score(double x, double y)
    {
       double distanceSquared = x * x + y * y;

       if (distanceSquared <= 1.0 * 1.0) return 10;
       if (distanceSquared <= 5.0 * 5.0) return 5;
       if (distanceSquared <= 10.0 * 10.0) return 1;
       return 0;
    }
}
