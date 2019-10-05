# [79. Word Search](https://leetcode.com/problems/word-search/)

Given a 2D board and a word, find if the word exists in the grid.

The word can be constructed from letters of sequentially adjacent cell, where "adjacent" cells are those horizontally or vertically neighboring. The same letter cell may not be used more than once.

**Example:**

```text
board =
[
  ['A','B','C','E'],
  ['S','F','C','S'],
  ['A','D','E','E']
]
```

Given word = **"ABCCED"**, return true.
Given word = **"SEE"**, return true.
Given word = **"ABCB"**, return false.

## 思路 - dfs + 回溯算法

这道题的要求是在二维数组中找到给定的字符串，这个字符串在二维数组中是连续的，可以拐弯。例如上面的例子`ABCCED`和`SEE`是存在的。简化出来如下方：

```text
ABCCED
[
  ['A','B','C','-'],
  ['-','-','C','-'],
  ['-','D','E','-']
]
```

```text
SEE
[
  ['-','-','-','-'],
  ['-','-','-','S'],
  ['-','-','E','E']
]
```

这种问题很类似与走迷宫，在二维数组中找出一条路径。最合适的方法是dfs和bfs.不过我这里采用的是dfs.

```csharp
private bool helper(int pos, int r, int c)
{
    ...

    if(r < this.row - 1 && words[pos + 1] == board[r + 1][c] && helper(pos + 1, r + 1, c))
        return true;

    if(r > 0 && words[pos + 1] == board[r - 1][c] && helper(pos + 1, r - 1, c))
        return true;

    if(c < this.col - 1 && words[pos + 1] == board[r][c + 1] && helper(pos + 1, r, c + 1))
        return true;

    if(c > 0 && words[pos + 1] == board[r][c - 1] && helper(pos + 1, r, c - 1))
        return true;
    ...
}
```

无论哪一种，都要采用vist & mark的方式。这种方式在一般的走迷宫没问题，走一步mark一步。但是在这里可能会有问题。因为被MARK掉的元素，很可能是后面几步要匹配的元素。例如下面的这个例子。

```text
SEEN
[
  ['-','S','-','-'],
  ['-','M','S','-'],
  ['-','E','E','-']
]
```

这里面要匹配的字符串是SEEM,是存在的。那么如果要vist & mark。M是SEEM的最后一个char。在匹配到第一行的S的时候，向上下左右没有E，这个时候把M mark为已经访问过，那么在找到第二个S的时候，由于M已经被mark，那么无法被访问到，会返回错误的结果。

```text
SEEN
[
  ['-','S','-','-'],
  ['-','(X)','S','-'],
  ['-','E','E','-']
]
```

如果不MARK也有问题。在匹配连续重复字符的时候也可能出错。

```text
EEEE
[
  ['-','-','-','-'],
  ['-','-','-','-'],
  ['-','E','E','-']
]
```

这种情况，矩阵可能只有两个EE的情况下返回.

为了避免这种情况，可以引入回溯算法。遍历所有的可能性，在MARK之后恢复到原来的状态。

```csharp
private bool helper(int pos, int r, int c)
{
    ...

    char tmp = board[r][c];
    board[r][c] = MASK;

    if(r < this.row - 1 && words[pos + 1] == board[r + 1][c] && helper(pos + 1, r + 1, c))
        return true;
    ...
    board[r][c] = tmp;
    return false;
    ...
}
```

## 代码 - dfs + 回溯算法

```csharp
public class Solution {
    private char[][] board;
    private char[] words;
    private int row = 0;
    private int col = 0;
    private char MASK = '0';
    public bool Exist(char[][] board, string word) {
        if(board == null || board.Length == 0 || board[0].Length == 0 || string.IsNullOrEmpty(word)) return false;

        this.board = board;
        words = word.ToCharArray();

        this.row = board.Length;
        this.col = board[0].Length;

        for(int r = 0; r < row; r++)
            for(int c = 0; c < col; c++ )
                if(words[0] == board[r][c] && helper(0, r, c))
                { return true;}

        return false;
    }

    private bool helper(int pos, int r, int c)
    {

        if(pos == words.Length - 1) return true;

        char tmp = board[r][c];
        board[r][c] = MASK;
        bool success = false;

        if(r < this.row - 1 && words[pos + 1] == board[r + 1][c] && helper(pos + 1, r + 1, c))
            return true;

        if(r > 0 && words[pos + 1] == board[r - 1][c] && helper(pos + 1, r - 1, c))
            return true;

        if(c < this.col - 1 && words[pos + 1] == board[r][c + 1] && helper(pos + 1, r, c + 1))
            return true;

        if(c > 0 && words[pos + 1] == board[r][c - 1] && helper(pos + 1, r, c - 1))
            return true;

        board[r][c] = tmp;
        return false;
    }
}
```
