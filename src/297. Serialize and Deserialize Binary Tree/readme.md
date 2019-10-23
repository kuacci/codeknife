# [Hard][297. Serialize and Deserialize Binary Tree](https://leetcode.com/problems/serialize-and-deserialize-binary-tree/)

Serialization is the process of converting a data structure or object into a sequence of bits so that it can be stored in a file or memory buffer, or transmitted across a network connection link to be reconstructed later in the same or another computer environment.

Design an algorithm to serialize and deserialize a binary tree. There is no restriction on how your serialization/deserialization algorithm should work. You just need to ensure that a binary tree can be serialized to a string and this string can be deserialized to the original tree structure.

Example:

```text
You may serialize the following tree:

    1
   / \
  2   3
     / \
    4   5
```

as "`[1,2,3,null,null,4,5]`"
Clarification: The above format is the same as how LeetCode serializes a binary tree. You do not necessarily need to follow this format, so please be creative and come up with different approaches yourself.

Note: Do not use class member/global/static variables to store states. Your serialize and deserialize algorithms should be stateless.

## 思路 - 先序 + 中序

这道题的目的是要求写出2个method，一个method用来将一个树序列化成为string. 另外一个method用来一串string转换为树。
将树转换为string的方法很多，先序，中序，后续或者层次遍历都可以。主要是要配合后面的反序列化，如何将一串string转化为tree.

首先想到的是采用先序 + 中序。用先序可以确认tree的root的位置，用中序可以确认子节点是左节点还是右节点。

因为这里的树是一颗二叉树，而不是二叉搜索树，所有可能存在重复值，并且也是无序的。生成string的时候，要记录tree node的顺序，所以还要结束一个Dictionary来保存节点ID和value。

## 代码 - 先序 + 中序

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
public class Codec
{
    private const string split = "|||";
    // Encodes a tree to a single string.
    public string serialize(TreeNode root)
    {
        if (root == null) return string.Empty;

        Dictionary<int, int> prerecorder = new Dictionary<int, int>();
        List<int> inrecorder = new List<int>();
        int id = 1;
        PreOrderTravel(root, prerecorder, ref id);
        InOrderTravel(root, inrecorder);

        StringBuilder ans = new StringBuilder();
        foreach (var item in prerecorder)
        {
            ans.Append($"{item.Key}:{item.Value},");
        }
        if (prerecorder.Count > 0)
            ans.Remove(ans.Length - 1, 1);
        ans.Append(split);

        for (int i = 0; i < inrecorder.Count; i++)
        {
            ans.Append($"{inrecorder[i]},");
        }

        if (prerecorder.Count > 0)
            ans.Remove(ans.Length - 1, 1);

        return ans.ToString();

    }

    private void PreOrderTravel(TreeNode node, Dictionary<int, int> recorder, ref int id)
    {
        if (node == null) return;

        recorder.Add(id, node.val);
        node.val = id;
        id += 1;
        if (node.left != null)
            PreOrderTravel(node.left, recorder, ref id);
        if (node.right != null)
            PreOrderTravel(node.right, recorder, ref id);
    }

    private void InOrderTravel(TreeNode node, List<int> recorder)
    {
        if (node == null) return;
        if (node.left != null)
            InOrderTravel(node.left, recorder);
        recorder.Add(node.val);
        if (node.right != null)
            InOrderTravel(node.right, recorder);
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;

        string[] nodes = data.Split(split);

        List<int> prenums = new List<int>();
        Dictionary<int, int> predic = new Dictionary<int, int>();
        foreach(var val in nodes[0].Split(","))
        {
            string[] kv = val.Split(":");
            int k = int.Parse(kv[0]);
            int v = int.Parse(kv[1]);
            prenums.Add(k);
            predic.Add(k, v);
        }

        List<int> innums = new List<int>();
        foreach (var val in nodes[1].Split(","))
        {
            int v = int.Parse(val);
            innums.Add(v);
        }

        int pIndex = 0;
        return ReBuildTree(prenums, innums, predic, 0, innums.Count - 1, ref pIndex);
    }

    private TreeNode ReBuildTree(List<int> prenum, List<int> innum, Dictionary<int,int> predic, int lindex,  int rindex, ref int pIndex)
    {
        if (pIndex > prenum.Count || lindex > rindex || lindex < 0 || rindex >= innum.Count) return null;

        int key = prenum[pIndex];
        int mid = -1;
        for (int i = lindex; i <= rindex; i++)
        {
            if(innum[i] == key)
            {
                mid = i;
                break;
            }
        }
        if (mid == -1) return null;

        TreeNode node = new TreeNode(predic[key]);
        pIndex += 1;
        if (pIndex < prenum.Count)
        {
            node.left = ReBuildTree(prenum, innum, predic, lindex, mid - 1, ref pIndex);
        }
        if(pIndex < prenum.Count)
        {
            node.right = ReBuildTree(prenum, innum, predic, mid + 1, rindex, ref pIndex);
        }

        return node;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.deserialize(codec.serialize(root));
```

## 思路 - 先序遍历

上述的方法太过冗余，string的产生使用了先序和中序两种遍历方式。为了简化方法，可以只使用先序。这里是借鉴了[1008. Construct Binary Search Tree from Preorder Traversal](https://leetcode.com/problems/construct-binary-search-tree-from-preorder-traversal/)

但是这里有个地方要注意的时候，序列化的时候，这棵树并不是一个满二叉树，所以遇到一侧子树/子节点为空的情况，要加入字符`"#"`标识为NULL.

例如下面这棵树生成的string是1,2,#,#,3,4,#,#,5,#,#

```text
    1
   / \
  2   3
     / \
    4   5
```

反序列化的时候，如果遇到数字，则按照先序的方式一直递归，遇到`"#"`则直接返回，接到左侧或者右侧。终止的条件是`string[] data`全部遍历完成.

## 代码 - 先序遍历

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
public class Codec
{
    private const string EmptySym = "#";
    // Encodes a tree to a single string.
    public string serialize(TreeNode root)
    {
        if (root == null) return string.Empty;
        StringBuilder ans = new StringBuilder();
        PreOrderTravel(root, ans);
        return ans.Remove(ans.Length - 1, 1).ToString();
    }

    private void PreOrderTravel(TreeNode node, StringBuilder ans)
    {
        if (node == null)
            ans.Append(EmptySym + ",");
        else
        {
            ans.Append(node.val + ",");
            PreOrderTravel(node.left, ans);
            PreOrderTravel(node.right, ans);
        }
    }

    private void InOrderTravel(TreeNode node, List<int> recorder)
    {
        if (node == null) return;
        if (node.left != null)
            InOrderTravel(node.left, recorder);
        recorder.Add(node.val);
        if (node.right != null)
            InOrderTravel(node.right, recorder);
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;

        string[] nodes = data.Split(",");
        int index = 0;
        return ReBuildTree(nodes, ref index);
    }

    private TreeNode ReBuildTree(string[] data, ref int index)
    {
        string vstr = data[index];
        TreeNode node = null;
        index += 1;
        if (vstr != EmptySym)
        {
            int val = int.Parse(vstr);
            node = new TreeNode(val);

            if (index < data.Length)
            {
                node.left = ReBuildTree(data, ref index);
                node.right = ReBuildTree(data, ref index);
            }
        }

        return node;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.deserialize(codec.serialize(root));
```
