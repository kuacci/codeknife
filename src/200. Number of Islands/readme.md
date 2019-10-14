# [200. Number of Islands](https://leetcode.com/problems/number-of-islands/)

Given a 2d grid map of '1's (land) and '0's (water), count the number of islands. An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. You may assume all four edges of the grid are all surrounded by water.

**Example 1:**

```text
Input:
11110
11010
11000
00000

Output: 1
```

**Example 2:**

```text
Input:
11000
11000
00100
00011

Output: 3
```

## 思路 - dfs

遇到这种问题，常规的思路是dfs. 遍历整个二维数组，当遇到`1`的时候，将count加1. 然后进入dfs，将所有与之相连的1都置为0.然后从dfs中退出来。这样就可以直到成片的1一共出现过几次。

## 代码 - dfs

```csharp
public class Solution {

    public int NumIslands(char[][] grid) {

        if(grid == null || grid.Length == 0) return 0;

        int count = 0;

        int row = grid.Length;
        int column = grid[0].Length;

        for(int r = 0; r < row; r++)
        {
            for(int c = 0; c < column; c++)
            {
                if(grid[r][c] == '1')
                {
                    count += 1;
                    dfs(grid, r, c);
                }
            }
        }
        return count;
    }

    private void dfs(char[][] grid, int r, int c)
    {
        int row = grid.Length;
        int column = grid[0].Length;

        if(r < 0 || c < 0 || r >= row || c >= column || grid[r][c] == '0')
            return;
        grid[r][c] = '0';
        dfs(grid, r - 1, c);
        dfs(grid, r + 1, c);
        dfs(grid, r, c - 1);
        dfs(grid, r, c + 1);
    }
}
```
