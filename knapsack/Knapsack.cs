using System;

public static class Knapsack
{
    public static int MaximumValue(int maximumWeight, (int weight, int value)[] items)
    {
        var dp = new int[maximumWeight + 1];

        foreach (var (weight, value) in items)
        {
            // update backwards เพื่อป้องกันการใช้ item เดิมซ้ำ
            for (int w = maximumWeight; w >= weight; w--)
            {
                dp[w] = Math.Max(dp[w], dp[w - weight] + value);
            }
        }

        return dp[maximumWeight];
    }
}
