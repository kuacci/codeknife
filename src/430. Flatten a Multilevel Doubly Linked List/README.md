# [Medium][430. Flatten a Multilevel Doubly Linked List](https://leetcode.com/problems/flatten-a-multilevel-doubly-linked-list/)

You are given a doubly linked list which in addition to the next and previous pointers, it could have a child pointer, which may or may not point to a separate doubly linked list. These child lists may have one or more children of their own, and so on, to produce a multilevel data structure, as shown in the example below.

Flatten the list so that all the nodes appear in a single-level, doubly linked list. You are given the head of the first level of the list.

**Example:**

```text
Input:
 1---2---3---4---5---6--NULL
         |
         7---8---9---10--NULL
             |
             11--12--NULL

Output:
1-2-3-7-8-11-12-9-10-4-5-6-NULL
```

Explanation for the above example:

Given the following multilevel doubly linked list:
![img](image/multilevellinkedlistflattened.png)

We should return the following flattened doubly linked list:
![img](image/multilevellinkedlist.png)

## 思路 - 辅助Stack

这题跟[114. Flatten Binary Tree to Linked List](src/114.%20Flatten%20Binary%20Tree%20to%20Linked%20List)很类似。
如果一个节点上面有child的节点，那么需要将child替换到next上面。而之前的next则要接到child的后面。
考虑到child的link上面可能还有child, 所以不能用一个临时变量记录曾经出现过的next。需要考虑使用某个数据类型Stack或者Queue. 查看例子，可以发现最先出现的next实际上是要接在最后一个child的最后，是FILO的情况，所以要使用Stack.

1. 用一个current指向指向head, next指向下一个。
2. 如果current有child则优先处理child
   1. 有child的情况下，要暂存next(如果有next)
3. 如果没有child，则正常的处理next。
   1. 如果next == null, 说明当前的链已经走到底了。要检查Stack里面是否还有之前压栈的Node，即进入next之前的遇到的child分支。如果有就弹出来。
   2. 如果next == null && Stack.Count == 0, 说明当前链已经走到底，而且已经没有分支，所以终止循环。
4. 按照逻辑更新current next, pre 的关系。

时间复杂度：O(N), 所有的节点要走一遍。
空间复杂度：O(N), Stack用来存储分支的next的引用。

## 代码 - 辅助Stack

```csharp
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node prev;
    public Node next;
    public Node child;

    public Node(){}
    public Node(int _val,Node _prev,Node _next,Node _child) {
        val = _val;
        prev = _prev;
        next = _next;
        child = _child;
}
*/
public class Solution {
    public Node Flatten(Node head) {
        if(head == null) return null;
        Stack<Node> stack = new Stack<Node>();

        Node current = head;
        Node next = head.next;

        while(true)
        {
            if(current.child != null)
            {
                if(next != null)
                    stack.Push(next);
                next = current.child;
                current.child = null;
            }
            else if(next == null && stack.Count > 0)
            {
                next = stack.Pop();
            }
            else if(next == null && stack.Count == 0) break;


            Node pre = current;
            current.next = next;
            current = current.next;
            current.prev = pre;
            next = current.next;
        }
        return head;
    }
}
```
