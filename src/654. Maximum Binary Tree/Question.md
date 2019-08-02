# 654. Maximum Binary Tree

Given an integer array with no duplicates. A maximum tree building on this array is defined as follow:

1. The root is the maximum number in the array.
2. The left subtree is the maximum tree constructed from left part subarray divided by the maximum number.
3. The right subtree is the maximum tree constructed from right part subarray divided by the maximum number.
Construct the maximum tree by the given array and output the root node of this tree.

> Example 1:
Input: [3,2,1,6,0,5]
Output: return the tree root node representing the following tree:

``` text
      6
    /   \
   3     5
    \    /
     2  0
       \
        1
```

## 思路

<待整理>

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
