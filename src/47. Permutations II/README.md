# [Medium][47. Permutations II](https://leetcode.com/problems/permutations-ii/)

Given a collection of numbers that might contain duplicates, return all possible unique permutations.

Example:

```text
Input: [1,1,2]
Output:
[
  [1,1,2],
  [1,2,1],
  [2,1,1]
]
```

## 思路 - back track + 去重

这题的要求跟[46. Permutations](src/46.%20Permutations) 非常的相似。但是多了一个去重的要求。这里借鉴了[回溯 + 剪枝（Python、Java、C++）](https://leetcode-cn.com/problems/permutations-ii/solution/hui-su-suan-fa-python-dai-ma-java-dai-ma-by-liwe-2/)

## 代码 - back track + 去重

```csharp
public class Solution {
    private IList<IList<int>> res = new List<IList<int>>();
    private bool[] used;

    private void findPermuteUnique(int[] nums, int depth, Stack<int> stack)
    {
        if (depth == nums.Length)
        {
            res.Add(new List<int>(stack));
            return;
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (!used[i])
            {
                // 修改 2：因为排序以后重复的数一定不会出现在开始，故 i > 0
                // 和之前的数相等，并且之前的数还未使用过，只有出现这种情况，才会出现相同分支
                // 这种情况跳过即可
                if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) { continue; }
                used[i] = true;
                stack.Push(nums[i]);
                findPermuteUnique(nums, depth + 1, stack);
                stack.Pop();
                used[i] = false;
            }
        }
    }

    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        if (nums == null  || nums.Length == 0) { return res; }
        // 修改 1：首先排序，之后才有可能发现重复分支
        Array.Sort(nums);
        used = new bool[nums.Length];
        findPermuteUnique(nums, 0, new Stack<int>());
        return res;
    }
}
```
