# [Medium][77. Combinations](https://leetcode.com/problems/combinations/)

Given two integers n and k, return all possible combinations of k numbers out of 1 ... n.

**Example:**

```text
Input: n = 4, k = 2
Output:
[
  [2,4],
  [3,4],
  [2,3],
  [1,2],
  [1,3],
  [1,4],
]
```

## 思路 - DP

这道题的意思不是很清晰，看了几遍以后发现它的目的是给出一个n值，代表着有从`1 - n`个数字可选，给出一个k值，从这 `1 - n`个数中选出来k个数字，做全排列。这是一个典型的全排列。

## 代码 - DP

```csharp
public IList<IList<int>> Combine(int n, int k)
{
    IList<IList<int>> ans = new List<IList<int>>();
    if (n == 0 || k == 0) return ans;

    CombineHelper(ans, new List<int>(), n, k, 1);


    return ans;
}

private void CombineHelper(IList<IList<int>> ans, List<int> res, int n, int k, int pos)
{
    if (res.Count == k)
    {
        List<int> r = new List<int>();
        r.AddRange(res);
        ans.Add(r);
    }
    else
    {
        for (int i = pos; i <= n; i++)
        {
            res.Add(i);
            CombineHelper(ans, res, n, k, i + 1);
            res.RemoveAt(res.Count - 1);
        }
    }
}
```
