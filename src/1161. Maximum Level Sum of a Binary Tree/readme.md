# [1161. Maximum Level Sum of a Binary Tree](https://leetcode.com/problems/maximum-level-sum-of-a-binary-tree/)

Given the root of a binary tree, the level of its root is 1, the level of its children is 2, and so on.

Return the smallest level X such that the sum of all the values of nodes at level X is maximal.

Example 1:

![img](image/capture.jpg)

```text
Input: [1,7,0,7,-8,null,null]
Output: 2
Explanation:
Level 1 sum = 1.
Level 2 sum = 7 + 0 = 7.
Level 3 sum = 7 + -8 = -1.
So we return the level with the maximum sum which is level 2.
```

Note:

```text
The number of nodes in the given tree is between 1 and 10^4.
-10^5 <= node.val <= 10^5
```

## 思路

由于node的数目可能是 1 ~ 10^4. 想法是遍历每一层，计算最大求和以及所在的level。采用中序遍历的方式，先求和，然后分别计算左右子树。为了能够清楚的知道是第几层，传入一个int level，标识当期计算的是第几层。将结果暂存到一个List中。

## 代码

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

    private List<int> sums = new List<int>();

    public int MaxLevelSum(TreeNode root) {

        int lvl = 0;
        int max = 0;

        SumVal(root, 1);

        for(int i = 0; i < sums.Count; i++)
        {
            if(sums[i] > max)
            {
                lvl = i + 1;
                max = sums[i];
            }
        }

        return lvl;

    }

    private void SumVal(TreeNode root, int level)
    {
        if(root == null)
            return;

        if(sums.Count < level)
            sums.Add(root.val);
        else
        {
            sums[level - 1] += root.val;
        }

        SumVal(root.left, level + 1);
        SumVal(root.right, level + 1);
    }
}
```
