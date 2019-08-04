# [LeetCode] 2. Add two numbers

You are given two **non-empty** linked lists representing two non-negative integers. The digits are stored in **reverse order** and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.

You may assume the two numbers do not contain any leading zero, except the number 0 itself.

**Example:**

>**Input:** (2 -> 4 -> 3) + (5 -> 6 -> 4)  
**Output:** 7 -> 0 -> 8  
**Explanation:** 342 + 465 = 807  

## 思路 - 递归

递归实现时，只需要考虑如下四种情形：

1. Left 与 Right 都为 Null，即 base condition 最终退出递归的条件。
2. Left 与 Right 都为 Null 但 Carry 不为 0，即 base condition 最终退出递归的条件。
3. Left 为 Null，但 Right 不为 Null
4. Right 为 Null，但 Left 不为 Null

## 代码 - 递归

```csharp
public class Solution
{
    public ListNode AddTwoNumbers(ListNode left, ListNode right)
    {
        return CalculateRecursively(left, right, 0);
    }

    private ListNode CalculateRecursively(ListNode left, ListNode right, int carray)
    {

        if (left == null && right == null)
        {
            return carray == 0 ? null : new ListNode(carray);
        }

        int total = 0;

        if (left != null)
        {
            total += left.Value;
        }

        if (right != null)
        {
            total += right.Value;
        }

        total += carray;

        var val = total % 10;

        return new ListNode(val)
        {
            Next = CalculateRecursively(left?.Next, right?.Next, total / 10)
        };
    }
}
```

## 思路

非递归实现时，l1 和 l2 同时从左往右开始相加。
如果 l1 或者 l2 一条链路上以及为空，则只需要加另外一条链路。
当前node 的val为 l1.val + l2.val + cflag。 即上一次求和的进位。
如果 l1 和 l2 之和大于 10 ， 则需要进位， 加到cflag上。
停止条件是l1, l2 都走到头，并且没有进位。如果有进位，则需要多进行一轮循环。

## 代码

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

        if(l1 == null) return l2;
        if(l2 == null) return l1;

        ListNode head = new ListNode(0);
        ListNode tmp = head;
        int cflag = 0;

        while(true)
        {
            int n1 = l1 == null ? 0 : l1.val;
            int n2 = l2 == null ? 0 : l2.val;
            n1 = n1 + n2 + cflag;
            cflag = n1 / 10;
            tmp.val = n1 % 10;

            l1 = l1 == null ? null : l1.next;
            l2 = l2 == null ? null : l2.next;

            if(l1 == null && l2 == null && cflag == 0) break;

            tmp.next = new ListNode(0);
            tmp = tmp.next;
        }

        return head;

    }
}
```
