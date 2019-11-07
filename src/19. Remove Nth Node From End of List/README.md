# [Medium][19. Remove Nth Node From End of List](https://leetcode.com/problems/remove-nth-node-from-end-of-list/)

Given a linked list, remove the n-th node from the end of list and return its head.

**Example:**

> Given linked list: 1->2->3->4->5, and n = 2.

After removing the second node from the end, the linked list becomes 1->2->3->5.
Note:

Given n will always be valid.

Follow up:

Could you do this in one pass?

## 思路 - 递归

先用递归的方式走到最后一个节点。然后往回走的同时 `n = n - 1`. 当n = 0 的时候，为了当前要删除的节点，需要return head.next来替换掉当前的节点。

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
public class Solution {
    private int n = 0;
    public ListNode RemoveNthFromEnd(ListNode head, int n) {
        if(head == null) return null;
        this.n = n;
        return RemoveNthFromEnd(head);
    }

    private ListNode RemoveNthFromEnd(ListNode head)
    {
        if(head == null) return null;
        if(head.next != null)
            head.next = RemoveNthFromEnd(head.next);
        this.n--;

        return n == 0 ? head.next : head;
    }
}
```
