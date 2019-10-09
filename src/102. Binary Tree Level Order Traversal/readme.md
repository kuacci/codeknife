# [102. Binary Tree Level Order Traversal](https://leetcode.com/problems/binary-tree-level-order-traversal/)

Given a binary tree, return the level order traversal of its nodes' values. (ie, from left to right, level by level).

**For example:**

```text
Given binary tree [3,9,20,null,null,15,7],
    3
   / \
  9  20
    /  \
   15   7
return its level order traversal as:
[
  [3],
  [9,20],
  [15,7]
]
```

## 思路 - 层次遍历

层次遍历，是按照顺序从左到右访问自己的兄弟节点，所有兄弟节点都访问完成之后才访问他们的子节点。在。使用先序，中序或者后续，都不实际，因为要跨到兄弟节点，需要经过一个交汇的父节点。如果兄弟节点跨的很远，甚至需要回到根节点再向下。

为了解决这个问题，需要借助一个Queue, 在访问根节点的时候，将左右节点放到Queue中。访问完根节点以后，从Queue里面拿出子节点依次进行访问。因为Queue里面的节点是来自于共一个父节点，都是兄弟节点。再次遍历的时候，将下一层子节点再放入Queue中。因为这些节点都来自于同一层的兄弟节点，所以他们都属于兄弟节点。

## 代码 - 层次遍历

```csharp
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    public IList<IList<int>> LevelOrder(TreeNode root) {

        IList<IList<int>> ans = new List<IList<int>>();
        if(root == null) return ans;

        Queue<TreeNode> nodes = new Queue<TreeNode>();
        nodes.Enqueue(root);

        while(nodes.Count > 0)
        {
            IList<int> lvl = new List<int>();

            for(int i = nodes.Count; i > 0; i--)
            {
                TreeNode node = nodes.Dequeue();
                if(node.left != null)
                    nodes.Enqueue(node.left);
                if(node.right != null)
                    nodes.Enqueue(node.right);
                lvl.Add(node.val);
            }
            ans.Add(lvl);
        }
        return ans;
    }
}
```
