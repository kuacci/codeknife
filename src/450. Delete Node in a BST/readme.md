# 450. Delete Node in a BST

Given a root node reference of a BST and a key, delete the node with the given key in the BST. Return the root node reference (possibly updated) of the BST.

Basically, the deletion can be divided into two stages:

Search for a node to remove.
If the node is found, delete the node.
Note: Time complexity should be O(height of tree).

Example:

```text
root = [5,3,6,2,4,null,7]
key = 3

    5
   / \
  3   6
 / \   \
2   4   7
```

Given key to delete is 3. So we find the node with value 3 and delete it.

One valid answer is [5,4,6,2,null,null,7], shown in the following BST.

```text
    5
   / \
  4   6
 /     \
2       7
```

Another valid answer is [5,2,6,null,4,null,7].

```text
    5
   / \
  2   6
   \   \
    4   7
```

## 思路

要删除某个节点，首先要找到这个节点。这个比较简单，用递归的方式，先走到该节点的位置。如果node.val < val, 则走右边，如果node.val > val 则走左边，如果node.val == val，进入到删除节点的逻辑。

``` csharp
    if(root.val > key)
    {
        node.left = DeleteNode(root.left, key);
    }
    else if (root.val < key) {
        node.right = DeleteNode(root.right, key);
    }
    else
    {
        // delete the node
    }
```

比较困难的地方是删除一个节点，并且还要保持树的有序性。删除的时候有3中情况：

1. 被删除的是叶子节点。
2. 被删除的一侧有子树，另一侧没有。
3. 被删除的节点左右两侧有树。

### 被删除的是叶子节点

这种情况比较简单，将一个null 返回父节点left/right, 完成删除。

![treenode1](/image/treenode1.jpg)

### 被删除的一侧有子树，另一侧没有

这种情况也相对简单，只要将一侧的子树返回给父节点，替换掉原先要删除的节点的位置即可。

![treenode2](/image/treenode2.jpg)

### 被删除的节点左右两侧有树

这种情况最为复杂。删除的节点有左子树和右子树。采用上面的方式，返回任意一侧的子树的同时又必须处理另外一侧的子树，毕竟父节点只能接收一颗子树。为了能够保持BST的有序性，需要先进行左右子树的合并。BST的特性就是，左子树所有节点小于当前节点，而右子树所有节点大于当前节点。所有可以知道左子树一定小于右子树。当删除当前节点的时候，可以将右子树移到左子树的最右端。即，先travel到左子树的大的右叶子节点（一直靠右走），然后接在右叶子最右侧。当然反过来也可以。

![treenode3](/image/treenode3.jpg)

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
    public TreeNode DeleteNode(TreeNode root, int key) {
        TreeNode node = root;
        if(root == null) return null;

        if(root.val > key)
        {
            node.left = DeleteNode(root.left, key);
        }
        else if (root.val < key) {
            node.right = DeleteNode(root.right, key);
        }
        else
        {
            if(root.left == null && root.right == null)
                return null;
            else if (root.left == null)
            {
                return root.right;
            }
            else if (root.right == null)
            {
                return root.left;
            }
            else
            {
                node = FindAndReplace(root.left, root.right);
            }
        }

        return node;
    }

    private TreeNode FindAndReplace(TreeNode root, TreeNode right)
    {
        if(root.right == null)
            root.right = right;
        else
            root.right = FindAndReplace(root.right, right);
        return root;
    }

}
```
