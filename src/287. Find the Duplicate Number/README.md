# [Medium][287. Find the Duplicate Number](https://leetcode.com/problems/find-the-duplicate-number/)

Given an array nums containing n + 1 integers where each integer is between 1 and n (inclusive), prove that at least one duplicate number must exist. Assume that there is only one duplicate number, find the duplicate one.

**Example 1:**

> Input: [1,3,4,2,2]
> Output: 2

**Example 2:**

> Input: [3,1,3,4,2]
> Output: 3

**Note:**

> You must not modify the array (assume the array is read only).
> You must use only constant, O(1) extra space.
> Your runtime complexity should be less than O(n2).
> There is only one duplicate number in the array, but it could be repeated more than once.

## 思路 - 1

先将这个数组排序，如果有重复数字，必然存在`nums[i-1] == nums[i]`.

时间复杂度： O(NlgN). 假设排序是用了O(NlgN), 再对数组进行遍历为O(N)，总时间复杂度为O(NlgN + N) = O(NlgN)
空间复杂度： O(1)

## 代码 - 1

```csharp
public class Solution {
    public int FindDuplicate(int[] nums) {
        Array.Sort(nums);
        for(int i = 1; i < nums.Length; i++)
            if(nums[i - 1] == nums[i]) return nums[i];
        return -1;
    }
}
```
