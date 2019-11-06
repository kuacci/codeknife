# [Medium][36. Valid Sudoku](https://leetcode.com/problems/valid-sudoku/)

Determine if a 9x9 Sudoku board is valid. Only the filled cells need to be validated according to the following rules:

1. Each row must contain the digits 1-9 without repetition.
2. Each column must contain the digits 1-9 without repetition.
3. Each of the 9 3x3 sub-boxes of the grid must contain the digits 1-9 without repetition.

![img](image/figure.png)

The Sudoku board could be partially filled, where empty cells are filled with the character '.'.

Example 1:

```text
Input:
[
  ["5","3",".",".","7",".",".",".","."],
  ["6",".",".","1","9","5",".",".","."],
  [".","9","8",".",".",".",".","6","."],
  ["8",".",".",".","6",".",".",".","3"],
  ["4",".",".","8",".","3",".",".","1"],
  ["7",".",".",".","2",".",".",".","6"],
  [".","6",".",".",".",".","2","8","."],
  [".",".",".","4","1","9",".",".","5"],
  [".",".",".",".","8",".",".","7","9"]
]
Output: true
```

**Example 2:**

```text
Input:
[
  ["8","3",".",".","7",".",".",".","."],
  ["6",".",".","1","9","5",".",".","."],
  [".","9","8",".",".",".",".","6","."],
  ["8",".",".",".","6",".",".",".","3"],
  ["4",".",".","8",".","3",".",".","1"],
  ["7",".",".",".","2",".",".",".","6"],
  [".","6",".",".",".",".","2","8","."],
  [".",".",".","4","1","9",".",".","5"],
  [".",".",".",".","8",".",".","7","9"]
]
Output: false
Explanation: Same as Example 1, except with the 5 in the top left corner being
    modified to 8. Since there are two 8's in the top left 3x3 sub-box, it is invalid.
```

Note:

A Sudoku board (partially filled) could be valid but is not necessarily solvable.
Only the filled cells need to be validated according to the mentioned rules.
The given board contain only digits 1-9 and the character '.'.
The given board size is always 9x9.

## 思路 - 逐行扫描

按照数独的游戏规则，逐行扫描每个元素。用一个 `IList<IList<int>> rowCache = new List<IList<int>>();` 保存某一行出现过的数字。用`IList<IList<int>> colCache = new List<IList<int>>();` 保存某一列的数字。
`Dictionary<int, List<int>> bloCache = new Dictionary<int, List<int>>();` 则用来保存摸一个块的数字。 块号由 (row / 3) * 10 + (col / 2)来表达。十位为行号，个位为列号，每3行或者3列并到一个块中。

例如 ：0行0列属于 block 0. 9 行9列属于block 22.

扫描的时候，分别判断上述3个cache中是否已经出现过重复的数字，如果没有将继续。如果出现则返回false.

## 代码 - 逐行扫描

```csharp
public class Solution {
    public bool IsValidSudoku(char[][] board)
    {
        IList<IList<int>> rowCache = new List<IList<int>>();
        IList<IList<int>> colCache = new List<IList<int>>();
        Dictionary<int, List<int>> bloCache = new Dictionary<int, List<int>>();
        for (int i = 0; i < board.Length; i++)
        {
            rowCache.Add(new List<int>());
        }
        for (int i = 0; i < board[0].Length; i++)
        {
            colCache.Add(new List<int>());
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int key = i * 10 + j;
                bloCache.Add(key, new List<int>());
            }
        }

        for (int row = 0; row < board.Length; row++)
        {
            for (int col = 0; col < board[0].Length; col++)
            {
                char val = board[row][col];
                if (val == '.') continue;

                int content = val - '0';

                if (rowCache[row].Contains(content)) return false;
                if (colCache[col].Contains(content)) return false;
                int bloIndex = (row / 3) * 10 + (col / 3);
                if (bloCache[bloIndex].Contains(content)) return false;

                rowCache[row].Add(content);
                colCache[col].Add(content);
                bloCache[bloIndex].Add(content);
            }
        }

        return true;
    }
}
```
