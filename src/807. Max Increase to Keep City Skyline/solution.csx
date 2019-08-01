public class Solution {
    public int MaxIncreaseKeepingSkyline(int[][] grid) {
        if(grid == null || grid.Length == 0 || grid.Length == 1) return 0;

        int heigh = grid.Length;
        int weight = grid[0].Length;

        int[] maxHei = new int[heigh];
        int[] maxWei = new int[weight];

        int inc = 0;

        for(int i = 0; i < heigh; i++)
        {
            for(int j = 0; j < weight; j++)
            {
                maxHei[i] = Math.Max(maxHei[i],grid[i][j]);
                maxWei[j] = Math.Max(maxWei[j],grid[i][j]);
            }
        }

        for(int i = 0; i < heigh; i++)
        {
            for(int j = 0; j < weight; j++)
            {
                inc = inc + Math.Min(maxHei[i], maxWei[j]) - grid[i][j];
            }
        }

        return inc;
    }
}