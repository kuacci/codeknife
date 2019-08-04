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
        
        TreeNode tmp = root;
        
        while(true)
        {
            if(tmp.val > val)
            {
                if(tmp.left == null)
                {
                    tmp.left = new TreeNode(val);
                    break;
                }
                else
                {
                    tmp = tmp.left;
                }
            }
            else
            {
                if(tmp.right == null)
                {
                    tmp.right = new TreeNode(val);
                    break;
                }
                else
                {
                    tmp = tmp.right;
                }
            }
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