# [Easy] [1. Two Sum](https://leetcode.com/problems/two-sum/)

Given an array of integers, return indices of the two numbers such that they add up to a specific target.  
You may assume that each input would have exactly one solution, and you may not use the same element twice.  
Example:
Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].

## 思路

1. 使用 hash 算法的 Dictionary 保存当前位置上的值与 index。即将当前的值看作为后续数字可匹配的差值。
2. 在后续的遍历时检查该 dictionary 中是否存在与当前值匹配的差值，若存在即返回当前 index 与 差值对应的 index，若不存在则继续执行 #1 中的操作。

时间复杂度：O(N), 数组遍历一遍为 O(N), 从Dictionary查找所需要的时间是O(1).
空间复杂度：O(N), 用Dictionary存储了数组。

### 代码

```csharp
public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        var dic = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (dic.ContainsKey(target - nums[i]))
            {
                return new int[] { i, dic[target - nums[i]] };
            }
            else
            {
                dic.Add(nums[i], i);
            }
        }
        return new int[] { 0, 0 };
    }
}
```
