# [Medium][39. Combination Sum](https://leetcode.com/problems/combination-sum/)

Given a set of candidate numbers (candidates) (without duplicates) and a target number (target), find all unique combinations in candidates where the candidate numbers sums to target.

The same repeated number may be chosen from candidates unlimited number of times.

Note:

All numbers (including target) will be positive integers.
The solution set must not contain duplicate combinations.

**Example 1:**

```text
Input: candidates = [2,3,6,7], target = 7,
A solution set is:
[
  [7],
  [2,2,3]
]
```

**Example 2:**

```text
Input: candidates = [2,3,5], target = 8,
A solution set is:
[
  [2,2,2,2],
  [2,3,3],
  [3,5]
]
```

## 思路 - 回溯

主要的思路就是用回溯去尝试各种的可能性。核心代码是下面这块：

```csharp
for (int i = pos; i < candidates.Length; i++)
{
    int counts = 0;

    while(total < target)
    {
        res.Add(candidates[i]);
        total += candidates[i];
        counts++;
        CombinationSumHelper(ans, res, candidates, total, i + 1, target);
    }

    for (int j = 0; j < counts; j++)
    {
        total -= res[res.Count - 1];
        res.RemoveAt(res.Count - 1);
    }
}
```

既然是回溯，自然要回复现场，在递归完成之后，要将之前添加进去的数字都删除掉。每次数字的起点不是数组的头部，而是由上一层循环决定的一个位置，继续下去计算。所以起点是`i = pos`, 下一个递归的起点是 `i + 1`.

因为题设中，重复的数字是允许的，所以要一直重复的加某个数字直到 `total > target`.

步骤如下图 ：

![image](image/figure1.png)
![image](image/figure2.png)
![image](image/figure3.png)

## 代码 - 回溯

```csharp
public class Solution {
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
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
                int counts = 0;

                while(total < target)
                {
                    res.Add(candidates[i]);
                    total += candidates[i];
                    counts++;
                    CombinationSumHelper(ans, res, candidates, total, i + 1, target);
                }

                for (int j = 0; j < counts; j++)
                {
                    total -= res[res.Count - 1];
                    res.RemoveAt(res.Count - 1);
                }
            }
        }
    }

}
```
