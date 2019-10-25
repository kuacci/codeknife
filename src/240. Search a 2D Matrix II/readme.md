# [Medium][240. Search a 2D Matrix II](https://leetcode.com/problems/search-a-2d-matrix-ii/)

Write an efficient algorithm that searches for a value in an m x n matrix. This matrix has the following properties:

Integers in each row are sorted in ascending from left to right.
Integers in each column are sorted in ascending from top to bottom.
Example:

Consider the following matrix:

```csharp
[
  [1,   4,  7, 11, 15],
  [2,   5,  8, 12, 19],
  [3,   6,  9, 16, 22],
  [10, 13, 14, 17, 24],
  [18, 21, 23, 26, 30]
]
```

> Given target = 5, return true.
> Given target = 20, return false.

## 思路 - 寻路

这个矩阵有个特点，行列都是有序的。即同一行里面，每个数字都是升序的，同一列也同样是升序。一个数字的West 和 North都比他小，而East和West都比他大。利用这个特点，可以从左下角开始搜索，如果`nums[r,c] > target`, 向 North方向，如果`nums[r,c] < target`, 向 East方向。
选择左下角，是以为超2个方向走的适合是可以分别`>` 或者 '<'的条件。同样符合条件的是右上角。右下和左下不能作为起始地点。

## 代码 - 寻路

```csharp
public class Solution {
    public bool SearchMatrix(int[,] matrix, int target)
    {
        int row = matrix.GetLength(0);
        int column = matrix.GetLength(1);

        if (row == 0 || column == 0) return false;

        int r = row - 1;
        int c = 0;

        while(r >= 0 && c < column)
        {
            if (matrix[r, c] == target)
                return true;
            if(matrix[r,c] > target)
            {
                r--;
            }
            else
            {
                c++;
            }
        }

        return false;
    }
}
```
