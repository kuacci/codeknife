# [Medium][148. Sort List](https://leetcode.com/problems/sort-list/)

Sort a linked list in O(n log n) time using constant space complexity.

**Example 1:**

> Input: 4->2->1->3
> Output: 1->2->3->4

**Example 2:**

> Input: -1->5->3->4->0
> Output: -1->0->3->4->5

## 思路 - 归并排序

题目要求时间复杂度在O(NlogN)，那么可以选用的排序方式有快排和归并排序。归并排序采用的分治算法，采用用递归的方式。
![img](image/figure1.png)

数组的归并需要用个辅助的int[]来帮助完成归并，空间复杂度是O(N). 但是这个ListNode可以采用原地断链接链的方式完成排序,达到O(1). 当然如果将线程栈的使用（递归）也算上的话，次排序的空间复杂度也是O(N).
![img](image/figure2.png)

算法复杂度：O(NlogN)， 归并的时间复杂度为O(NlogN).
空间复杂度：O(N), 递归的时候使用了线程的Stack。

## 代码 - 归并排序

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
    public ListNode SortList(ListNode head)
    {
        if (head == null || head.next == null) return head;

        // use fast && slow pointer to separate the LinkList into 2
        ListNode fast = head.next;
        ListNode slow = head;
        while (fast != null && fast.next != null)
        {
            fast = fast.next.next;
            slow = slow.next;
        }

        ListNode rightNode = slow.next;
        slow.next = null; // break the chain

        ListNode leftNode = SortList(head);
        rightNode = SortList(rightNode);

        ListNode ans = new ListNode(-1);
        ListNode h = ans;

        while (true)
        {
            if (leftNode == null)
            {
                h.next = rightNode;
                break;
            }
            else if (rightNode == null)
            {
                h.next = leftNode;
                break;
            }
            else
            {
                if (leftNode.val < rightNode.val)
                {
                    h.next = leftNode;
                    leftNode = leftNode.next;
                }
                else
                {
                    h.next = rightNode;
                    rightNode = rightNode.next;
                }
                h = h.next;
            }
        }
        return ans.next;
    }
}
```
