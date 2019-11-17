# [Medium][138. Copy List with Random Pointer](https://leetcode.com/problems/copy-list-with-random-pointer/)

A linked list is given such that each node contains an additional random pointer which could point to any node in the list or null.

Return a deep copy of the list.

**Example 1:**

![img](figure1.png)

```text
Input:
{"$id":"1","next":{"$id":"2","next":null,"random":{"$ref":"2"},"val":2},"random":{"$ref":"2"},"val":1}

Explanation:
Node 1's value is 1, both of its next and random pointer points to Node 2.
Node 2's value is 2, its next pointer points to null and its random pointer points to itself.
```

**Note:**

You must return the copy of the given head as a reference to the cloned list.

## 思路 - 递归

复制节点比较适合使用分值算法的思路。`CopyRandomList(Node head)`这个方法的作用就是赋值当前的节点。 newHead.val可以由head.val决定，`newHead.next`和`newHead.random`则交由`CopyRandomList`继续进行下一步的构造。
`head.next`是单向链表，所以问题很好解决，走到底，当它是null的时候就会退出递归。
比较麻烦的是`head.random`.非常有可能组成一个环形链表。退出的机制为head.random是否已经被创建过。如果创建过，则把它连起来，不需要再进入创建的递归过程。所以需要通过一个Dictionary<Node, Node>来保存已经初始化过的Node。

这里要注意的是，创建好`Node newHead = new Node();`之后要先加入到`memo`中，然后再进行递归调用。如果顺序相反的话，会进入死循环。

如果代码如下, 那么在进入递归的时候，`memo.ContainsKey(head) == false`。已经要返回之后newHead才会加入到memo中。着会导致环形链表的情况下进入死循环。

```csharp
public Node CopyRandomList(Node head) {
    if(memo.ContainsKey(head)) return memo[head];
    Node newHead = new Node();

    // ...
    newHead.random = CopyRandomList(head.random);
    memo.Add(head, newHead);
```

正确解的核心代码：

```csharp
private Dictionary<Node, Node> memo = new Dictionary<Node, Node>();

public Node CopyRandomList(Node head) {
    if(memo.ContainsKey(head)) return memo[head];
    Node newHead = new Node();
    memo.Add(head, newHead);
    // ...
    newHead.random = CopyRandomList(head.random);
}
```

## 代码 - 递归

```csharp
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node next;
    public Node random;

    public Node(){}
    public Node(int _val,Node _next,Node _random) {
        val = _val;
        next = _next;
        random = _random;
}
*/
public class Solution {
    private Dictionary<Node, Node> memo = new Dictionary<Node, Node>();

    public Node CopyRandomList(Node head) {
        if(head == null) return null;
        if(memo.ContainsKey(head)) return memo[head];
        Node newHead = new Node();
        memo.Add(head, newHead);
        newHead.val = head.val;
        newHead.next = CopyRandomList(head.next);
        newHead.random = CopyRandomList(head.random);
        return newHead;
    }
}
```
