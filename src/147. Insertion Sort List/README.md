# [Medium][147. Insertion Sort List](https://leetcode.com/problems/insertion-sort-list/)

Sort a linked list using insertion sort.

![img](image/Insertion-sort-example-300px.gif)

A graphical example of insertion sort. The partial sorted list (black) initially contains only the first element in the list.
With each iteration one element (red) is removed from the input data and inserted in-place into the sorted list

Algorithm of Insertion Sort:

Insertion sort iterates, consuming one input element each repetition, and growing a sorted output list.
At each iteration, insertion sort removes one element from the input data, finds the location it belongs within the sorted list, and inserts it there.
It repeats until no input elements remain.

**Example 1:**

> Input: 4->2->1->3
> Output: 1->2->3->4

**Example 2:**

> Input: -1->5->3->4->0
> Output: -1->0->3->4->5

## 思路 - 1

数组的插入排序, 如下：
![img](image/figure2.gif)

```csharp
for(int i = 1; i < nums.Length; i++)
{
    for(int j = i; j > 0 && nums[j] < nums[j - 1]; j-->)
    {
        Swap(nums, j, j - 1);
    }
}
```

单向项链表在做这个操作的时候，问题会出现在第二层循环，以为单向链表不能回退。为了支持回退，可以使用`List<TreeNode>`. 将遍历过的TreeNode放到`List<TreeNode>`, 再对其进行比较并排序。

时间复杂度: O(N^2)
空间复杂度：O(N)

## 代码 - 1

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
    public ListNode InsertionSortList(ListNode head) {
        if(head == null || head.next == null) return head;
        List<ListNode> list = new List<ListNode>();
        list.Add(head);
        ListNode tail = head.next;
        while(tail != null)
        {
            int pre = list.Count;
            while(pre > 0 && tail.val < list[pre - 1].val)
            {
                pre--;
            }
            if(pre == list.Count)
            {
                list.Add(tail);
            }
            else
            {
                list.Insert(pre, tail);
            }
            tail = tail.next;
        }
        for(int i = 0; i < list.Count - 1; i ++)
        {
            list[i].next = list[i + 1];
        }
        list[list.Count - 1].next = null;

        return list[0];
    }
}
```
