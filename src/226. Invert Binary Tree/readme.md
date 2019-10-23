# [Easy][226. Invert Binary Tree](https://leetcode.com/problems/invert-binary-tree/)

Invert a binary tree.

Example:

```text
Input:

     4
   /   \
  2     7
 / \   / \
1   3 6   9
Output:

     4
   /   \
  7     2
 / \   / \
9   6 3   1
Trivia:
This problem was inspired by this original tweet by Max Howell:
```

Google: 90% of our engineers use the software you wrote (Homebrew), but you can’t invert a binary tree on a whiteboard so f*** off.

## 思路 - 后序遍历

先用先走到最左侧，然后右侧，最后回到父节点进行反转，以此类推，直到回到根节点。这是典型的后序遍历。

## 代码 - 后序遍历

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
    public TreeNode InvertTree(TreeNode root) {
        if(root == null) return root;

        if(root.left != null)
            InvertTree(root.left);
        if(root.right != null)
            InvertTree(root.right);

        TreeNode tmp = root.left;
        root.left = root.right;
        root.right = tmp;
        return root;
    }
}
```
