using System;
using System.Collections.Generic;
using System.Linq;

// Definition of the Coord struct
public struct Coord
{
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public ushort X { get; }
    public ushort Y { get; }
}

// Full implementation of the Plot struct
public struct Plot
{
    public Coord Coord1 { get; }
    public Coord Coord2 { get; }
    public Coord Coord3 { get; }
    public Coord Coord4 { get; }

    public Plot(Coord coord1, Coord coord2, Coord coord3, Coord coord4)
    {
        Coord1 = coord1;
        Coord2 = coord2;
        Coord3 = coord3;
        Coord4 = coord4;
    }

    // Overriding Equals and GetHashCode is crucial for correct behavior with collections like HashSet
    public override bool Equals(object? obj)
    {
        return obj is Plot other &&
               Coord1.Equals(other.Coord1) &&
               Coord2.Equals(other.Coord2) &&
               Coord3.Equals(other.Coord3) &&
               Coord4.Equals(other.Coord4);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Coord1, Coord2, Coord3, Coord4);
    }
}

// Full implementation of the ClaimsHandler class
public class ClaimsHandler
{
    private readonly HashSet<Plot> _stakedClaims = new();
    private Plot _lastClaimedPlot;

    public void StakeClaim(Plot plot)
    {
        _stakedClaims.Add(plot);
        _lastClaimedPlot = plot;
    }

    public bool IsClaimStaked(Plot plot)
    {
        return _stakedClaims.Contains(plot);
    }

    public bool IsLastClaim(Plot plot)
    {
        return _lastClaimedPlot.Equals(plot);
    }

    public Plot GetClaimWithLongestSide()
    {
        Plot longestPlot = default;
        double longestSideSquared = 0;

        foreach (var plot in _stakedClaims)
        {
            var sides = new (Coord c1, Coord c2)[]
            {
                (plot.Coord1, plot.Coord2),
                (plot.Coord2, plot.Coord3),
                (plot.Coord3, plot.Coord4),
                (plot.Coord4, plot.Coord1)
            };

            foreach (var side in sides)
            {
                // Calculate squared length to avoid costly square root operations
                double currentLengthSquared = Math.Pow(side.c1.X - side.c2.X, 2) + Math.Pow(side.c1.Y - side.c2.Y, 2);

                if (currentLengthSquared > longestSideSquared)
                {
                    longestSideSquared = currentLengthSquared;
                    longestPlot = plot;
                }
            }
        }

        return longestPlot;
    }
}