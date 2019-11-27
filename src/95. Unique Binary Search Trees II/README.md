# [Medium][95. Unique Binary Search Trees II](https://leetcode.com/problems/unique-binary-search-trees-ii/)

Given an integer n, generate all structurally unique BST's (binary search trees) that store values 1 ... n.

**Example:**

```text
Input: 3
Output:
[
  [1,null,3,2],
  [3,2,null,1],
  [3,1,null,null,2],
  [2,1,3],
  [1,null,2,null,3]
]
Explanation:
The above output corresponds to the 5 unique BST's shown below:

   1         3     3      2      1
    \       /     /      / \      \
     3     2     1      1   3      2
    /     /       \                 \
   2     1         2                 3
```

## 思路 - dp

本题的思路跟[96. Unique Binary Search Trees](src/96.%20Unique%20Binary%20Search%20Trees)非常类似。在上题中，计算sum的方式是`sum += (lcount * rcount);`, 在这里却要转换成为生成对应的TreeNode的组合。怎么样才能做到这一点？

可以将计算出现数字的地方修改为返回一个TreeNode. 在计算`sum += (lcount * rcount);`的地方，则转换成遍历左右节点的组合：

```csharp
for (int i = lo; i <= hi; i++)
{
    IList<TreeNode> leftTree = GenerateTrees(lo, i - 1);
    IList<TreeNode> rightTree = GenerateTrees(i + 1, hi);

    foreach (var l in leftTree)
    {
        foreach (var r in rightTree)
        {
            TreeNode root = new TreeNode(i);
            root.left = l;
            root.right = r;
            ans.Add(root);
        }
    }
}
```

## 代码 - dp

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
    public IList<TreeNode> GenerateTrees(int n)
    {
        if(n == 0)
        {
            return new List<TreeNode>();
        }
        return GenerateTrees(1, n);
    }

    private IList<TreeNode> GenerateTrees(int lo, int hi)
    {
        IList<TreeNode> ans = new List<TreeNode>();
        if(lo > hi)
        {
            ans.Add(null);
            return ans;
        }

        for (int i = lo; i <= hi; i++)
        {
            IList<TreeNode> leftTree = GenerateTrees(lo, i - 1);
            IList<TreeNode> rightTree = GenerateTrees(i + 1, hi);

            foreach (var l in leftTree)
            {
                foreach (var r in rightTree)
                {
                    TreeNode root = new TreeNode(i);
                    root.left = l;
                    root.right = r;
                    ans.Add(root);
                }
            }
        }
        return ans;
    }
}
```
