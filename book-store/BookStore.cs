using System;
using System.Collections.Generic;
using System.Linq;

public static class BookStore
{
    private const decimal BookPrice = 8.0m;

    private static readonly Dictionary<int, decimal> Discounts = new Dictionary<int, decimal>
    {
        { 1, 0.00m },  // No discount for 1 book
        { 2, 0.05m },  // 5% discount for 2 different books
        { 3, 0.10m },  // 10% discount for 3 different books
        { 4, 0.20m },  // 20% discount for 4 different books
        { 5, 0.25m }   // 25% discount for 5 different books
    };

    public static decimal Total(IEnumerable<int> books)
    {
        if (books == null)
            return 0;

        var bookCounts = books.GroupBy(b => b)
                             .ToDictionary(g => g.Key, g => g.Count());

        if (bookCounts.Count == 0)
            return 0;

        return FindMinimumCost(bookCounts.Values.ToArray());
    }

    private static decimal FindMinimumCost(int[] bookCounts)
    {
        // If no books left, cost is 0
        if (bookCounts.All(count => count == 0))
            return 0;

        decimal minCost = decimal.MaxValue;

        // Try all possible group sizes from 1 to 5
        for (int groupSize = 1; groupSize <= 5; groupSize++)
        {
            // Check if we can form a group of this size
            int availableTypes = bookCounts.Count(count => count > 0);
            if (groupSize > availableTypes)
                continue;

            // Calculate cost for this group
            decimal groupCost = groupSize * BookPrice * (1 - Discounts[groupSize]);

            // Create remaining book counts after taking this group
            var remainingCounts = new int[bookCounts.Length];
            Array.Copy(bookCounts, remainingCounts, bookCounts.Length);

            // Take one book from the types with highest counts (greedy approach)
            var indices = bookCounts
                .Select((count, index) => new { count, index })
                .Where(x => x.count > 0)
                .OrderByDescending(x => x.count)
                .Take(groupSize)
                .Select(x => x.index)
                .ToArray();

            foreach (int index in indices)
            {
                remainingCounts[index]--;
            }

            // Recursively calculate cost for remaining books
            decimal remainingCost = FindMinimumCost(remainingCounts);
            decimal totalCost = groupCost + remainingCost;

            minCost = Math.Min(minCost, totalCost);
        }

        return minCost;
    }
}