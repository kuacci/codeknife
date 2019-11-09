# [Medium][48. Rotate Image](https://leetcode.com/problems/rotate-image/)

You are given an n x n 2D matrix representing an image.

Rotate the image by 90 degrees (clockwise).

Note:

You have to rotate the image in-place, which means you have to modify the input 2D matrix directly. DO NOT allocate another 2D matrix and do the rotation.

**Example 1:**

```text
Given input matrix =
[
  [1,2,3],
  [4,5,6],
  [7,8,9]
],

rotate the input matrix in-place such that it becomes:
[
  [7,4,1],
  [8,5,2],
  [9,6,3]
]
```

**Example 2:**

```text
Given input matrix =
[
  [ 5, 1, 9,11],
  [ 2, 4, 8,10],
  [13, 3, 6, 7],
  [15,14,12,16]
],

rotate the input matrix in-place such that it becomes:
[
  [15,13, 2, 5],
  [14, 3, 4, 1],
  [12, 6, 8, 9],
  [16, 7,10,11]
]
```

## 思路 - 旋转四个矩形

来研究每个元素在旋转的过程中如何移动。

![img](image/figure1.png)
![img](image/figure2.png)

先将四个角分别顺时针移动一位。这样只要一个变量作为缓存即可，每次记录一下被替换的那个位置的值。每次顺时针转动的时候，下一位的坐标都是同样的规律。假设当前的坐标是`[row, col]`, 那么下一步将会移动到`[N - col, row]`， 四个角要移动4次，为一次循环。

```csharp
for(int i = 0; i < 4; i++)
{
    rdx = cdx;
    cdx = N - rdx1;
    num1 = matrix[rdx][cdx];
    matrix[rdx][cdx] = num;
    num = num1;
    rdx1 = rdx;
}
```

轮完一圈之后，向右边移动一位，做下一轮顺时针的旋转。第一行的时候起始列位置为0, 终止位置为 matrix.Length - 2. 最后一位不做翻转，因为最后一位起始是当前行第一个翻转过来的数字。当做完一行以后，起始的列位置与行位置一直，如`[0,0], [1,1], [2,2]`. 终止位置也相应的要收缩 matrix.Length - 2 - r. 所以列的循环为:

```csharp
for(int c = r; c < matrix.Length - 1 - r; c++)
```

至于行的循环只要进行到 `n / 2`即可。

时间复杂度： O(N^2), N X N 的矩阵，每个元素都访问一遍。
空间复杂度： O(1), 只要一个临时变量

## 代码 - 旋转四个矩形

```csharp
public class Solution {
    int N = 0;
    public void Rotate(int[][] matrix) {

        this.N = matrix.Length - 1;

        for(int r = 0; r <= matrix.Length / 2; r++)
        {
            for(int c = r; c < matrix.Length - 1 - r; c++)
            {

                int num = matrix[r][c];
                int num1 = num;
                int rdx = r;
                int rdx1 = rdx;
                int cdx = c;

                for(int i = 0; i < 4; i++)
                {
                    rdx = cdx;
                    cdx = N - rdx1;
                    num1 = matrix[rdx][cdx];
                    matrix[rdx][cdx] = num;
                    num = num1;
                    rdx1 = rdx;
                }
            }
        }
    }
}
```
