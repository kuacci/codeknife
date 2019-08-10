# 814. Binary Tree Pruning

We are given the head node root of a binary tree, where additionally every node's value is either a 0 or a 1.

Return the same tree where every subtree (of the given tree) not containing a 1 has been removed.

(Recall that the subtree of a node X is X, plus every node that is a descendant of X.)

Example 1:
Input: [1,null,0,0,1]
Output: [1,null,0,null,1]

Explanation:

```text
Only the red nodes satisfy the property "every subtree not containing a 1".
The diagram on the right represents the answer.
```

![img1](image/1.png)

Example 2:
Input: [1,0,1,0,0,0,1]
Output: [1,null,1,null,1]
![img2](image/2.png)

Example 3:
Input: [1,1,0,1,1,0,1,0]
Output: [1,1,0,1,1,null,1]

![img3](image/3.png)

Note:

The binary tree will have at most 100 nodes.
The value of each node will only be 0 or 1.

## 思路

按照这题的要求，应该是用先序遍历，先左后右再中。如果是叶子节点，且val为0的情况下，将当前节点置为null，非叶子节点则返回本身。

## 代码

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
    public TreeNode PruneTree(TreeNode root) {

        if(root.left != null)
            root.left = PruneTree(root.left);
        if(root.right != null)
            root.right = PruneTree(root.right);

        if(root.left == null && root.right == null && root.val == 0)
            return null;
        else
            return root;
    }
}
```
