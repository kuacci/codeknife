# [Medium][82. Remove Duplicates from Sorted List II](https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/)

Given a sorted linked list, delete all nodes that have duplicate numbers, leaving only distinct numbers from the original list.

**Example 1:**

> Input: 1->2->3->3->4->4->5
> Output: 1->2->5

**Example 2:**

> Input: 1->1->1->2->3
> Output: 2->3

## 思路 - 递归

先走到linked list的最末端。利用线程的stack作为存储，然后比较Parent Node和当前node的val是否相等。如果相等，将用node.val替换掉自己的位置，完成删除的动作。这个动作要持续到将相等val的parent node也删除为止。
由于node.val会被替换掉，parent node的next可能会被跟parent.val不同，导致本来应该删除的parent而没有被删除。所以要用isParentDuplicate，通知到parent，它是否应该被删除。

node是否被传递给parent.next是严格受限的情况。一下
几个情况，当前Node都要被删除 ：

1. node.val == node.next.val
2. parent.val == node.val
3. isParentDuplicate == true

## 代码 - 递归

```csharp
public class Solution {
    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null || head.next == null) return head;
        bool isParentDuplicate = false;
        return DeleteDuplicatesHelper(null, head, ref isParentDuplicate);
    }

    private ListNode DeleteDuplicatesHelper(ListNode parent, ListNode node, ref bool isParentDuplicate)
    {
        ListNode nextNode = null;
        if (node.next != null) nextNode = DeleteDuplicatesHelper(node, node.next, ref isParentDuplicate);
        bool isRemove = isParentDuplicate;
        if (parent != null)
            isParentDuplicate = parent.val == node.val;
        else
            isParentDuplicate = false;

        if (!isRemove && (nextNode == null || nextNode.val != node.val ) && (parent == null || (parent != null && parent.val != node.val)))
            node.next = nextNode;
        else
            node = nextNode;

        return node;
    }
}
```
