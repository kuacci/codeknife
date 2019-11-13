# [Medium][82. Remove Duplicates from Sorted List II](https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/)

Given a sorted linked list, delete all nodes that have duplicate numbers, leaving only distinct numbers from the original list.

**Example 1:**

> Input: 1->2->3->3->4->4->5
> Output: 1->2->5

**Example 2:**

> Input: 1->1->1->2->3
> Output: 2->3

## 思路 - 递归

先走到linked list的最末端。利用线程的stack作为存储，然后比较Parent Node和当前node的val是否相等。如果相等，将用node.val替换掉自己的位置，完成删除的动作。这个动作要持续到将相等val的parent node也删除为止。
由于node.val会被替换掉，parent node的next可能会被跟parent.val不同，导致本来应该删除的parent而没有被删除。所以要用isParentDuplicate，通知到parent，它是否应该被删除。

node是否被传递给parent.next是严格受限的情况。一下
几个情况，当前Node都要被删除 ：

1. node.val == node.next.val
2. parent.val == node.val
3. isParentDuplicate == true

## 代码 - 递归

```csharp
public class Solution {
    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null || head.next == null) return head;
        bool isParentDuplicate = false;
        return DeleteDuplicatesHelper(null, head, ref isParentDuplicate);
    }

    private ListNode DeleteDuplicatesHelper(ListNode parent, ListNode node, ref bool isParentDuplicate)
    {
        ListNode nextNode = null;
        if (node.next != null) nextNode = DeleteDuplicatesHelper(node, node.next, ref isParentDuplicate);
        bool isRemove = isParentDuplicate;
        if (parent != null)
            isParentDuplicate = parent.val == node.val;
        else
            isParentDuplicate = false;

        if (!isRemove && (nextNode == null || nextNode.val != node.val ) && (parent == null || (parent != null && parent.val != node.val)))
            node.next = nextNode;
        else
            node = nextNode;

        return node;
    }
}
```

## 思路 - 虚拟头指针 + 双指针

这里的思路是利用双指针来确认要删除的链表的范围，用一个`pre`指针来标记之前一个节点。由于可能一开始就删除了第一个节点，使得后面无法运算，需要使用一个虚拟头指针来指向head，并且引导后面的计算。最后返回的是start.next.

双指针的思路是：

1. cur记录左边的一个节点
2. nxt记录右边的一个节点，即cur.next
3. 如果nxt.val == cur.val，说明要删除至少2个节点。nxt继续向右走，一直走到val不等的时候。
4. 用pre.val = nxt的方式，截断之前的相同val的节点。
5. 用cur.next = pre, nxt = cur?.next. 的方式进行下一轮计算。
6. 知道nxt走到尾。

## 代码 - 虚拟头指针 + 双指针

```csharp
public ListNode DeleteDuplicates(ListNode head)
{
    if (head == null || head.next == null) return head;

    ListNode start = new ListNode(-1);
    start.next = head;

    ListNode pre = start;
    ListNode cur = head;
    ListNode nxt = head.next;

    while (nxt != null)
    {
        if(cur.val == nxt.val)
        {
            while (nxt != null && cur.val == nxt.val) nxt = nxt.next;
            pre.next = nxt;
            cur = pre.next;
        }
        else
        {
            pre = pre.next;
            cur = pre.next;
        }
        nxt = cur == null ? null : cur.next;

    }

    return start.next;
}
```