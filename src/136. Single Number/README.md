# [Medium][136. Single Number](https://leetcode.com/problems/single-number/)

Given a non-empty array of integers, every element appears twice except for one. Find that single one.

Note:

Your algorithm should have a linear runtime complexity. Could you implement it without using extra memory?

**Example 1:**

Input: [2,2,1]
Output: 1

**Example 2:**

Input: [4,1,2,1,2]
Output: 4

## 思路 - `List<int>`

由于重复的数字只出现2次，可以用一个`List<int>`保存未出现过的数字，如果数字已经出现过，那么移除它。最后没有出现过数字就是唯一的数字。

时间复杂度：O(N)，遍历一次数组，`List<int>`的插入和删除为O(1)。
空间复杂度：O(N)，最坏的情况下`List<int`保存了一半数组的元素。

## 代码 - - `List<int>`

```csharp
public class Solution {
    public int SingleNumber(int[] nums) {
        List<int> memo = new List<int>();
        for(int i = 0; i < nums.Length; i++)
        {
            if(memo.Contains(nums[i])) memo.Remove(nums[i]);
            else memo.Add(nums[i]);
        }
        return memo[0];
    }
}
```

## 思路 - 排序

先对数组进行排序，然后再比较前后的数字是否相同即可。
时间复杂度：O(NlgN). 取决于排序的时间复杂度，假设使用的是快速排序那么时间复杂度是O(NlgN). 去重的过程只遍历一次数组，为O(N).
空间复杂度：O(1).

## 代码 - 排序

```csharp
public class Solution {
    public int SingleNumber(int[] nums) {
        if(nums.Length == 1) return nums[0];
        Array.Sort(nums);
        if(nums[0] != nums[1]) return nums[0];
        if(nums[nums.Length - 1] != nums[nums.Length -2]) return nums[nums.Length - 1];
        for(int i = 1; i < nums.Length - 2; i++)
        {
            if(nums[i] != nums[i - 1] && nums[i] != nums[i + 1]) return nums[i];
        }
        return -1;
    }
}
```

## 思路 - 数学规律 - 异或

根据异或的的规则 A ^ A = 0, A ^ 0 = A。以及异或的交互律和结合律：A ^ b ^ A = (A ^ A) ^ B = 0 ^ B = B. 可以用异或除重复的数字。

## 代码  - 数学规律 - 异或

```csharp
public class Solution {
    public int SingleNumber(int[] nums) {
        int ans = nums[0];
        for(int i = 1; i < nums.Length; i++)
            ans ^= nums[i];
        return ans;
    }
}
```
