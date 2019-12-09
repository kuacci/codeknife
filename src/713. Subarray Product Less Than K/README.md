# [Medium][713. Subarray Product Less Than K](https://leetcode.com/problems/subarray-product-less-than-k/)

Your are given an array of positive integers nums.

Count and print the number of (contiguous) subarrays where the product of all the elements in the subarray is less than k.

**Example 1:**

> Input: nums = [10, 5, 2, 6], k = 100
> Output: 8
> Explanation: The 8 subarrays that have product less than 100 are: [10], [5], [2], [6], [10, 5], [5, 2], [2, 6], [5, 2, 6].
> Note that [10, 5, 2] is not included as the product of 100 is not strictly less than k.

**Note:**

> 0 < nums.length <= 50000.
> 0 < nums[i] < 1000.
> 0 <= k < 10^6.

## 思路 - dp

利用一个dp数组，记录从i位置一直乘到j位置的值，如果`dp[i] < k`则`ans += 1`. 如果`dp[i] >= k` 则转到下一层循环。

时间复杂度：O(N ^ 2)
空间复杂度：O(N)

## 代码 - dp

```csharp
public class Solution {
    public int NumSubarrayProductLessThanK(int[] nums, int k) {
        int N = nums.Length;
        int ans = 0;
        int[] dp = new int[N + 1];

        for(int i = 0; i < N; i++)
        {
            dp[i] = 1;
            for(int j = i + 1; j <= N; j++)
            {
                int val = dp[j - 1] * nums[j - 1];
                if(val >= k) break;
                ans += 1;
                dp[j] = val;
            }
        }
        return ans;
    }
}
```

## 思路 - 双指针

并非所有情况都适合用dp的。这里可以通过观察总结出来一些特点，用数学方式解决可以更加便捷。
可以采用双指针的方式来进行计算。`int l`指向数组的左端，`int r`则是右端。

1. 从0的位置开始，每次将r向右移动1位。一直计算到数组的最后一个元素。
2. 用int cur计算从l到r之间的数只积。初始值是`nums[0]`, 当`r`每移动一步，则`cur *= nums[r]`.
3. 如果`cur >= k`，那么超出范围了。要将`l`移动一位，并且将`cur`中去掉`l`对应的值`nums[l]`. 即，`cur /= nums[l]`.
4. 计算r到l之间有多少组合可以用`ans += r - l + 1`来表示。

算法复杂度：O(N).数组扫描一遍，最坏的情况下每个元素被touch了2遍。
空间复杂度：O(1)

## 代码 - 双指针

```csharp
public class Solution {
    public int NumSubarrayProductLessThanK(int[] nums, int k) {
        if(k <= 1) return 0;
        int N = nums.Length;
        int ans = 1;
        int cur = nums[0];

        for(int r = 1, l = 0; r < nums.Length; r++)
        {
            cur *= nums[r];
            while(cur >= k) cur /= nums[l++];
            ans += r - l + 1;
        }
        return ans;
    }
}
```
