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
    public TreeNode InsertIntoBST(TreeNode root, int val) {
        
        if(root == null) return new TreeNode(val);

        if(root.val < val)
            root.right = InsertIntoBST(root.right, val);
        else
            root.left = InsertIntoBST(root.left, val);
        return root;
        
    }
}

public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int x) { val = x; }
}