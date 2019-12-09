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
