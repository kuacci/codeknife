# [Medium][445. Add Two Numbers II](https://leetcode.com/problems/add-two-numbers-ii/)

You are given two non-empty linked lists representing two non-negative integers. The most significant digit comes first and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.

You may assume the two numbers do not contain any leading zero, except the number 0 itself.

**Follow up:**
What if you cannot modify the input lists? In other words, reversing the lists is not allowed.

**Example:**

>**Input:** (7 -> 2 -> 4 -> 3) + (5 -> 6 -> 4)
**Output:** 7 -> 8 -> 0 -> 7

## 思路 - 翻转链表

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

## 思路 - 递归

同样的理由，因为这个加法是需要右对齐，所以用递归的方式，走到最右端。递归的时候涉及到一个问题，如果同时将 l1.next 和 l2.next 传递到下一个调用。在 l1 和 l2 长度不一样的情况下，到达右端的时机是不一致的。那么需要先计算 l1 和 l2的长度。作为递归的一个参数传入。每次递归调用的时候这个长度 -1 , 当两个长度相等的时候，才将l1.next 和 l2.next传递到下一个调用。
进位的处理，递归调用传入参数 ref int cflag. cflag在方法中进行计算。从下一层返回后，参与这一次的计算。如果这次一有进位，则置为1，返回上一层。
在最外层调用，要判断是否有进位的情况。如果有进位，则需要增加一个新的ListNode, val设为1.

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

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        if(l1 == null && l2 == null) return null;
        if(l1 == null) return l2;
        if(l2 == null) return l1;

        int len1 = Count(l1);
        int len2 = Count(l2);

        int cflag = 0;

        // the longer ListNode pass as l1, the shorter ListNode pass as l2.
        // AddWithCarry assume the size of  l1 >= l2
        ListNode head = len1 >= len2 ?
            AddWithCarry(l1, l2, len1, len2, ref cflag) :
            AddWithCarry(l2, l1, len2, len1, ref cflag) ;

        if(cflag == 1)  // if it has a carry flag, add a new header with val = 1
        {
            ListNode newhead = new ListNode(1);
            newhead.next = head;
            return newhead;
        }

        return head;

    }

    private ListNode AddWithCarry(ListNode l1, ListNode l2, int len1, int len2, ref int cflag)
    {
        if(l1 == null || l2 == null) return null;

        ListNode nextNode = null;
        int val = 0;

        if(len1 == len2)
        {
            val = l1.val + l2.val;
            if(l1.next != null)
                nextNode = AddWithCarry(l1.next, l2.next, --len1, --len2, ref cflag);
        }
        else
        {
            val = l1.val;
            nextNode = AddWithCarry(l1.next, l2, --len1, len2, ref cflag);
        }

        val += cflag;

        ListNode newheader = new ListNode(0);
        newheader.val = val % 10;
        cflag = val / 10;

        // if next node is not initiated, then doesn't to link to the tail
        if(nextNode != null)
            newheader.next = nextNode;

        return newheader;
    }

    private int Count(ListNode list)
    {
        int count = 0;
        while(list != null)
        {
            count ++;
            list = list.next;
        }
        return count;
    }
}
```
