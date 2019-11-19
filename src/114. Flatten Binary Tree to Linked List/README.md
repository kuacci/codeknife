# [Medium][114. Flatten Binary Tree to Linked List](https://leetcode.com/problems/flatten-binary-tree-to-linked-list/)

Given a binary tree, flatten it to a linked list in-place.

For example, given the following tree:

```text
    1
   / \
  2   5
 / \   \
3   4   6
```

The flattened tree should look like:

```text
1
 \
  2
   \
    3
     \
      4
       \
        5
         \
          6
```

## 思路 - 后续遍历

这个题目的要求是将二叉树扁平化。采用的方法是后续遍历。

1. 后续遍历走到树的最左侧。
2. 如果是叶子节点直接返回到上一层。
3. 如果左子树为空，那么不用做什么。
4. 如果左右子树不为空，则要将右子树接到左子树的最右下角。（参考BST的删除的方式）

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
    public void Flatten(TreeNode root) {
        if(root == null) return;
        if(root.left != null) Flatten(root.left);
        if(root.right != null) Flatten(root.right);
        // postorder traversal
        if(root.left == null) return;
        if(root.right != null) FlattenHelper(root.left, root.right);
        root.right = root.left;
        root.left = null;
    }

    private void FlattenHelper(TreeNode left, TreeNode right)
    {
        if(left.right != null)
        {
            FlattenHelper(left.right, right);
        }
        else
        {
            left.right = right;
        }
    }
}
```
