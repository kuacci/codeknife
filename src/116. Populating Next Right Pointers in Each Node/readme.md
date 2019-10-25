# [Medium][116. Populating Next Right Pointers in Each Node](https://leetcode.com/problems/populating-next-right-pointers-in-each-node/)

You are given a perfect binary tree where all leaves are on the same level, and every parent has two children. The binary tree has the following definition:

struct Node {
  int val;
  Node *left;
  Node *right;
  Node *next;
}
Populate each next pointer to point to its next right node. If there is no next right node, the next pointer should be set to NULL.

Initially, all next pointers are set to NULL.

**Example:**

![image](image\116_sample.png)

```text
Input: {"$id":"1","left":{"$id":"2","left":{"$id":"3","left":null,"next":null,"right":null,"val":4},"next":null,"right":{"$id":"4","left":null,"next":null,"right":null,"val":5},"val":2},"next":null,"right":{"$id":"5","left":{"$id":"6","left":null,"next":null,"right":null,"val":6},"next":null,"right":{"$id":"7","left":null,"next":null,"right":null,"val":7},"val":3},"val":1}

Output: {"$id":"1","left":{"$id":"2","left":{"$id":"3","left":null,"next":{"$id":"4","left":null,"next":{"$id":"5","left":null,"next":{"$id":"6","left":null,"next":null,"right":null,"val":7},"right":null,"val":6},"right":null,"val":5},"right":null,"val":4},"next":{"$id":"7","left":{"$ref":"5"},"next":null,"right":{"$ref":"6"},"val":3},"right":{"$ref":"4"},"val":2},"next":null,"right":{"$ref":"7"},"val":1}

Explanation: Given the above perfect binary tree (Figure A), your function should populate each next pointer to point to its next right node, just like in Figure B.
```

**Note:**

* You may only use constant extra space.
* Recursive approach is fine, implicit stack space does not count as extra space for this problem.

## 思路 - 层次遍历

这题的要求是将每层的节点的next field指向它右侧的节点，最右边的节点则指向null. 3个常用的树的遍历方式，前序遍历，中序遍历和后续遍历，在这种情况下不太适用。最好的方式是采用[层次遍历](https://zhuanlan.zhihu.com/p/56895993)。这里要借助`Queue<Node>`。

## 代码 - 层次遍历

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
    public Node Connect(Node root)
    {
        if (root == null) return root;
        Queue<Node> q = new Queue<Node>();
        q.Enqueue(root);

        while(q.Count > 0)
        {
            int count = q.Count;
            Node right = null;

            for (int i = 0; i < count; i++)
            {
                Node node = q.Dequeue();
                node.next = right;
                right = node;
                if (node.right != null)
                    q.Enqueue(node.right);
                if (node.left != null)
                    q.Enqueue(node.left);
            }
        }

        return root;
    }
}
```
