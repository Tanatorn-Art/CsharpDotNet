using System;
using System.Collections.Generic;
using System.Linq;

public static class NthPrime
{
    public static int Prime(int nth)
    {
        if (nth < 1)
            throw new ArgumentOutOfRangeException(nameof(nth), "nth must be at least 1");

        return GeneratePrimes().Skip(nth - 1).First();
    }

    private static IEnumerable<int> GeneratePrimes()
    {
        yield return 2; // First prime

        var primes = new List<int> { 2 };
        int candidate = 3;

        while (true)
        {
            if (IsPrime(candidate, primes))
            {
                primes.Add(candidate);
                yield return candidate;
            }
            candidate += 2; // Skip even numbers after 2
        }
    }

    private static bool IsPrime(int candidate, List<int> knownPrimes)
    {
        int sqrtCandidate = (int)Math.Sqrt(candidate);

        foreach (int prime in knownPrimes)
        {
            if (prime > sqrtCandidate)
                break;

            if (candidate % prime == 0)
                return false;
        }

        return true;
    }
}