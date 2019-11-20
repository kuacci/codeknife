# [Medium][120. Triangle](https://leetcode-cn.com/problems/triangle/)

Given a triangle, find the minimum path sum from top to bottom. Each step you may move to adjacent numbers on the row below.

For example, given the following triangle

[
     [2],
    [3,4],
   [6,5,7],
  [4,1,8,3]
]
The minimum path sum from top to bottom is 11 (i.e., 2 + 3 + 5 + 1 = 11).

Note:

Bonus point if you are able to do this using only O(n) extra space, where n is the total number of rows in the triangle.

## 思路 - backtrack

这题的要求是求最小的路径。路径从上到下，并且行程一个直角三角形。假设某一点为`triangle[row][pos]`, 下一个可能的点被限定在`triangle[row+1][pos]`和`triangle[row+1][pos+1]`.
可以用backtrack的方式从上往下遍历一遍，求出最小值。

不过这个解超时了。

## 代码 - backtrack

```csharp
public class Solution {
    private int ans = int.MaxValue;
    public int MinimumTotal(IList<IList<int>> triangle) {
        if(triangle == null || triangle.Count == 0) return 0;
        MinimumTotalBackTrack(triangle, 0, 0, 0);
        return ans;
    }

    private void MinimumTotalBackTrack(IList<IList<int>> triangle, int sum, int row, int pos)
    {
        if(row == triangle.Count)
        {
            ans = Math.Min(ans, sum);
        }
        else
        {
            int num = triangle[row][pos];
            sum += num;
            MinimumTotalBackTrack(triangle, sum, row + 1, pos);
            MinimumTotalBackTrack(triangle, sum, row + 1, pos + 1);
        }
    }
}
```

## 思路 - dp - 自底向上

dp的思路跟[62. Unique Paths](src/62.%20Unique%20Paths)和[63. Unique Paths II](src/63.%20Unique%20Paths%20II)的思路很类似，利用最优子结构的特点。计算从底部开始。

1. 计算从倒数第二列开始`triangle[row][idx] where row = triangle.Count - 2, idx = 0` .
2. 将下列的正下方和右边一格的数字中较小的一个数字加到当前这一格：`triangle[row][idx] += Math.Min(triangle[row + 1][idx], triangle[row + 1][idx + 1]);`。
3. 最后的结果会汇集到`triagle[0][0]`这个位置。返回这个位置的值。

NOTE:因为每次计算结果只需要使用到下一层的数字，而且运动方向是从下往上的，所有原地计算没问题。

## 代码 - dp - 自底向上

```csharp
public class Solution {
    public int MinimumTotal(IList<IList<int>> triangle) {
        if(triangle == null || triangle.Count == 0) return 0;
        for(int row = triangle.Count - 2; row >= 0; row--)
        {
            for(int idx = 0; idx < triangle[row].Count; idx++)
            {
                triangle[row][idx] += Math.Min(triangle[row + 1][idx], triangle[row + 1][idx + 1]);
            }
        }
        return triangle[0][0];
    }
}
```
