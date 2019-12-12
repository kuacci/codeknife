# [Medium][153. Find Minimum in Rotated Sorted Array](https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/)

Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.

(i.e.,  [0,1,2,4,5,6,7] might become  [4,5,6,7,0,1,2]).

Find the minimum element.

You may assume no duplicate exists in the array.

**Example 1:**

> Input: [3,4,5,1,2]
> Output: 1

**Example 2:**

> Input: [4,5,6,7,0,1,2]
> Output: 0

## 思路 - 暴力破解

这道题目的题设是给定一个已经排序的升序数组，在某一点上发生了偏转。可知没有重复数，且偏转的点只有一个。那么可以得出一个结论，发生偏转的点存在一个特点是:`nums[i - 1] > nums[i]`. 最小的数出现在`nums[i]`的位置. 当然也有可能没有发生偏转，那么最小值在`nums[0]`. 采用遍历的方式判断这个偏转点的位置。

时间复杂度：O(N)
空间复杂度：O(1)

## 代码 - 暴力破解

```csharp
public class Solution {
    public int FindMin(int[] nums) {
        for(int i = 1; i < nums.Length; i++)
            if(nums[i - 1] > nums[i]) return nums[i];
        return nums[0];
    }
}
```
