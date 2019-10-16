# [220. Contains Duplicate III](https://leetcode-cn.com/problems/contains-duplicate-iii/)

Given an array of integers, find out whether there are two distinct indices i and j in the array such that the absolute difference between nums[i] and nums[j] is at most t and the absolute difference between i and j is at most k.

Example 1:

> Input: nums = [1,2,3,1], k = 3, t = 0
> Output: true

Example 2:

> Input: nums = [1,0,1,1], k = 1, t = 2
> Output: true

Example 3:

> Input: nums = [1,5,9,1,5,9], k = 2, t = 3
> Output: false

## 思路 - 暴力破解

给出一个数组，搜索数组中是存在这样的情况:

* j - i <= k
* Math.Abs(nums[i] - nums[j]) <= t

即，给定一个互动窗口的范围 `j - i <= k`， 这个滑动窗口内任意2个值的差值<= t, 那么返回true, 否则就是false.

nums[i] - nums[j]的时候，有可能输入值包含了int.MaxValue或者 int.MinValue的情况。为了不让数据溢出，使用double类型。 `Math.Abs((double)nums[i] - (double)nums[j]`。

时间复杂度为 O(n * min(n,k)). 整个`int[]`会遍历一遍，在滑动窗口有第二层循环。
空间复杂度O(1), 没有借助额外的存储空间。

## 代码 - 暴力破解

```csharp
public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
    {
        for (int i = 0; i < nums.Length; ++i)
        {
            for (int j = Math.Max(i - k, 0); j < i; ++j)
            {
                if (Math.Abs((double)nums[i] - (double)nums[j]) <= t) return true;
            }
        }
        return false;
    }
}
```
