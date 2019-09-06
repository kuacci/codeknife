# 797. All Paths From Source to Target

Given a directed, acyclic graph of N nodes.  Find all possible paths from node 0 to node N-1, and return them in any order.

The graph is given as follows:  the nodes are 0, 1, ..., graph.length - 1.  graph[i] is a list of all nodes j for which the edge (i, j) exists.

**Example:**

```text
Input: [[1,2], [3], [3], []]
Output: [[0,1,3],[0,2,3]]
Explanation: The graph looks like this:
0--->1
|    |
v    v
2--->3
There are two paths: 0 -> 1 -> 3 and 0 -> 2 -> 3.
```

**Note:**

* The number of nodes in the graph will be in the range [2, 15].
* You can print different paths in any order, but you should keep the order of nodes inside one path.

## 思路 - 图的遍历

这道题的思路应该是使用深度优先。

## 代码

```csharp
public class Solution {

    private int[][] gGraph;
    private int Len = 0;

    public IList<IList<int>> AllPathsSourceTarget(int[][] graph) {
        gGraph = graph;
        Len = gGraph.Length;
        return Sort(0);
    }


    private IList<IList<int>> Sort(int node)
    {
        IList<IList<int>> ans = new List<IList<int>>();

        if(node == Len - 1)
        {
            IList<int> path = new List<int>();
            path.Add(Len - 1);
            ans.Add(path);
            return ans;
        }

        foreach(var nei in gGraph[node])
        {
            foreach(var path in Sort(nei))
            {
                path.Insert(0, node);
                ans.Add(path);
            }
        }

        return ans;
    }

}
```
