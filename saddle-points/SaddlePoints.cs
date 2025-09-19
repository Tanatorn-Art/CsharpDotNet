using System;
using System.Collections.Generic;
using System.Linq;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        var result = new List<(int , int)>();

        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);

        for (int row = 0; row < rowCount; row++)
        {
            int rowMax = Enumerable.Range(0, colCount)
                .Select(c => matrix[row, c])
                .Max();

            for (int col = 0; col < colCount; col++)
            {
                if (matrix[row, col] == rowMax)
                {
                    int colMin = Enumerable.Range(0, rowCount)
                        .Select(r => matrix[r, col])
                        .Min();

                    if (matrix[row, col] == colMin)
                    {
                        result.Add((row + 1 , col + 1));
                    }
                }
            }
        }
        return result;
    }
}