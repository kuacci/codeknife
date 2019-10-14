# 230. [Kth Smallest Element in a BST](https://leetcode.com/problems/kth-smallest-element-in-a-bst/)

Given a binary search tree, write a function kthSmallest to find the kth smallest element in it.

**Note:**
You may assume k is always valid, 1 ≤ k ≤ BST's total elements.

**Example 1:**

```text
Input: root = [3,1,4,null,2], k = 1
   3
  / \
 1   4
  \
   2
Output: 1
```

**Example 2:**

```text
Input: root = [5,3,6,2,4,null,null,1], k = 3
       5
      / \
     3   6
    / \
   2   4
  /
 1
Output: 3
```

Follow up:
What if the BST is modified (insert/delete operations) often and you need to find the kth smallest frequently? How would you optimize the kthSmallest routine?

## 思路 - 中序遍历

BST的特性是左子树小于根节点，根节点小于右子树。所以为了找寻最小的节点，需要用中序遍历。但是题目是要求找到第k个。所以在做中序遍历的时候，访问Node之后k-1. 如果k == 0 代表着已经找到第k个小的节点了。返回这个节点的值。
时间复杂度为O(n)，空间.复杂度为O(1)

## 代码 - 中序遍历

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
    private int k = 0;
    public int KthSmallest(TreeNode root, int k) {

        if(root == null) return int.MinValue;
        this.k = k;

        return helper(root);
    }

    private int helper(TreeNode node)
    {
        int ans = int.MinValue;
        if(node.left != null)
            ans = helper(node.left);
        if(ans > int.MinValue) return ans;
        this.k -= 1;
        if(k == 0) return node.val;
        if(node.right != null)
        ans = helper(node.right);
        return ans;
    }
}
```

## 思路 - 辅助数组

上述的逻辑过于复杂，代码也不够清晰。为了简化代码量，可以使用一个辅助数组。按照中序遍历，将值插入数组中，这是一个有序切递增的数组。找到第k个最小数，那么找到数组的第K - 1个位置就可以.
在减少了代码量的同时，也增加了空间复杂度。时间复杂度为O(n)，空间.复杂度为O(n)

## 代码 - 辅助数组

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
    private int k = 0;
    public int KthSmallest(TreeNode root, int k) {

        List<int> ans = new List<int>();

        helper(root, ans);
        return ans[k - 1];
    }

    private void helper(TreeNode node, List<int> ans)
    {
        if(node == null) return;

        helper(node.left, ans);
        ans.Add(node.val);
        helper(node.right, ans);
    }
}
```
