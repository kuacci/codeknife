# [Medium][24. Swap Nodes in Pairs](https://leetcode.com/problems/swap-nodes-in-pairs/)

Given a linked list, swap every two adjacent nodes and return its head.

You may not modify the values in the list's nodes, only nodes itself may be changed.

**Example:**

> Given 1->2->3->4, you should return the list as 2->1->4->3.

## 思路 - 递归

这道题目的要求是将节点成对的翻转。第二个节点返到前面，而前面的反到后面。 对于第二个节点，提到前面，将他的next节点指向第一个节点。而第一个节点的`next`则通过递过获得。

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
    public ListNode SwapPairs(ListNode head) {
        if(head == null || head.next == null) return head;
        ListNode n = head.next;
        ListNode t = n.next;
        n.next = head;
        head.next = SwapPairs(t);
        return n;

    }
}
```
