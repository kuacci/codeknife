# [Easy][108. Convert Sorted Array to Binary Search Tree](https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/)

Given an array where elements are sorted in ascending order, convert it to a height balanced BST.

For this problem, a height-balanced binary tree is defined as a binary tree in which the depth of the two subtrees of every node never differ by more than 1.

**Example:**

Given the sorted array: [-10,-3,0,5,9],

One possible answer is: [0,-3,9,-10,null,5], which represents the following height balanced BST:

```text
      0
     / \
   -3   9
   /   /
 -10  5
```

## 思路 - 二分法

这里的要求是将一个有序的`int[]`转换为BST. BST的特点是左子树小于根节点，右侧子树大于根节点。所以将`int[]`转换为BST的时候，先要找到中间的节点，转换成root，将左侧的数组生成左子树，右侧的数组生成右子树。这个过程可以用递归来完成。

## 代码 - 二分法

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
    public TreeNode SortedArrayToBST(int[] nums) {
        return SortedArrayToBST(nums, 0, nums.Length -1);
    }

    private TreeNode SortedArrayToBST(int[] nums, int lo, int hi)
    {
        if(lo > hi) return null;
        if(lo == hi) return new TreeNode(nums[lo]);

        int mid = (lo + hi) / 2;

        TreeNode root = new TreeNode(nums[mid]);
        root.left = SortedArrayToBST(nums, lo, mid - 1);
        root.right = SortedArrayToBST(nums, mid + 1, hi);
        return root;
    }
}
```
