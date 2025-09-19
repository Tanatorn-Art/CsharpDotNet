using System;

public class SpaceAge
{
    private readonly int seconds;

    private const double EarthYearInSeconds = 31557600;

    public SpaceAge(int seconds)
    {
        this.seconds = seconds;
    }

    public double OnEarth()
    {
        return seconds / EarthYearInSeconds;
    }

    public double OnMercury()
    {
        return seconds / (EarthYearInSeconds * 0.2408467);
    }

    public double OnVenus()
    {
        return seconds / (EarthYearInSeconds * 0.61519726);
    }

    public double OnMars()
    {
        return seconds / (EarthYearInSeconds * 1.8808158);
    }

    public double OnJupiter()
    {
        return seconds / (EarthYearInSeconds * 11.862615);
    }

    public double OnSaturn()
    {
        return seconds / (EarthYearInSeconds * 29.447498);
    }

    public double OnUranus()
    {
        return seconds / (EarthYearInSeconds * 84.016846);
    }

    public double OnNeptune()
    {
        return seconds / (EarthYearInSeconds * 164.79132);
    }
}