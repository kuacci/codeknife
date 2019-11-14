# [Medium][86. Partition List](https://leetcode.com/problems/partition-list/)

Given a linked list and a value x, partition it such that all nodes less than x come before nodes greater than or equal to x.

You should preserve the original relative order of the nodes in each of the two partitions.

**Example:**

> Input: head = 1->4->3->2->5->2, x = 3
> Output: 1->2->2->4->3->5

## 思路 - 双指针

这道题目的要求是将一个无序的列表，按照给定的某个K值来做分割，小于K的节点在左边，大于等于K的节点在右边。在左右两侧要保持各个节点之间的连接顺序，即不需要做额外的排序。
最直接的方法就是使用2个指针，`NodeList less`指向比K小的节点的起始位置。用`NodeList greater`指向大于等于的节点的起始位置。分别用`ln`和`gn`来连接下一个`>K`和`>=k`的节点。
最后再将less和greater的首尾相接。less的尾部再`ln.next`。greate的头部再`greater.next`, 为了保持greate的尾部指向null,还需要将`gn.next = null`.

## 代码 - 双指针

```csharp
public ListNode Partition(ListNode head, int x)
{

    ListNode less = new ListNode(-1);
    ListNode ln = less;
    ListNode greater = new ListNode(-1);
    ListNode gn = greater;

    while (head != null)
    {
        if (head.val < x)
        {
            ln.next = head;
            ln = ln.next;
        }
        else

        {
            gn.next = head;
            gn = gn.next;
        }

        head = head.next;
    }


    ln.next = greater.next;
    gn.next = null;

    return less.next;

}
```
