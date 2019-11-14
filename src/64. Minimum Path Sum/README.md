# [Medium][64. Minimum Path Sum](https://leetcode.com/problems/minimum-path-sum/)

Given a m x n grid filled with non-negative numbers, find a path from top left to bottom right which minimizes the sum of all numbers along its path.

Note: You can only move either down or right at any point in time.

Example:

```text
Input:
[
  [1,3,1],
  [1,5,1],
  [4,2,1]
]
Output: 7
```

Explanation: Because the path 1→3→1→1→1 minimizes the sum.

## 思路 - DP

最直接的方法就是暴力破解，但这个当然不是我们想要的方法。观察这里的数字，可以看出来最后的结果起始时由上一步的移动来构成的。由于只能朝一个方向移动，右或者下。无论超那个方向，没有走到最后，都无法断定哪条路时最小的和。所以要用DP的方式来帮助解决。可以解决[62. Unique Paths](src/62.%20Unique%20Paths)和[63. Unique Paths II](src/63. Unique Paths II/README.md).

1. 先计算第一列和第一行的和。分别模拟沿着边走的时候的和的情况。
2. 以`grid[1][1]`作为起始位置开始计算。走到这个位置有2种方式，从上面下来，或者从左侧过来，我们不用关心它时怎么到达的，只关心过来的时候，最小值时哪一个。用这个最小值加上当前位置的值。
3. 一直计算到`grid[n][n]`. 返回最后一个值。

## 代码 - DP

```csharp
public class Solution {
    public int MinPathSum(int[][] grid) {

        int Row = grid.Length;
        int Col = grid[0].Length;

        for(int i = 1; i < Col; i++)
            grid[0][i] += grid[0][i - 1];
        for(int i = 1; i < Row; i++)
            grid[i][0] += grid[i - 1][0];
        for(int i = 1; i < Row; i++)
        {
            for(int j = 1; j < Col; j++)
            {
                grid[i][j] += Math.Min(grid[i - 1][j], grid[i][j - 1]);
            }
        }

        return grid[Row - 1][Col - 1];
    }
}
```
