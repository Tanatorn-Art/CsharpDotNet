using System;
using System.Collections.Generic;
using System.Linq;

public static class KillerSudokuHelper
{
    public static IEnumerable<int[]> Combinations(int sum, int size, int[] exclude)
    {
        var excluded = new HashSet<int>(exclude);
        var result = new List<int[]>();
        var current = new List<int>();

        void Backtrack(int start, int remainingSum, int remainingSize)
        {
            if (remainingSize == 0 && remainingSum == 0)
            {
                result.Add(current.ToArray());
                return;
            }
            if (remainingSize == 0 || remainingSum <= 0) return;

            for (int digit = start; digit <= 9; digit++)
            {
                if (excluded.Contains(digit)) continue;
                current.Add(digit);
                Backtrack(digit + 1, remainingSum - digit, remainingSize - 1);
                current.RemoveAt(current.Count - 1);
            }
        }

        Backtrack(1, sum, size);

        // เรียงลำดับผลลัพธ์
        return result.OrderBy(arr => string.Join("", arr)).ToList();
    }
}
