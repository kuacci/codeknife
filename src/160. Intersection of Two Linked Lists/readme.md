# 160. Intersection of Two Linked Lists

Write a program to find the node at which the intersection of two singly linked lists begins.

For example, the following two linked lists:

![image](image/160_statement.png)

begin to intersect at node c1.

**Example 1:**

![image](image/160_statement_1.png)

```text
Input: intersectVal = 8, listA = [4,1,8,4,5], listB = [5,0,1,8,4,5], skipA = 2, skipB = 3
Output: Reference of the node with value = 8
Input Explanation: The intersected node's value is 8 (note that this must not be 0 if the two lists intersect). From the head of A, it reads as [4,1,8,4,5]. From the head of B, it reads as [5,0,1,8,4,5]. There are 2 nodes before the intersected node in A; There are 3 nodes before the intersected node in B.
```

**Example 2:**

![image](image/160_statement_2.png)

```text
Input: intersectVal = 2, listA = [0,9,1,2,4], listB = [3,2,4], skipA = 3, skipB = 1
Output: Reference of the node with value = 2
Input Explanation: The intersected node's value is 2 (note that this must not be 0 if the two lists intersect). From the head of A, it reads as [0,9,1,2,4]. From the head of B, it reads as [3,2,4]. There are 3 nodes before the intersected node in A; There are 1 node before the intersected node in B.
```

**Example 3:**

![image](image/160_statement_3.png)

```text
Input: intersectVal = 0, listA = [2,6,4], listB = [1,5], skipA = 3, skipB = 2
Output: null
Input Explanation: From the head of A, it reads as [2,6,4]. From the head of B, it reads as [1,5]. Since the two lists do not intersect, intersectVal must be 0, while skipA and skipB can be arbitrary values.
Explanation: The two lists do not intersect, so return null.
```

**Notes :**

* If the two linked lists have no intersection at all, return null.
* The linked lists must retain their original structure after the function returns.
* You may assume there are no cycles anywhere in the entire linked structure.
* Your code should preferably run in O(n) time and use only O(1) memory.

## 思路 - Stack

将两个ListNode分别push到两个Stack中。然后比较Stack顶部的ListNode并且弹出，直到2个不相等，那么就找到他们的交汇点。
时间复杂度 O(n+m) , 空间复杂度 O(n+m).

## 代码 - Stack

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
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {

        if(headA == null || headB == null) return null;

        Stack<ListNode> stackA = new Stack<ListNode>();
        Stack<ListNode> stackB = new Stack<ListNode>();

        ListNode tA = headA;
        ListNode tB = headB;

        while(tA != null)
        {
            stackA.Push(tA);
            tA = tA.next;
        }

        while(tB != null)
        {
            stackB.Push(tB);
            tB = tB.next;
        }

        if(stackA.Peek() != stackB.Peek()) return null;

        while(stackA.Count > 0 && stackB.Count >0)
        {
            if(stackA.Peek() != stackB.Peek()) break;

            tA = stackA.Pop();
            tB = stackB.Pop();
        }

        return tA;

    }
}
```

## 思路 - 双指针-模拟环形链表

采用双指针，分别用crtA和crtB指向headA和headB，指针同时向后走，如果走到链表的尾部，则走向另外一个ListNode的header.从而模拟一个环形链表，但是又不会改变ListNode的结构。如果两个ListNode相交与一点，那么这两个指针迟早要在相交的相遇。
不过这里有个bug，如果两个ListNode没有交汇点，那么会陷入死循环。
时间复杂度O(n+m), 空间复杂度O(1).

## 代码 - 双指针-模拟环形链表

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
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {
        if (headA == null || headB == null)
            return null;

        ListNode crtA = headA;
        ListNode crtB = headB;

        while (crtA != crtB) {
            crtA = (crtA == null) ? headB : crtA.next;
            crtB = (crtB == null) ? headA : crtB.next;
        }

        return crtA;
    }
}
```
