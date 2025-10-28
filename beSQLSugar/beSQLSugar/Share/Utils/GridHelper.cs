namespace beSQLSugar.Share.Utils
{
    public static class GridHelper
    {
        // Chuyển từ 2D array sang jagged array
        public static int[][] ToJagged(this int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int[][] jagged = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                jagged[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                    jagged[i][j] = grid[i, j];
            }
            return jagged;
        }

        // Chuyển từ jagged array sang 2D array
        public static int[,] To2D(this int[][] jagged)
        {
            int rows = jagged.Length;
            int cols = jagged[0].Length;
            int[,] grid = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i, j] = jagged[i][j];
            return grid;
        }
    }
}
