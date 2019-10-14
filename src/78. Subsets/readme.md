# [78. Subsets](https://leetcode.com/problems/subsets/)

Given a set of distinct integers, nums, return all possible subsets (the power set).

**Note:** The solution set must not contain duplicate subsets.

**Example:**

```text
Input: nums = [1,2,3]
Output:
[
  [3],
  [1],
  [2],
  [1,2,3],
  [1,3],
  [2,3],
  [1,2],
  []
]
```

## 思路 - 回溯算法

这道题目跟[17. Letter Combinations of a Phone Number](../17.%20Letter%20Combinations%20of%20a%20Phone%20Number)很类似. 要通过回溯算法遍历所有的可能性。
区别在于，遍历`nums[]`的时候，进入到下层递归调用的时候，起始位置是递进的。所有需要传入一个其实位置start作为for循环的起始位置。而下一层起始位置`start`是当前的i的位置+1(i + 1).

## 代码 - 回溯算法

```csharp
public class Solution
{
    public IList<IList<int>> Subsets(int[] nums)
    {
        IList<IList<int>> ans = new List<IList<int>>();
        if (nums.Length == 0) return ans;
        ans.Add(new List<int>());
        IList<int> len = new List<int>();
        helper(ans, len, nums, 0);
        return ans;
    }

    private void helper(IList<IList<int>> ans, IList<int> len, int[] nums, int start)
    {
        for (int i = start; i < nums.Length; i++)
        {
            len.Add(nums[i]);
            IList<int> output = new List<int>(len);
            ans.Add(output);
            helper(ans, len, nums, i + 1);
            len.RemoveAt(len.Count - 1);
        }
    }
}
```
