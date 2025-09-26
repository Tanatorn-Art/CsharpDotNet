using System;

public static class GameOfLife
{
    public static int[,] Tick(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int[,] nextGen = new int[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int liveNeighbors = CountLiveNeighbors(matrix, r, c, rows, cols);

                if (matrix[r, c] == 1)
                {
                    // Alive cell
                    nextGen[r, c] = (liveNeighbors == 2 || liveNeighbors == 3) ? 1 : 0;
                }
                else
                {
                    // Dead cell
                    nextGen[r, c] = (liveNeighbors == 3) ? 1 : 0;
                }
            }
        }

        return nextGen;
    }

    private static int CountLiveNeighbors(int[,] matrix, int row, int col, int rows, int cols)
    {
        int count = 0;

        for (int dr = -1; dr <= 1; dr++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                if (dr == 0 && dc == 0) continue; // skip self

                int nr = row + dr;
                int nc = col + dc;

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                {
                    count += matrix[nr, nc];
                }
            }
        }

        return count;
    }
}
