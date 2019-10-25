# [Easy][169. Majority Element](https://leetcode.com/problems/majority-element/)

Given an array of size n, find the majority element. The majority element is the element that appears more than ⌊ n/2 ⌋ times.

You may assume that the array is non-empty and the majority element always exist in the array.

**Example 1:**

> Input: [3,2,3]
> Output: 3

**Example 2:**

> Input: [2,2,1,1,1,2,2]
> Output: 2

## 思路 - 计数排序

这里要求计算出一个计算出一个一个majority element, 在数组中出现的次数大于数组元素的一半。考虑到这种情况，有数字经常性的重复出现，首先想到的是要用计数排序。统计某个数字出现的次数。如果这个数字出现的次数大于 n / 2, 那么久返回那个数字。

## 代码 - 计数排序

```csharp
public class Solution {
    public int MajorityElement(int[] nums) {
        int mlen = nums.Length / 2;
        Dictionary<int, int> dic = new Dictionary<int, int>();
        int ans = 0;
        for(int i = 0; i < nums.Length; i++)
        {
            if(dic.ContainsKey(nums[i]))
                dic[nums[i]] += 1;
            else
                dic.Add(nums[i], 1);

            if(dic[nums[i]] > mlen)
            {
                ans = nums[i];
            }
        }
        return ans;
    }
}
```
