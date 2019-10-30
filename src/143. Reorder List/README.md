# [Medium][143. Reorder List](https://leetcode.com/problems/reorder-list/)

Given a singly linked list L: L0→L1→…→Ln-1→Ln,
reorder it to: L0→Ln→L1→Ln-1→L2→Ln-2→…

Example 1:

> Given 1->2->3->4, reorder it to 1->4->2->3.

Example 2:

> Given 1->2->3->4->5, reorder it to 1->5->2->4->3.

## 思路 - 辅助栈

这道题的要求是重建一个新的链表，先左边走一个L0，将他的next指向最后的一个节点Ln.Ln的next则指向L0原来的下一个节点L1.最后成为一个新的Link L0→Ln→L1→Ln-1→L2→Ln-2→…

想法是将L0按顺序推入到Stack中。然后从栈顶一个个的pop出来。将L0的next指向Ln, 而Ln的next则指向L0原来的next (L1).

终止的条件是Li 与 栈顶的 node是同一个，或者Li.next与栈顶的node是同一个。这个时候说明已经完成了。这个时候要将最后一个node的next指向null.

时间复杂度 O(N). 将所有的node放入stack，遍历n，然后将原来的Linked list遍历一般，Stack中的node pop一半，同样是n. O(2n) = O(N)
空间复杂度 O(N). 使用额外的栈存储了链表。

## 代码 - 辅助栈

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
    public void ReorderList(ListNode head)
    {
        if (head == null) return;
        Stack<ListNode> stack = new Stack<ListNode>();

        ListNode node = head;
        ListNode newHead = head;
        while(node != null)
        {
            stack.Push(node);
            node = node.next;
        }

        if (stack.Count <= 2) return;

        node = stack.Pop();
        while(newHead != null && node != null && newHead != node && newHead.next != node)
        {

            node.next = newHead.next;
            newHead.next = node;
            newHead = node.next;
            node = stack.Pop();
        }
        node.next = null;

    }
}
```
