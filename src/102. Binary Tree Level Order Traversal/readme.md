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

层次遍历，是按照顺序从左到右访问自己的兄弟节点，所有兄弟节点都访问完成之后才访问他们的子节点。在不使用辅助数组的情况下使用先序，中序或者后续，都不实际，因为要跨到兄弟节点，需要经过一个交汇的父节点。如果兄弟节点跨的很远，甚至需要回到根节点再向下。

为了解决这个问题，需要借助一个Queue, 在访问根节点的时候，将左右节点放到Queue中。访问完根节点以后，从Queue里面拿出子节点依次进行访问。因为Queue里面的节点是来自于共一个父节点，都是兄弟节点。再次遍历的时候，将下一层子节点再放入Queue中。因为这些节点都来自于同一层的兄弟节点，所以他们都属于兄弟节点。

每个节点被访问了2次，一次是放入到Queue中，一次是从Queue里面提取。时间复杂度为O(2 * N) = O(N), 空间复杂度，借助了一个Queue，最大的长度是树的宽度，假如是一个满二叉树大小为N, 宽度为 2^(n-1), 这个queue必须能容纳下这个宽度。空间复杂度为O(N).

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

## 思路 - 先序遍历 + `List<int>`

在层次遍历的时候，借助了Queue来保存兄弟节点。同样，在借助辅助数组的情况下，也能使用先序遍历的方式获得层次遍历的结果。
在左先序遍历的时候，传入一个参数来标识当前访问的节点是第几层，然后依次放入当前层次的辅助数组中。

```csharp
private void helper(TreeNode node, IList<IList<int>> ans， int level)
{
    if(node == null)
        return;

    // add node to proper List<int>

    helper(node.left, ans, level + 1);
    helper(node.left, ans, level + 1);

}
```

由于所以节点只需要访问依次，时间复杂度比借助Queue的时候要节省一半。O(N).没有使用Queue，所以也节省了使用Queue的空间，取而代之的是使用了线程的栈作为存储空间。同时需要辅助数组作为返回值，空间复杂度依然是O(N).

## 代码 - 先序遍历 + `List<int>`

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
        helper(root, ans, 0);

        return ans;
    }

    private void helper(TreeNode node, IList<IList<int>> ans, int level)
    {
        if(node == null)
            return;
        if(ans.Count < level + 1)
            ans.Add(new List<int>());
        ans[level].Add(node.val);

        helper(node.left, ans, level + 1);
        helper(node.right, ans, level + 1);
    }
}
```
