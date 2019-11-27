# [Medium][96. Unique Binary Search Trees](https://leetcode.com/problems/unique-binary-search-trees/)

Given n, how many structurally unique BST's (binary search trees) that store values 1 ... n?

**Example:**

```text
Input: 3
Output: 5
Explanation:
Given n = 3, there are a total of 5 unique BST's:

   1         3     3      2      1
    \       /     /      / \      \
     3     2     1      1   3      2
    /     /       \                 \
   2     1         2                 3
```

## 思路 - dp

本题的要求是给出一个数字n，给出包含的n个节点的BST数的所有排列组合。像这种求排列组合数目的题目，第一个想法就是用dp.
使用dp的情况下，首先要弄明白是否有可能的公式或者规律可循。

假设已经有个排好序的数组，长度为n.求它的各种BST组合，可以转换成下面这种图形。选取i为根节点，左边的转换为它的左子树，右边的元素转换为右子树。既然要给出所有的可能性，就将根节点从左遍历到右侧，依次计算它的sum。

![img](image/image1.png)

对于范围[0,n]的数组，当它的根节点在i的时候，它的BST的数目是：`Count(i) = F(i) * F(n-i-1)`
有2个特例是 `Count(1) = Count(0) = 1`

考虑到很多时候i会重复，所有要借助辅助的Dictionary来记录已经计算过的情况。

## 代码 - dp

```csharp
public class Solution {
    private Dictionary<int, int> memo = new Dictionary<int, int>();
    public int NumTrees(int n) {
        if(n <= 1) return 1;
        if(memo.ContainsKey(n)) return memo[n];
        int sum = 0;
        for(int i = 0; i <= n - 1; i++)
        {
            int lcount = NumTrees(i);
            int rcount = NumTrees(n - i - 1);
            sum += (lcount * rcount);
        }
        memo.Add(n, sum);
        return sum;
    }
}
```
