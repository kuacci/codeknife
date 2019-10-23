# [Medium][861. Score After Flipping Matrix](https://leetcode.com/problems/score-after-flipping-matrix/)

We have a two dimensional matrix A where each value is 0 or 1.

A move consists of choosing any row or column, and toggling each value in that row or column: changing all 0s to 1s, and all 1s to 0s.

After making any number of moves, every row of this matrix is interpreted as a binary number, and the score of the matrix is the sum of these numbers.

Return the highest possible score.

``` text
Example 1:

Input: [[0,0,1,1],[1,0,1,0],[1,1,0,0]]
Output: 39
Explanation:
Toggled to [[1,1,1,1],[1,0,0,1],[1,1,1,1]].
0b1111 + 0b1001 + 0b1111 = 15 + 9 + 15 = 39
```

Note:

1. 1 <= A.length <= 20
2. 1 <= A[0].length <= 20
3. A[i][j] is 0 or 1.

## 思路

题目的要求是将一个输入矩阵通过toggle操作，变换成一个二进制数的和最大的情况。toggel操作的时候，可以选择行或者列为基准全部取反。
例如将第一行toggle

$$
\left[
 \begin{matrix}
   0 & 0 & 1 & 1 \\
   - & - & - & - \\
   1 & 0 & 1 & 0 \\
   1 & 1 & 0 & 0
  \end{matrix}
\right]
$$

转化之后

$$
\left[
 \begin{matrix}
   1 & 1 & 0 & 0 \\
   - & - & - & - \\
   1 & 0 & 1 & 0 \\
   1 & 1 & 0 & 0
  \end{matrix}
\right]
$$

由于是转化成二进制求和，那么最高位是1的情况是数值最大的情况。例如`1000` 一定比 `0111`大。
所以第一步要做的就是将最高位（左侧）全部置成1.
后面要做的就是变换较低位。可以翻转到1出现的最多的情况。

前：
$$
\left[
 \begin{matrix}
   1 & 1 &  *0*  & 0 \\
   1 & 0 &  *1*  & 0 \\
   1 & 1 &  *0*  & 0
  \end{matrix}
\right]
$$

后：
$$
\left[
 \begin{matrix}
   1 & 1 & *1* &  0 \\
   1 & 0 & *0* & 0 \\
   1 & 1 & *1* & 0
  \end{matrix}
\right]
$$

原则是：

1. 最高位无论是1还是0，原则是要保证最高位是1. 因此同一列里面，如果跟最一致，则不会变换，如果不一致，则会被toggle。
2. 同一列中，取相同数最多的数量。如果0的个数大于1的个数，那么并不需要正的toggle。只要统计0的个数即可。因为toggle后，0就会变成1.

## 代码

```csharp
public class Solution {
    public int MatrixScore(int[][] A) {

        int C = A[0].Length;
        int R = A.Length;

        int ans = 0;

        for(int c = 0; c < C; c++) // col index
        {
            int cnt = 0;
            for(int r = 0; r < R; r++) // row index
            {
                cnt += A[r][c] ^ A[r][0];
            }

            ans += Math.Max(cnt, R - cnt) * (1 << C - c - 1);
        }

        return ans;

    }
}
```
