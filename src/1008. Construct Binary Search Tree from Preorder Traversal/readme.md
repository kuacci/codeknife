# [1008. Construct Binary Search Tree from Preorder Traversal](https://leetcode.com/problems/construct-binary-search-tree-from-preorder-traversal/)

Return the root node of a binary search tree that matches the given preorder traversal.

(Recall that a binary search tree is a binary tree where for every node, any descendant of node.left has a value < node.val, and any descendant of node.right has a value > node.val.  Also recall that a preorder traversal displays the value of the node first, then traverses node.left, then traverses node.right.)

Example 1:

```text
Input: [8,5,1,7,10,12]
Output: [8,5,10,1,7,null,12]
```

![example](image/example.png)

Note:

```text
1. 1 <= preorder.length <= 100
2. The values of preorder are distinct.
```

## 思路

这道题的要求是输入一棵树的先序遍历的结果，要求重新build这棵树。

![dataflow](image/dataflow.png)

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
    public TreeNode BstFromPreorder(int[] preorder) {

        if(preorder?.Length == 0) return null;

        TreeNode root = null;

        for(int i = 0; i < preorder.Length; i++)
            root = BuildTreeNode(root, preorder[i]);
        return root;
    }

    private TreeNode BuildTreeNode(TreeNode root, int val)
    {
        if(root == null) return new TreeNode(val);

        if(val < root.val)
        {
            root.left = BuildTreeNode(root.left, val);
        }
        else
        {
            root.right = BuildTreeNode(root.right, val);
        }
        return root;
    }
}
```
