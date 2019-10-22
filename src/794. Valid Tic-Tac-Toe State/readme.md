# [Medium][794. Valid Tic-Tac-Toe State](https://leetcode.com/problems/valid-tic-tac-toe-state/)

A Tic-Tac-Toe board is given as a string array board. Return True if and only if it is possible to reach this board position during the course of a valid tic-tac-toe game.

The board is a 3 x 3 array, and consists of characters " ", "X", and "O".  The " " character represents an empty square.

Here are the rules of Tic-Tac-Toe:

* Players take turns placing characters into empty squares (" ").
* The first player always places "X" characters, while the second player always places "O" characters.
* "X" and "O" characters are always placed into empty squares, never filled ones.
* The game ends when there are 3 of the same (non-empty) character filling any row, column, or diagonal.
* The game also ends if all squares are non-empty.
* No more moves can be played if the game is over.

**Example 1:**

> Input: board = ["O  ", "   ", "   "]
> Output: false
> Explanation: The first player always plays "X".

**Example 2:**

> Input: board = ["XOX", " X ", "   "]
> Output: false
> Explanation: Players take turns making moves.

**Example 3:**

> Input: board = ["XXX", "   ", "OOO"]
> Output: false

**Example 4:**

> Input: board = ["XOX", "O O", "XOX"]
> Output: true

**Note:**

> board is a length-3 array of strings, where each string board[i] has length 3.
> Each board[i][j] is a character in the set {" ", "X", "O"}.

## 思路 - 穷举法

这道题的下一个井字棋。输入一个棋盘的布局，然后验证这个布局是否合法。规则如下：

* 玩家轮流将字符放入空位（" "）中。
* 第一个玩家总是放字符 “X”，且第二个玩家总是放字符 “O”。
* “X” 和 “O” 只允许放置在空位中，不允许对已放有字符的位置进行填充。
* 当有 3 个相同（且非空）的字符填充任何行、列或对角线时，游戏结束。
* 当所有位置非空时，也算为游戏结束。
* 如果游戏结束，玩家不允许再放置字符。

总结下来几个规律：

1. 先数下过的棋子。PlayA下过的棋子数量应该是PlayB的数量相等，或者最多多一个。因为PlayA先手。
2. 验证2个人是否都赢了，都赢是不可能的。
3. 平手是允许的，也作为true，但是棋子数参看1#.
4. 如果PlayerA赢了棋子数 A = B + 1;
5. 如果PlayerB赢了棋子数 A = B;

## 代码 - 穷举法

```csharp
public class Solution {
    public bool ValidTicTacToe(string[] board)
    {
        char[][] chs = new char[3][];
        for (int i = 0; i < board.Length; i++)
        {
            chs[i] = board[i].ToCharArray();
        }

        int playera = 0;
        int playerb = 0;

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (chs[i][j] == 'X') playera++;
                else if (chs[i][j] == 'O') playerb++;

        bool awin = IsWin(chs, 'X');
        bool bwin = IsWin(chs, 'O');

        if (awin && bwin) return false;
        else if (awin && playera <= playerb) return false;
        else if (bwin && playerb != playera) return false;
        else if (playera < playerb || playera > playerb + 1) return false;

        return true;
    }
    private bool IsWin(char[][] chs, char c)
    {
        if (chs[1][1] == c)
        {
            return (chs[0][0] == c && chs[2][2] == c)
                || (chs[0][2] == c && chs[2][0] == c)
                || (chs[0][1] == c && chs[2][1] == c)
                || (chs[1][0] == c && chs[1][2] == c);
        }
        else if (chs[0][0] == c)
        {
            return (chs[0][1] == c && chs[0][2] == c) || (chs[1][0] == c && chs[2][0] == c);
        }
        else if (chs[2][2] == c)
        {
            return (chs[0][2] == c && chs[1][2] == c) || (chs[2][0] == c && chs[2][1] == c);
        }
        return false;
    }
}
```
