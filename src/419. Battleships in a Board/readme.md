# [Medium][419. Battleships in a Board](https://leetcode.com/problems/battleships-in-a-board/)

Given an 2D board, count how many battleships are in it. The battleships are represented with 'X's, empty slots are represented with '.'s. You may assume the following rules:

* You receive a valid board, made of only battleships or empty slots.
* Battleships can only be placed horizontally or vertically. In other words, they can only be made of the shape 1xN (1 row, N columns) or Nx1 (N rows, 1 column), where N can be of any size.
* At least one horizontal or vertical cell separates between two battleships - there are no adjacent battleships.

**Example:**

> X..X
> ...X
> ...X

In the above board there are 2 battleships.
**Invalid Example:**

> ...X
> XXXX
> ...X

This is an invalid board that you will not receive - as battleships will always have a cell separating between them.

**Follow up:**
Could you do it in **one-pass**, using only **O(1) extra memory** and **without modifying** the value of the board?

## 思路

这题是用一个二维数组表示一个战舰的布置板。战舰所在的位置用'X'来标识。战舰的方向是纵向或者竖向，两艘战舰之间是不会相交的。

按照这个设定，用两个for循环遍历二维数组。如果碰到'X', 分别判断纵向`board[h][w-1]`和竖向`board[h-1][w]`-1的位置是否为'X'。如果是战舰的起始位置，那么他的上一个位置必然不是'X'而是'.'，反之，如果是'X'，则必然不是战舰起始位置，就不需要再加一艘战舰了。

## 代码

```csharp
public class Solution {
    public int CountBattleships(char[][] board) {

        if(board == null || board.Length == 0 || board[0].Length == 0) return 0;

        int count = 0;
        int heigth = board.Length;
        int width = board[0].Length;

        for(int h = 0; h < heigth; h++)
        {
            for(int w = 0; w < width; w++)
            {
                if(board[h][w] == 'X')
                {

                    if(h > 0 && board[h-1][w] == 'X') continue;
                    if(w > 0 && board[h][w-1] == 'X') continue;

                    count ++;
                }
            }
        }

        return count;
    }
}
```

## 思路 - [改进]

上述的算法中，战舰的位置可能会被访问2次。第一个位置被访问的时候，先计算一次，挪到下一个位置的时候，会再次检测上原来位置。为了能够减少重复访问，如果已经标识为战舰的首位置， 则继续检查纵向的下一个位置，如果是'X'，那么将w直接移动到下一个位置。

## 代码 - [改进]

```csharp
public class Solution {
    public int CountBattleships(char[][] board) {

        if(board == null || board.Length == 0 || board[0].Length == 0) return 0;

        int count = 0;

        for(int h = 0; h < board.Length; h++)
        {
            for(int w = 0; w < board[0].Length; w++)
            {
                if(board[h][w] == 'X')
                {
                    if(h > 0 && board[h-1][w] == 'X') continue;
                    while(w < board[0].Length - 1 && board[h][w+1] == 'X') w++;
                    count ++;
                }
            }
        }

        return count;
    }
}
```

## 思路 - [改进2]

上面的代码行数比较多，在leetcode上的执行效率只能排在中等水平。还需要优化代码就要转化思路。核心的想法还是一个：在检测到舰船第一个位置的时候，count++. 观察各种情况，舰船首位置有下面的几个pattern：

1. 出现在board[0][0]上的情况，则不检查上一个位置，因为没有。
2. 首位置出现在0行，即 w = 0的情况。这个时候只有竖向位置board[h-1][0] == '.'的情况可以确认是舰首。
3. 首位置出现在0列，即 h = 0的情况。这个时候只有横向位置board[0][w-1] == '.'的情况可以确认是舰首。
4. 当 w != 0 && h != 0的情况，这个是 board[h-1][w] == '.' && board[h][w-1] == '.' 的情况才可以确认是舰首。

## 代码 - [改进3]

```csharp
public class Solution {
    public int CountBattleships(char[][] board) {

        if(board == null || board.Length == 0 || board[0].Length == 0) return 0;

        int count = 0;

        for(int h = 0; h < board.Length; h++)
        {
            for(int w = 0; w < board[0].Length; w++)
            {
                if(board[h][w] == 'X' && ((h == 0 || board[h-1][w] == '.') && (w == 0 || board[h][w-1] == '.')))
                    count++;
            }
        }
        return count;
    }
}
```
