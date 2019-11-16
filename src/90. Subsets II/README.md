# [Medium][90. Subsets II](https://leetcode.com/problems/subsets-ii/)

Given a collection of integers that might contain duplicates, nums, return all possible subsets (the power set).

Note: The solution set must not contain duplicate subsets.

**Example:**

```text
Input: [1,2,2]
Output:
[
  [2],
  [1],
  [1,2,2],
  [2,2],
  [1,2],
  []
]
```

## 思路 - 回溯

这题是[78. Subsets](src/78.%20Subsets)的进阶版。输入的`nums[]`中有重复的数字。为了去掉重复的情况，在同一层循环中如果前后两个相同就要跳过。因为相同的数字已经计算过了。

```csharp
    for (int i = pos; i < nums.Length; i++)
    {
        if (i > pos && nums[i] == nums[i - 1]) continue;
        //...
    }
```

## 代码 - 回溯

```csharp
public IList<IList<int>> SubsetsWithDup(int[] nums)
{

    Array.Sort(nums);
    IList<IList<int>> ans = new List<IList<int>>();
    SubsetsWithDupBacktrack(ans, new List<int>(), nums, 0);
    ans.Add(new List<int>());
    return ans;
}

private void SubsetsWithDupBacktrack(IList<IList<int>> ans, IList<int> mem, int[] nums, int pos)
{
    for (int i = pos; i < nums.Length; i++)
    {
        if (i > pos && nums[i] == nums[i - 1]) continue;

        mem.Add(nums[i]);
        List<int> res = new List<int>();
        res.AddRange(mem);
        ans.Add(res);
        SubsetsWithDupBacktrack(ans, mem, nums, i + 1);
        mem.RemoveAt(mem.Count - 1);
    }
}
```
