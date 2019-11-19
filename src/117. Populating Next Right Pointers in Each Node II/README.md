# [Medium][117. Populating Next Right Pointers in Each Node II](https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii/)

Given a binary tree

struct Node {
  int val;
  Node *left;
  Node *right;
  Node *next;
}
Populate each next pointer to point to its next right node. If there is no next right node, the next pointer should be set to NULL.

Initially, all next pointers are set to NULL.

Example:

![img](image/117_sample.png)

Note:

**You may only use constant extra space.**
Recursive approach is fine, implicit stack space does not count as extra space for this problem.

## 思路 - 递归

这道题是[116. Populating Next Right Pointers in Each Node](src/116.%20Populating%20Next%20Right%20Pointers%20in%20Each%20Node)的进阶版。之前是使用了BFS的思路借用了`Queue<Node>`, 所有空间复杂度为O(N).
这道题是限定了空间复杂度为O(1), 所有无法使用BFS了。不过题设给的提示，可以使用递归，线程的`Stack`不计算在空间复杂度的要求之内。

一个根节点下的左右两个节点比较好判断，如果左右皆不为空，左边节点的next就是右节点。那么问题是右节点的next如何得来。需要从根节点的兄弟节点上去找。普通的二叉树去兄弟节点上去遍历，而不适用Queue的话非常难做到。但是这题的数据结构中有next这一指针，非常有效的解决了这个问题。

这里的思路是使用类似与先序遍历的方式，区别在于`先根->再右->再左`。这么做的原因，是因为next是指向右侧的兄弟节点，所有要从右往左遍历。这个顺序根BFS的时候是一致的。

1. 先判断根节点下在左右节点是否可以连接，如果`left != null && right != null`， 那么直接用`left.next = right`.
2. 确定还没有连的node是`left`还是`right`. 为下一个连接做准备。
3. 从root.next中找第一个不为空的子节点。
   `while(head != null && (head.left == null && head.right == null)) head = head.next;`
4. 将2#上确定的node跟3#中找到的node相连接。BTW，2#找到的有可能是最右侧的节点，这个是3#返回的可能是null.
5. 遍历root.right.
6. 遍历root.left.

## 代码 - 递归

```csharp
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node left;
    public Node right;
    public Node next;

    public Node(){}
    public Node(int _val,Node _left,Node _right,Node _next) {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
}
*/
public class Solution {
    public Node Connect(Node root) {
        if(root == null || (root.left == null && root.right == null)) return root;

        if(root.left != null && root.right != null) root.left.next = root.right;
        Node cur = root.right == null ? root.left : root.right;
        Node head = root.next;
        while(head != null && (head.left == null && head.right == null)) head = head.next;

        cur.next = head == null ? null : head.left != null ? head.left : head.right;
        Connect(root.right);
        Connect(root.left);
        return root;
    }
}
```