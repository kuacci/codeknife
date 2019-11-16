# [Easy][206. Reverse Linked List](https://leetcode.com/problems/reverse-linked-list/)

Reverse a singly linked list.

Example:

Input: 1->2->3->4->5->NULL
Output: 5->4->3->2->1->NULL
Follow up:

A linked list can be reversed either iteratively or recursively. Could you implement both?

## 代码 - 迭代

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
    public ListNode ReverseList(ListNode head) {
        if(head == null) return null;
        ListNode pre = null;
        while(true)
        {
            ListNode tmp = head.next;
            head.next = pre;
            pre = head;
            if(tmp == null) break;
            head = tmp;
        }
        return head;
    }
}
```

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
    public ListNode ReverseList(ListNode head) {
        if(head == null || head.next == null) return head;
        ListNode newHead = ReverseList(head.next);

        head.next.next = head;
        head.next = null;
        return newHead;
    }
}
```
