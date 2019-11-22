# [Medium][142. Linked List Cycle II](https://leetcode.com/problems/linked-list-cycle-ii/)

Given a linked list, return the node where the cycle begins. If there is no cycle, return null.

To represent a cycle in the given linked list, we use an integer pos which represents the position (0-indexed) in the linked list where tail connects to. If pos is -1, then there is no cycle in the linked list.

Note: Do not modify the linked list.

Example 1:

> Input: head = [3,2,0,-4], pos = 1
> Output: tail connects to node index 1
> Explanation: There is a cycle in the linked list, where tail connects to the second node.

![img](circularlinkedlist.png)

Example 2:

> Input: head = [1,2], pos = 0
> Output: tail connects to node index 0
> Explanation: There is a cycle in the linked list, where tail connects to the first node.

![img](image/circularlinkedlist_test2.png)

Example 3:

> Input: head = [1], pos = -1
> Output: no cycle
> Explanation: There is no cycle in the linked list.

![img](image/circularlinkedlist_test3.png)

## 思路- Floyd

[Floyd 的算法](https://leetcode-cn.com/problems/linked-list-cycle-ii/solution/huan-xing-lian-biao-ii-by-leetcode/)被划分成两个不同的 阶段 。在第一阶段，找出列表中是否有环，如果没有环，可以直接返回 null 并退出。否则，用 相遇节点 来找到环的入口。

## 代码 - Floyd

```csharp
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution {
    public ListNode DetectCycle(ListNode head) {
        if(head == null) return null;

        ListNode ans = null;
        ListNode fast = head;
        ListNode slow = head;

        while(fast != null && fast.next != null)
        {
            fast = fast?.next?.next;
            slow = slow?.next;
            if(fast == slow)
            {
                ans = head;
                break;
            }
        }

        if(ans == null) return null;

        while(ans != slow)
        {
            ans = ans.next;
            slow = slow.next;
        }
        return ans;

    }
}
```
