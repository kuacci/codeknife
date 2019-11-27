# [Medium][113. Path Sum II](https://leetcode-cn.com/problems/path-sum-ii/)

Given a binary tree and a sum, find all root-to-leaf paths where each path's sum equals the given sum.

Note: A leaf is a node with no children.

Example:

```text
Given the below binary tree and sum = 22,

      5
     / \
    4   8
   /   / \
  11  13  4
 /  \    / \
7    2  5   1
Return:

[
   [5,4,11,2],
   [5,8,4,5]
]
```

## 思路 - backtrack

这题的要求是统计从根节点到叶子节点的和为sum的路径总数。
想法是每个节点可能有左子树和右子树，所以一个个加下去做尝试。这里比较好的方式就是用backtrack。

终止的条件为：

1. node为叶子节点，即`node.left == null && node.right == null`
2. `node.val = sum`

```cshapr
if (sum == node.val && node.right == null && node.left == null)
{
    List<int> r = new List<int>();
    r.AddRange(res);
    r.Add(node.val);
    ans.Add(r);
    return;
}
```

回溯的核心代码如下：

```csharp
res.Add(node.val);
PathSumHelper(ans, res, node.left, sum - node.val);
PathSumHelper(ans, res, node.right, sum - node.val);
res.RemoveAt(res.Count - 1);
```

## 代码 - backtrack

```csharp
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    public IList<IList<int>> PathSum(TreeNode root, int sum)
    {
        IList<IList<int>> ans = new List<IList<int>>();
        PathSumHelper(ans, new List<int>(), root, sum);
        return ans;
    }

    private void PathSumHelper(IList<IList<int>> ans, List<int> res, TreeNode node, int sum)
    {
        if (node == null) return;
        if (sum == node.val && node.right == null && node.left == null)
        {
            List<int> r = new List<int>();
            r.AddRange(res);
            r.Add(node.val);
            ans.Add(r);
            return;
        }
        res.Add(node.val);
        PathSumHelper(ans, res, node.left, sum - node.val);
        PathSumHelper(ans, res, node.right, sum - node.val);
        res.RemoveAt(res.Count - 1);
    }
}
```
