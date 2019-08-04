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
    public TreeNode BstFromPreorder(int[] preorder) {

        if(preorder?.Length == 0) return null;

        TreeNode root = null;

        for(int i = 0; i < preorder.Length; i++)
            root = BuildTreeNode(root, preorder[i]);
        return root;
    }

    private TreeNode BuildTreeNode(TreeNode root, int val)
    {
        if(root == null) return new TreeNode(val);

        if(val < root.val)
        {
            root.left = BuildTreeNode(root.left, val);
        }
        else
        {
            root.right = BuildTreeNode(root.right, val);
        }
        return root;
    }
}

public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int x) { val = x; }
}