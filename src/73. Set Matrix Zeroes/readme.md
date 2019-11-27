# [Medium][73. Set Matrix Zeroes](https://leetcode.com/problems/set-matrix-zeroes/)

Given a m x n matrix, if an element is 0, set its entire row and column to 0. Do it in-place.

Example 1:

```text
Input:
[
  [1,1,1],
  [1,0,1],
  [1,1,1]
]
Output:
[
  [1,0,1],
  [0,0,0],
  [1,0,1]
]
```

Example 2:

```text
Input:
[
  [0,1,2,0],
  [3,4,5,2],
  [1,3,1,5]
]
Output:
[
  [0,0,0,0],
  [0,4,5,0],
  [0,3,1,0]
]
```text

Follow up:

A straight forward solution using O(mn) space is probably a bad idea.
A simple improvement uses O(m + n) space, but still not the best solution.
Could you devise a constant space solution?

## 思路 - 空间复杂度 O(m+n)

当矩阵中一个元素为0的时候，该元素同行列都被置为0. 这里要解决的一个问题是当置0的时候，可能会把原生的0覆盖掉。以至于在后面访问到0的时候，无法判断是原生的0，还是被翻转的0.为了解决这个问题，需要记录下来原生的0的情况。
所以使用2个辅助数组，在遍历第一遍矩阵的时候，用一个数组记录被置0的行号，另一个数组记录被置0的列号。再次遍历矩阵，如果行或者列比MARK过，则置零。

时间复杂度：O(m * n). 遍历矩阵以求解哪些行列需要置零O(m * n), 再次遍历数组置零。O(2 * m * n).
空间复杂度为 O (m+n).

## 代码 - 空间复杂度 O(m+n)

```csharp
public class Solution {
    public void SetZeroes(int[][] matrix) {

        int r = matrix.Length;
        int c = matrix[0].Length;

        bool[] rx = new bool[r];
        bool[] cx = new bool[c];

        for(int i = 0; i < r; i++ ) {
            for(int j = 0; j < c; j++)
            {
                if(matrix[i][j] == 0)
                {
                    rx[i] = true;
                    cx[j] = true;
                }
            }
        }

        for(int i = 0; i < r; i++ ) {
            for(int j = 0; j < c; j++)
            {
                if(rx[i] || cx[j])
                    matrix[i][j] = 0;
            }
        }
    }
}
```

## 思路 - MASK - O(1)

上面的思路虽然直观，但是空间复杂的为 O(m+n), 从空间复杂度来说并不是题目要求的结果。为了优化空间复杂度，到达O(1)的要求。可以设定一个int的常量作为MASK。当第一次扫描二维矩阵时，当遇到0的时候，就地将`非0`元素转化为MASK的值。转化非0元素，是为了避免将同行或者同列其他的0清除掉。如果提前清除掉这些0，会导致数据的丢失。

当然这个算法有个BUG，就是作为MASK的值，如果出现在matrix中，就会被忽略。

## 代码 - MASK - O(1)

```csharp
public class Solution {
    public void SetZeroes(int[][] matrix) {

        int r = matrix.Length;
        int c = matrix[0].Length;
        int MASK = 2000000000;

        for(int i = 0; i < r; i++ ) {
            for(int j = 0; j < c; j++)
            {
                if(matrix[i][j] == 0)
                {
                    matrix[i][j] = MASK;
                    for(int ii = 0; ii < r; ii++ )
                        if(matrix[ii][j] != 0) matrix[ii][j] = MASK;
                    for(int jj = 0; jj < c; jj++ )
                        if(matrix[i][jj] != 0) matrix[i][jj] = MASK;
                }
            }
        }

        for(int i = 0; i < r; i++ ) {
            for(int j = 0; j < c; j++)
            {
                if(matrix[i][j] == MASK)
                    matrix[i][j] = 0;
            }
        }
    }
}
```

## 思路 - 首行列 - O(1)

另外一种想法，就是将第0行和第0列来暂存某元素出现0的情况。例如，当`matrix[i][j]=0`, 那么就将`matrix[i][0]=0` 和 ``matrix[0][j]=0`. 第二轮转换0的时候，只要判断首行列是否为0就可以了。但是这么做有个要注意的问题，就是首行列本身的数据就会被覆盖，要考虑首行列出现0的情况。

1. 检测首行列的0的情况，如果有0，标识行或者列要被置0.
2. 从i=1 & j=1开始扫描，将对应的首行列内值设为0.
3. 按照首行列的结果，将matrix内的值设置为0.
4. 开始转换首行列内的0.

## 代码 - 首行列 - O(1)

```csharp
public class Solution {
    public void SetZeroes(int[][] matrix) {

        int r = matrix.Length;
        int c = matrix[0].Length;

        bool isVertical = false;
        bool isHorizontal = false;

        if(matrix[0][0] == 0)
        {
            isVertical = true;
            isHorizontal = true;
        }
        else
        {
            for(int i = 1; i < r; i++ )
            {
                if(matrix[i][0] == 0)
                {
                    matrix[0][0] = 0;
                    isHorizontal = true;
                }
            }
            for(int j = 1; j < c; j++)
            {
                if(matrix[0][j] == 0)
                {
                    matrix[0][0] = 0;
                    isVertical = true;
                }
            }
        }

        for(int i = 0; i < r; i++ ) {
            for(int j = 0; j < c; j++)
            {
                if(matrix[i][j] == 0)
                {
                    matrix[0][j] = 0;
                    matrix[i][0] = 0;
                }
            }
        }

        for(int i = 1; i < r; i++ ) {
            for(int j = 1; j < c; j++)
            {
                if(matrix[i][0] == 0 || matrix[0][j] == 0)
                    matrix[i][j] = 0;
            }
        }
        if(isHorizontal) {
            for(int i = 1; i < r; i++ )
                matrix[i][0] = 0;
        }
        if(isVertical) {
            for(int j = 1; j < c; j++ )
                matrix[0][j] = 0;
        }

    }
}
```
