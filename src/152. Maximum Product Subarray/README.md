# [Medium][152. Maximum Product Subarray](https://leetcode.com/problems/maximum-product-subarray/)

Given an integer array nums, find the contiguous subarray within an array (containing at least one number) which has the largest product.

**Example 1:**

> Input: [2,3,-2,4]
> Output: 6
> Explanation: [2,3] has the largest product 6.

**Example 2:**

> Input: [-2,0,-1]
> Output: 0
> Explanation: The result cannot be 2, because [-2,-1] is not a subarray.

## 思路 - dp

比较暴力的算法是创建一个同等规模的dp table`int[,] dp = new int[N, N + 1];`用来记录整个乘法的过程，dp[i,i] 存放的是1. `dp[i, j]`存放的是`dp[i, j - 1] * nums[j - 1]`的值。用`int ans`保存在整个运算工程中最大的结果。

时间复杂度：O(N ^ 2)
空间复杂度: O(N ^ 2)

## 代码 - dp

```csharp
public class Solution {
    public int MaxProduct(int[] nums) {
        if(nums == null || nums.Length == 0) return 0;
        int N = nums.Length;
        int ans = int.MinValue;
        int[,] dp = new int[N, N + 1];
        for(int i = 0; i < N; i++)
        {
            dp[i, i] = 1;
        }

        for(int i = 0; i < N; i ++)
        {
            for(int j = i + 1; j <= N ; j++)
            {
                dp[i, j] = dp[i, j - 1] * nums[ j - 1];
                ans = Math.Max(dp[i, j], ans);
            }
        }
        return ans;
    }
}
```

## 思路 - dp - 优化

上面的计算时间复杂度为O(N ^ 2). 有没有可能缩短为O(N)呢。观察输入的数字进行分析，大致分为3种情况：

1. 整个nums里面全部都是正数。这个情况下最大的积就是从第一个数字一直乘到最后一个。
   例如：`[1,2,3,4,5]`, 最大积自然是从头乘到尾。做一次遍历即可。

2. 整个nums里面有负数的情况。这个时候情况比较复杂。

   * 只有一个负数的情况。`[1,2,-3,4,5]`，由于有负数的存在，所以在负数的这个位置要断开，分别计算负数前面和后面谁比较大。
   * 存在两个负数的情况。`[-1,2,3,4,-1]`. 有2个负数的时候，负负得正，相当于没有负数，即回到了1#的情况。
   * 存在复数个负数的情况。最后回根据负数的个数是基数还是偶数落到上面两种情况中的一种。
   * 思路：在这种情况下，用两个临时变量`int imax`和`int imin`,分别记录正数和负数。当遇到一个负数的时候，将imax和imin两个数交换。在遍历完一遍数组之后，取最大值即可。也能做到遍历一遍数组的要求，做到O(N).

3. nums中存在0的情况。`0 * x = 0`, 只要遇到0就会被截断。所以这个时候要引入第三个临时变量，记录切断之前的一个最大值, 也就是最后返回的最大值`int max`. 剩下的就是不断重复上面1#和2#的情况，直到结束。

时间复杂度：O(N)
空间复杂度：O(1)

## 代码 - dp - 优化

```csharp
public class Solution {
    public int MaxProduct(int[] nums) {
        int max = int.MinValue, imax = 1, imin = 1;
        for(int i = 0; i <nums.Length; i++){
            if(nums[i] < 0)
            {
              int tmp = imax;
              imax = imin;
              imin = tmp;
            }
            imax = Math.Max(imax * nums[i], nums[i]);
            imin = Math.Min(imin * nums[i], nums[i]);

            max = Math.Max(max, imax);
        }
        return max;
    }
}
```
