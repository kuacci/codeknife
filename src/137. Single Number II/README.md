# [Medium][137. Single Number II](https://leetcode.com/problems/single-number-ii/)

Given a non-empty array of integers, every element appears three times except for one, which appears exactly once. Find that single one.

Note:

Your algorithm should have a linear runtime complexity. Could you implement it without using extra memory?

**Example 1:**

> Input: [2,2,3,2]
> Output: 3

**Example 2:**

> Input: [0,1,0,1,0,1,99]
> Output: 99

## 思路 - 排序

先对数组进行排序，当某个数跟他左右两边都不相等的时候，这个数就是单独的数。
时间复杂度：O(NlgN). 取决于排序的时间复杂度，假设使用的是快速排序那么时间复杂度是O(NlgN). 去重的过程只遍历一次数组，为O(N).
空间复杂度：O(1).

## 代码 - 排序

```csharp
public class Solution {
    public int SingleNumber(int[] nums) {
        if(nums.Length == 1) return nums[0];
        Array.Sort(nums);
        if(nums[0] != nums[1]) return nums[0];
        if(nums[nums.Length - 1] != nums[nums.Length - 2]) return nums[nums.Length - 1];

        for(int i = 1; i < nums.Length - 1; i++)
            if(nums[i] != nums[i - 1] && nums[i] != nums[i + 1]) return nums[i];
        return -1;
    }
}
```
