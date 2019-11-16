# [Medium][92. Reverse Linked List II](https://leetcode.com/problems/reverse-linked-list-ii/)

Reverse a linked list from position m to n. Do it in one-pass.

**Note:** 1 ≤ m ≤ n ≤ length of list.

**Example:**

> Input: 1->2->3->4->5->NULL, m = 2, n = 4
> Output: 1->4->3->2->5->NULL

## 思路 - 翻转指针 - 迭代

这题是[206. Reverse Linked List](src/206.%20Reverse%20Linked%20List)的进阶版。
翻转的核心代码是：

```csharp
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
```

有了这个理论基础之后就是找m和n位置的对应节点。先使用一个哑节点作为头节点，指向head的位置。用两个指针`left`和`right`分别指向m和n位置的节点。

翻转之后，left转到右侧，他的next应该要接到原先的right.next. 所以将这个位置的节点先传递给left.next. left的前驱节点要断开与left的链接，转向right的节点。所以还需要使用一个指针累记录left的前驱。

## 代码 - 翻转指针 - 迭代

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
    public ListNode ReverseBetween(ListNode head, int m, int n)
    {
        ListNode node = new ListNode(-1);
        node.next = head;
        ListNode pre = node;
        ListNode left = null;
        ListNode right = null;

        int pos = 0;

        while (pos < m - 1)
        {
            pre = pre.next;
            pos++;
        }
        left = pre.next;
        pos += 1;
        right = left;
        while (pos < n)
        {
            right = right.next;
            pos++;
        }

        ListNode newpre = right.next;
        right.next = null;

        while(true)
        {
            ListNode tmp = left.next;
            left.next = newpre;
            newpre = left;
            if (tmp == null) break;
            left = tmp;
        }

        pre.next = left;

        return node.next;
    }

}
```
