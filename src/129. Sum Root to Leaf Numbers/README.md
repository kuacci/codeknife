# [Medium][129. Sum Root to Leaf Numbers](https://leetcode.com/problems/sum-root-to-leaf-numbers/)

Given a binary tree containing digits from 0-9 only, each root-to-leaf path could represent a number.

An example is the root-to-leaf path 1->2->3 which represents the number 123.

Find the total sum of all root-to-leaf numbers.

Note: A leaf is a node with no children.

**Example:**

```text
Input: [1,2,3]
    1
   / \
  2   3
Output: 25
Explanation:
The root-to-leaf path 1->2 represents the number 12.
The root-to-leaf path 1->3 represents the number 13.
Therefore, sum = 12 + 13 = 25.
```

**Example 2:**

```text
Input: [4,9,0,5,1]
    4
   / \
  9   0
 / \
5   1
Output: 1026
Explanation:
The root-to-leaf path 4->9->5 represents the number 495.
The root-to-leaf path 4->9->1 represents the number 491.
The root-to-leaf path 4->0 represents the number 40.
Therefore, sum = 495 + 491 + 40 = 1026.
```

## 思路 - 先序遍历

这道题目的要求是将从根节点到叶子节点路径上的数字作为一个数，将所有的数进行相加求和。每条根节点到叶子节点的路径上的数字都为一个单独的数字。例如：

```text
    4
   / \
  9   0
 / \
5   1
```

可以分解为

```text
    4
   /
  9
 /
5
495
------------
    4
   /
  9
   \
    1

491
------------
    4
     \
      0

40
```

所有他们的和为 495 + 491 + 40 = 1026.

观察他们生成数字的过程，是一个先序遍历。

1. 先访问根节点，访问左，访问右节点，最终达到某个叶子节点。
2. 将传递下来的值* 10, 加上当前值，往下传递，以此来计算数字。
3. 当前节点如果是叶子节点，累加计算。

时间复杂度：O(N), 每个节点访问以此。
空间复杂度：O(N), 使用了一个变量来计算和为O(1), 递归使用了线程的栈O(N), 传入的TreeNode为O(N), 总共为O(1 + N + N) = O(1).

## 代码 - 先序遍历

```csharp
public class Solution {
    private int R = 0;
    private int C = 0;
    public void Solve(char[][] board) {
        if(board == null || board.Length == 0) return;
        R = board.Length;
        C = board[0].Length;
        for(int i = 0; i < C; i++) // E
        {
            Connect(board, 0, i);
            Connect(board, R - 1, i);
        }
        for(int i = 1; i < R - 1; i++) // S
        {
            Connect(board, i, 0);
            Connect(board, i, C - 1);
        }

        for(int i = 0; i < R; i++)
        {
            for(int j = 0; j < C; j++)
            {
                board[i][j] = board[i][j] == 'S' ? 'O' : 'X';
            }
        }
    }

    private void Connect(char[][] board, int row, int col)
    {
        if(row >= 0 && row <R && col >= 0 && col < C && board[row][col] == 'O')
        {
            board[row][col] = 'S';
            Connect(board, row, col + 1);  // E
            Connect(board, row + 1, col);  // S
            Connect(board, row, col - 1);  // W
            Connect(board, row - 1, col);  // N
        }
    }
}
```
