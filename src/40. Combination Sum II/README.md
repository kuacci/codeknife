# [Medium][40. Combination Sum II](https://leetcode.com/problems/combination-sum-ii/)

Given a collection of candidate numbers (candidates) and a target number (target), find all unique combinations in candidates where the candidate numbers sums to target.

Each number in candidates may only be used once in the combination.

Note:

All numbers (including target) will be positive integers.
The solution set must not contain duplicate combinations.

**Example 1:**

```text
Input: candidates = [10,1,2,7,6,1,5], target = 8,
A solution set is:
[
  [1, 7],
  [1, 2, 5],
  [2, 6],
  [1, 1, 6]
]
```

**Example 2:**

```text
Input: candidates = [2,5,2,1,2], target = 5,
A solution set is:
[
  [1,2,2],
  [5]
]
```

## 思路 - 回溯

这题跟[39. Combination Sum](src/39.%20Combination%20Sum)非常相似。
区别点在于：

1. 数字不能叠加使用。
2. 重复的组合要去掉。

数字不能叠加使用的要求很好达到，将不在叠加数字，每次只传入当前的数字即可。

```csharp
for (int i = pos; i < candidates.Length; i++)
{
    // if (i > pos && candidates[i] == candidates[i - 1]) continue;
    res.Add(candidates[i]);
    CombinationSumHelper(ans, res, candidates, total + candidates[i], i + 1, target);
    res.RemoveAt(res.Count - 1);
}
```

不能出现重复的组合要如何去掉呢。由于初始化的时候，已经对数组排过序。如果出现由重复的数字，必然是挨着的。即 `candidates[i] == candidates[i-1]`. 遇到这种情况跳过即可，因为组合已经在前面出现过了。

## 代码 - 回溯

```csharp
public class Solution {
    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        Array.Sort(candidates);
        IList<IList<int>> ans = new List<IList<int>>();

        CombinationSumHelper(ans, new List<int>(), candidates, 0, 0, target);

        return ans;
    }

    private void CombinationSumHelper(IList<IList<int>> ans, IList<int> res, int[] candidates, int total, int pos, int target)
    {

        if (total == target)
        {
            List<int> r = new List<int>();
            r.AddRange(res);
            ans.Add(r);
        }
        else if (total < target)
        {
            for (int i = pos; i < candidates.Length; i++)
            {
                if (i > pos && candidates[i] == candidates[i - 1]) continue;
                res.Add(candidates[i]);
                CombinationSumHelper(ans, res, candidates, total + candidates[i], i + 1, target);
                res.RemoveAt(res.Count - 1);
            }
        }
    }
}
```
