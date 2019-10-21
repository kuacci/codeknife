# [61. Rotate List](https://leetcode.com/problems/rotate-list/)

Given a linked list, rotate the list to the right by k places, where k is non-negative.

**Example 1:**

> Input: 1->2->3->4->5->NULL, k = 2
> Output: 4->5->1->2->3->NULL
> Explanation:
> rotate 1 steps to the right: 5->1->2->3->4->NULL
> rotate 2 steps to the right: 4->5->1->2->3->NULL

**Example 2:**

> Input: 0->1->2->NULL, k = 4
> Output: 2->0->1->NULL
> Explanation:
> rotate 1 steps to the right: 2->0->1->NULL
> rotate 2 steps to the right: 1->2->0->NULL
> rotate 3 steps to the right: 0->1->2->NULL
> rotate 4 steps to the right: 2->0->1->NULL

## 思路 - 环形链表

这个题目要求将链表向右移动k步，越过边界的node移动到左边。为了实现这个方式，首先要将链表头尾相连，组成一个环形链表。同时用一个指针指向链表tail。原来的header就是tail的下一个节点，tail.next.

节点向右移动，意味指着tail是向左移动。因为这个是单向链表，tail向左移动非常耗时，可以将思路转化一下。因为已经是环形链表，tail向左移动k步，相当于向右移动 length - k. 如果k超过length，相当于转了一圈，再往前走k'. 所有通过换算，相当于tail向右移动`k' = length - k % length`.

返回之前还需要向前再走一步，获得header的位置，并且断开tail与header之间的联系，移动就完成了。

## 代码 - 环形链表

```csharp
/**
    * Definition for singly-linked list.
    * public class ListNode {
    *     public int val;
    *     public ListNode next;
    *     public ListNode(int x) { val = x; }
    * }
    */
public ListNode RotateRight(ListNode head, int k)
{
    if (k == 0) return head;
    if (head == null) return null;

    int len = 1;
    ListNode tail = head;

    while (tail.next != null)
    {
        len += 1;
        tail = tail.next;
    }
    tail.next = head;

    k = len - k % len;

    while (k > 0)
    {
        tail = tail.next;
        k--;
    }
    head = tail.next;
    tail.next = null;
    return head;
}
```
