# [Medium][54. Spiral Matrix](https://leetcode.com/problems/spiral-matrix/)

Given a matrix of m x n elements (m rows, n columns), return all elements of the matrix in spiral order.

**Example 1:**

```text
Input:
[
 [ 1, 2, 3 ],
 [ 4, 5, 6 ],
 [ 7, 8, 9 ]
]
Output: [1,2,3,6,9,8,7,4,5]
```

**Example 2:**

```text
Input:
[
  [1, 2, 3, 4],
  [5, 6, 7, 8],
  [9,10,11,12]
]
Output: [1,2,3,4,8,12,11,10,9,5,6,7]
```

## 思路 - 模拟轨迹

按照题目的要求是进行一个顺时针螺旋方向的遍历。当遇到边界的时候就顺时针转向。定义4调边界，正上方的边界TR (Top Row), 正下方的边界 BR (Bottom Row), 左边界 LC (Left Column), 右边界 RC (Right Column). 每次遇到边界之后，边界向中心收缩，同时转向。最后终止的条件是所有的边界互相跨越，这个遍历就结束了。

## 代码 - 模拟轨迹

```csharp
public class Solution {
    public IList<int> SpiralOrder(int[][] matrix) {
        IList<int> ans = new List<int>();

        if(matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return ans;

        int TR = 0, BR = matrix.Length - 1;
        int LC = 0, RC = matrix[0].Length - 1;

        while(true)
        {
            if(LC > RC) break;
            for(int i = LC; i <= RC; i++)
                ans.Add(matrix[TR][i]);
            TR++;

            if(TR > BR) break;
            for(int i = TR; i <= BR; i++)
                ans.Add(matrix[i][RC]);
            RC--;

            if(LC > RC) break;
            for(int i = RC; i >= LC; i--)
                ans.Add(matrix[BR][i]);
            BR--;

            if(TR > BR) break;
            for(int i = BR; i >= TR; i--)
                ans.Add(matrix[i][LC]);
            LC++;
        }

        return ans;
    }
}
```
