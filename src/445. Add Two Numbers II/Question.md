# [LeetCode] 445. Add Two Numbers II

You are given two non-empty linked lists representing two non-negative integers. The most significant digit comes first and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.

You may assume the two numbers do not contain any leading zero, except the number 0 itself.

**Follow up:**
What if you cannot modify the input lists? In other words, reversing the lists is not allowed.

**Example:**

>**Input:** (7 -> 2 -> 4 -> 3) + (5 -> 6 -> 4)
**Output:** 7 -> 8 -> 0 -> 7

## 思路

题目要求ListNode 是 右对齐， 而不是左对齐。个位数在右端，而不是在左端。思路是先将NodeList翻转。开始的位置就是个位。接下来的思路就跟 [2. Add Two Nums](../2.%20Add%20Two%20Nums/Question-no-recrusive.md) 一样了。

## 代码

``` csharp
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
 
public class Solution {
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        if(l1 == null) return l2;
        if(l2 == null) return l1;

        l1 = RevertNode(l1);
        l2 = RevertNode(l2);

        ListNode head = new ListNode(0);
        ListNode tmp = head;
        int cflag = 0;

        while(true)
        {
            int val1 = l1 == null ? 0 : l1.val;
            int val2 = l2 == null ? 0 : l2.val;

            int val = val1 + val2 + cflag;
            tmp.val = val % 10;
            cflag = val / 10;

            l1 = l1 == null ? null : l1.next;
            l2 = l2 == null ? null : l2.next;

            if(l1 == null && l2 == null && cflag == 0) break;

            tmp.next = new ListNode(0);
            tmp = tmp.next;
        }

        head =  RevertNode(head);
        return head;

    }

    private ListNode RevertNode(ListNode l)
    {
        if(l == null || l.next == null) return l;

        ListNode newheader = RevertNode(l.next);

        l.next.next = l;
        l.next = null;

        return newheader;
    }
}
```
