public static class Triangle
{
    public static bool IsValid(double a, double b, double c)
    {
        return a > 0 && b > 0 && c > 0
            && (a + b >= c)
            && (b + c >= a)
            && (a + c >= b);
    }
    public static bool IsEquilateral(double side1, double side2, double side3)
    {
        return IsValid(side1, side2, side3)
            && side1 == side2
            && side2 == side3;
    }
    public static bool IsIsosceles(double side1, double side2, double side3)
    {
        return IsValid(side1 ,side2, side3)
            && (side1 == side2 || side2 == side3 || side1 == side3);
    }
    public static bool IsScalene(double side1, double side2, double side3)
    {
        return IsValid(side1 , side2, side3)
            && side1 != side2
            && side2 != side3
            && side1 != side3;
    }
}