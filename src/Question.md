# 701. Insert into a Binary Search Tree

Given the root node of a binary search tree (BST) and a value to be inserted into the tree, insert the value into the BST. Return the root node of the BST after the insertion. It is guaranteed that the new value does not exist in the original BST.

Note that there may exist multiple valid ways for the insertion, as long as the tree remains a BST after insertion. You can return any of them.

For example,

```text
Given the tree:
        4
       / \
      2   7
     / \
    1   3
And the value to insert: 5
```

You can return this binary search tree:

```text
         4
       /   \
      2     7
     / \   /
    1   3 5
```

This tree is also valid:

```text
         5
       /   \
      2     7
     / \
    1   3
         \
          4
```

## 思路 -- 迭代



## 代码

``` csharp
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
    public TreeNode ConstructMaximumBinaryTree(int[] nums) {

        if(nums == null || nums.Length == 0) return null;

        TreeNode head = new TreeNode(nums[0]);

        for(int i = 1; i < nums.Length; i ++)
        {
            head = PlaceTreeNode(head, new TreeNode(nums[i]));
        }

        return head;
    }

    private TreeNode PlaceTreeNode(TreeNode pre, TreeNode next)
    {
        if(pre.val > next.val)
        {
            if(pre.right != null)
            {
                next = PlaceTreeNode(pre.right, next);
            }
            pre.right = next;
        }
        else    // pre.val <= next.val
        {
            if(next.left != null)
            {
                pre = PlaceTreeNode(next.left, pre);
            }
            next.left = pre;
            pre = next;
        }

        return pre;
    }
}
```
