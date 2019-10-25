# [Medium][103. Binary Tree Zigzag Level Order Traversal](https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal/)

Given a binary tree, return the zigzag level order traversal of its nodes' values. (ie, from left to right, then right to left for the next level and alternate between).

For example:
Given binary tree `[3,9,20,null,null,15,7]`,

```text
    3
   / \
  9  20
    /  \
   15   7
```

return its zigzag level order traversal as:

```text
[
  [3],
  [20,9],
  [15,7]
]
```

## 思路 - 双堆栈

这题的要求是左右往返的遍历树。根节点从左往右遍历，它的字节的从右往左遍历，再往下反方向从左向右。依次往复，直到遍历整棵树。
这里借助了2个`Stack<TreeNode>`, 分别保存从左往右遍历方向的Node节点，和从右往左方向的Node节点。这里是利用了堆栈的FILO的特性，使得压栈方向和输出方向相反。

## 代码 - 双堆栈

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
    public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {

            IList <IList<int>> ans = new List<IList<int>>();
            Stack<TreeNode> lstack = new Stack<TreeNode>();
            Stack<TreeNode> rstack = new Stack<TreeNode>();

            if (root == null) return ans;

            lstack.Push(root);
            bool right2left = false;

            while (lstack.Count >  0 || rstack.Count > 0)
            {
                IList<int> line = new List<int>();

                Stack<TreeNode> dest;
                Stack<TreeNode> src;

                src = right2left ? rstack : lstack;
                dest = right2left ? lstack : rstack;

                while (src.Count > 0)
                {
                    TreeNode node = src.Pop();
                    line.Add(node.val);

                    if(right2left)
                    {
                        if (node.right != null)
                            dest.Push(node.right);
                        if (node.left != null)
                            dest.Push(node.left);
                    }
                    else
                    {
                        if (node.left != null)
                            dest.Push(node.left);
                        if (node.right != null)
                            dest.Push(node.right);
                    }
                }
                ans.Add(line);
                right2left = !right2left;
            }

            return ans;
        }
}
```
