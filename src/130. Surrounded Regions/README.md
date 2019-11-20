# [Medium][130. Surrounded Regions](https://leetcode.com/problems/surrounded-regions/)

Given a 2D board containing 'X' and 'O' (the letter O), capture all regions surrounded by 'X'.

A region is captured by flipping all 'O's into 'X's in that surrounded region.

Example:

> X X X X
> X O O X
> X X O X
> X O X X

After running your function, the board should be:

> X X X X
> X X X X
> X X X X
> X O X X

**Explanation:**

Surrounded regions shouldn’t be on the border, which means that any 'O' on the border of the board are not flipped to 'X'. Any 'O' that is not on the border and it is not connected to an 'O' on the border will be flipped to 'X'. Two cells are connected if they are adjacent cells connected horizontally or vertically.

## 思路 - BFS

题目的要求是给出一个二维数组表示一块棋盘，要将被包围的'O'全部转变为'X'. 唯一的出路是在边界上。边界上的'O'会被保留，而且与边界上'O'相连通的'O'也会被保留。

由于是图的遍历，首先考虑到的是BFS或者DFS。这里使用的是BFS。因为只有边界上的'O'会最终保留，所有只要围着4条边界走一圈扫描是否有'O'的存在即可。如果遇到'O'，则按照它的位置开始以递归的方式开始遍历4个方向`E/S/W/N`.

图的另外一个要素是标记已经访问过的节点，这里采用的策略是将'O'的节点替换成'S' - `Survivor`。
在巡视完边界之后，开始扫描整块棋盘。如果遇到`S`，代表着之前扫描的时候跟边界连通的`O`，将`S`替换为`O`. 否则替换为'X'。这个时候遇到的`O`是被围困的，所有可以无差别对待。

## 代码 - BFS

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
