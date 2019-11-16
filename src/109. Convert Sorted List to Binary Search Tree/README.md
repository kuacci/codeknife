# [Easy][109. Convert Sorted List to Binary Search Tree](https://leetcode.com/problems/convert-sorted-list-to-binary-search-tree/)

Given a singly linked list where elements are sorted in ascending order, convert it to a height balanced BST.

For this problem, a height-balanced binary tree is defined as a binary tree in which the depth of the two subtrees of every node never differ by more than 1.

Example:

Given the sorted linked list: [-10,-3,0,5,9],

One possible answer is: [0,-3,9,-10,null,5], which represents the following height balanced BST:

```text
      0
     / \
   -3   9
   /   /
 -10  5
```

## 思路 - 递归

这里的要求是将一个有序的单向链表转换成BST。这题是[108. Convert Sorted Array to Binary Search Tree](src/108.%20Convert%20Sorted%20Array%20to%20Binary%20Search%20Tree)的进阶版。
思路也是参考这题，做二分法。但是单线链表不同于数组，不能快速的知道`lo`,`mid`和`hi`的位置。需要分别计算。

1. **mid : **使用快慢指针的方式，从head开始往右走，当fast走到底的时候，low所在的位置就是mid所在的位置。
2. **lo :** 左侧的lo位置是head, 这点跟跟数组一样。右侧的为`lo = mid.next`. 相当于`lo = mid + 1`.
3. **hi :** 左侧的hi位置，起始应该是mid的上一个节点。为了能找到这个节点，需要有个pre来帮助记录。并且，要在传入到下一次递归的之后，断开与mid的连接。右侧的hi位置则比较简单。

时间复杂度：O(NlogN). 思路是二分法的思路，所以寻找中点的过程的时间复杂度是(logN). 对数组每个点都有一次找中间节点的过程所以是O(N * logN)
空间复杂度：O(logN).因为使用递归的方法，所以需要考虑递归栈的空间复杂度。对于一棵费平衡二叉树，可能需要O(N) 的空间，但是问题描述中要求维护一棵平衡二叉树，所以保证树的高度上界为 O(logN)，因此空间复杂度为 O(logN)。

## 代码 - 递归

 ```csharp
 /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
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
    public TreeNode SortedListToBST(ListNode head) {

        if(head == null) return null;
        if(head.next == null) return new TreeNode(head.val);

        ListNode slow = head;
        ListNode fast = head;
        ListNode pre = new ListNode(-1);
        pre.next = head;

        while(fast != null && fast.next != null)
        {
            pre = pre.next;
            slow = slow.next;
            fast = fast.next.next;
        }
        pre.next = null;

        TreeNode root = new TreeNode(slow.val);
        root.left = SortedListToBST(head);
        root.right = SortedListToBST(slow.next);

        return root;
    }
}
```

## 思路 - 数组 + 二分

上面的方法有改进的空间。单项列表为了确认Mid的位置，需要重复的遍历各个节点。
为了解决这个问题，可以先遍历单向链表，并且将结果保持在`int[]`中，然后再用二分法进行计算。

## 代码 - 数组 + 二分

```csharp
public class Solution {

    public TreeNode SortedListToBST(ListNode head) {
        List<int> nums = new List<int>();
        SortListToArray(nums, head);
        return ArrayToBST(nums, 0, nums.Count - 1);
    }

    private void SortListToArray(List<int> nums, ListNode head)
    {
        if(head == null) return;
        nums.Add(head.val);
        SortListToArray(nums, head.next);
    }

    private TreeNode ArrayToBST(List<int> nums, int lo, int hi)
    {
        if(lo > hi) return null;
        if(lo == hi) return new TreeNode(nums[lo]);

        int mid = (lo + hi) / 2;
        TreeNode root = new TreeNode(nums[mid]);
        root.left = ArrayToBST(nums, lo, mid - 1);
        root.right = ArrayToBST(nums, mid + 1, hi);
        return root;
    }
}
```
