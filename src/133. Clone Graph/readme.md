# [Medium][133. Clone Graph](https://leetcode.com/problems/clone-graph/)

Given a reference of a node in a connected undirected graph, return a deep copy (clone) of the graph. Each node in the graph contains a val (int) and a list (List[Node]) of its neighbors.

![image](image/113_sample.png)

Input:

```text
{"$id":"1","neighbors":[{"$id":"2","neighbors":[{"$ref":"1"},{"$id":"3","neighbors":[{"$ref":"2"},{"$id":"4","neighbors":[{"$ref":"3"},{"$ref":"1"}],"val":4}],"val":3}],"val":2},{"$ref":"4"}],"val":1}

Explanation:
Node 1's value is 1, and it has two neighbors: Node 2 and 4.
Node 2's value is 2, and it has two neighbors: Node 1 and 3.
Node 3's value is 3, and it has two neighbors: Node 2 and 4.
Node 4's value is 4, and it has two neighbors: Node 1 and 3.
```

Note:

> The number of nodes will be between 1 and 100.
> The undirected graph is a simple graph, which means no repeated edges and no self-loops in the graph.
> Since the graph is undirected, if node p has node q as neighbor, then node q must have node p as neighbor too.
> You must return the copy of the given node as a reference to the cloned graph.

## 思路 - BFS

这道题就是遍历整个图,所以遍历时候要记录已经访问点, 我们用一个字典记录已经访问过的节点。如果已经访问过，就把这个节点连接起来作为neighbors. 用Queue来记录下一组要访问的neighbors.

## 代码 - BFS

```csharp
/*
// Definition for a Node.
public class Node {
    public int val;
    public IList<Node> neighbors;

    public Node(){}
    public Node(int _val,IList<Node> _neighbors) {
        val = _val;
        neighbors = _neighbors;
}
*/
public class Solution {
    public Node CloneGraph(Node node)
    {
        if (node == null) return null;

        Node header = new Node(node.val, new List<Node>());

        Queue<Node> src = new Queue<Node>();
        src.Enqueue(node);
        Queue<Node> dest = new Queue<Node>();
        dest.Enqueue(header);
        Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
        visited.Add(node, header);

        while(src.Count > 0)
        {
            Node srcNode = src.Dequeue();
            Node destNode = dest.Dequeue();
            foreach (var item in srcNode.neighbors)
            {
                if(visited.ContainsKey(item))
                {
                    destNode.neighbors.Add(visited[item]);
                }
                else
                {
                    Node neig = new Node(item.val, new List<Node>());
                    destNode.neighbors.Add(neig);
                    src.Enqueue(item);
                    dest.Enqueue(neig);
                    visited.Add(item, neig);
                }
            }
        }

        return header;
    }
}
```

## 思路 - DFS

用递归的方式走到最远的node，直到某个node被访问过，或者为空，说明已经到底，然后从递归退出即可进入下一轮。

## 代码 - DFS

```csharp
/*
// Definition for a Node.
public class Node {
    public int val;
    public IList<Node> neighbors;

    public Node(){}
    public Node(int _val,IList<Node> _neighbors) {
        val = _val;
        neighbors = _neighbors;
}
*/
public class Solution {
    public Node CloneGraph(Node node)
    {
        Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
        return CloneGraphDFS(visited, node);
    }

    private Node CloneGraphDFS(Dictionary<Node, Node> visited, Node srcNode)
    {
        if (srcNode == null) return null;
        if (visited.ContainsKey(srcNode)) return visited[srcNode];

        Node destNode = new Node(srcNode.val, new List<Node>());
        visited.Add(srcNode, destNode);
        foreach (var item in srcNode.neighbors)
        {
            destNode.neighbors.Add(CloneGraphDFS(visited, item));
        }

        return destNode;
    }
}
```
