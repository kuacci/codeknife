# 654. Maximum Binary Tree

Given an integer array with no duplicates. A maximum tree building on this array is defined as follow:

1. The root is the maximum number in the array.
2. The left subtree is the maximum tree constructed from left part subarray divided by the maximum number.
3. The right subtree is the maximum tree constructed from right part subarray divided by the maximum number.
Construct the maximum tree by the given array and output the root node of this tree.

> Example 1:
Input: [3,2,1,6,0,5]
Output: return the tree root node representing the following tree:

``` text
      6
    /   \
   3     5
    \    /
     2  0
       \
        1
```

## 思路

观察数组的顺序，以及构造出来的树。发现里面的规律如下：

1. 数的根节点是数组中最大的节点。
2. 以最大的节点为根节点，数组中左边部分的元素作为左子树。数组右边部分的元素作为右子树。
3. 例如数组[3,2,1,**6**,0,5]，最大值为6，左边的元素为[3,2,1], 这三个元素作为左子树。从数组的左侧开始构造这个树的时候，如果下一个元素比上一个元素小，则把他连到上一个元素的右节点。

```text
      6
    /
   3
    \
     2
       \
        1
```

4. 当上一个元素比下一个元素小的时候，较大的数在右边，那么左边的元素要归到右边这个元素所构成的树节点的左子树。
5. 当前节点的left或者right以及有node的时候，则需要继续向下（左或者右）寻找合适的位置。
6. 如果比较了前一个节点的数，如果小于当前这个节点的数，则需要把前一个节点取下来，连到当前节点的left, 当前节点则替换掉前一节点的位置，连回去。例如[6,0]6与0,进行比较的时候，6 > 0, 0要连在6的右侧

```text
      6
        \
         0
```

当进行下一个元素比较的时候[6,0,5]，进行比较的时候，6 > 5, 5要连在6的右侧.6的右侧已经存在0，所以5和0进行比较。因为0 < 5, 0 连到5的左侧，5取代了0的位置，连在6的右侧。

```text
      6
        \
         5
         /
        0
```

## 代码

``` csharp
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
    public TreeNode ConstructMaximumBinaryTree(int[] nums) {

        if(nums == null || nums.Length == 0) return null;

        TreeNode head = new TreeNode(nums[0]);

        for(int i = 1; i < nums.Length; i ++)
        {
            head = PlaceTreeNode(head, new TreeNode(nums[i]));
        }

        return head;
    }

    private TreeNode PlaceTreeNode(TreeNode pre, TreeNode next)
    {
        if(pre.val > next.val)
        {
            if(pre.right != null)
            {
                next = PlaceTreeNode(pre.right, next);
            }
            pre.right = next;
        }
        else    // pre.val <= next.val
        {
            if(next.left != null)
            {
                pre = PlaceTreeNode(next.left, pre);
            }
            next.left = pre;
            pre = next;
        }

        return pre;
    }
}
```
