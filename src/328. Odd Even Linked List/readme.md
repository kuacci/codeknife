# [Medium][328. Odd Even Linked List](https://leetcode.com/problems/odd-even-linked-list/)

Given a singly linked list, group all odd nodes together followed by the even nodes. Please note here we are talking about the node number and not the value in the nodes.

You should try to do it in place. The program should run in O(1) space complexity and O(nodes) time complexity.

**Example 1:**

> Input: 1->2->3->4->5->NULL
> Output: 1->3->5->2->4->NULL

**Example 2:**

> Input: 2->1->3->5->6->4->7->NULL
> Output: 2->3->6->7->1->5->4->NULL

**Note:**

> The relative order inside both the even and odd groups should remain as it was in the input.
> The first node is considered odd, the second node even and so on ...

## 思路

思路就是指针的运用。用一个指针指向奇数NodeList,跳过偶数，另外一个则挑选出偶数个。最后将偶数的指针头个链接到奇数链表的尾部。

![img](figure1.jpg)
![img](figure2.jpg)
![img](figure3.jpg)
![img](figure4.jpg)
![img](figure5.jpg)
![img](figure6.jpg)

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
public class Solution
{
    public ListNode OddEvenList(ListNode head)
    {

        if(head == null || head.next == null) return head;
        ListNode oddhead = head;
        ListNode evenhead = oddhead.next;

        while (oddhead != null && oddhead.next != null)
        {
            ListNode tmp = oddhead.next;
            oddhead.next = oddhead.next.next;

            oddhead = oddhead.next;
            tmp.next = oddhead == null ? null : oddhead.next;
        }

        ListNode thead = head;
        while (thead.next != null)
            thead = thead.next;
        thead.next = evenhead;
        return head;
    }
}
```

## 思路 - 优化

在处理ListNode的过程中，用一个辅助指针来记录奇数链表的尾部，可以优化执行效率。

## 代码 - 优化

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
    public ListNode OddEvenList(ListNode head) {
        if (head == null) {
            return null;
        }

        ListNode odd = head;
        ListNode even = odd.next;
        ListNode evenFront = odd.next;
        ListNode oddEnd = odd;

        while (odd != null) {
            odd.next = even?.next;

            if (even != null) {
                even.next = odd.next?.next;
                even = even.next;
            }

            oddEnd = odd;
            odd = odd.next;
        }

        oddEnd.next = evenFront;

        return head;
    }
}
```
