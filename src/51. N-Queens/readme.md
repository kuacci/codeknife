# [51. N-Queens](https://leetcode.com/problems/n-queens/)

The n-queens puzzle is the problem of placing n queens on an n×n chessboard such that no two queens attack each other.

![img](image/8-queens.png)

Given an integer n, return all distinct solutions to the n-queens puzzle.

Each solution contains a distinct board configuration of the n-queens' placement, where 'Q' and '.' both indicate a queen and an empty space respectively.

Example:

```text
Input: 4
Output: [
 [".Q..",  // Solution 1
  "...Q",
  "Q...",
  "..Q."],

 ["..Q.",  // Solution 2
  "Q...",
  "...Q",
  ".Q.."]
]
Explanation: There exist two distinct solutions to the 4-queens puzzle as shown above.
```

## 思路 - back trace

思路跟[46. Permutations](../46.%20Permutations)很类似。通过back trace来尝试各种的可能性。先假设第一列棋子的位置，然后通过递归的方式来确定第二列，以此类推。将棋子的位置暂存到中间状态的一个`IList<string> graph`对象中，如果能走到最后一列并且布置上棋子，那么就是合法的solution,所以最后才将结果保存到`IList<IList<string>> ans = new List<IList<string>>();`中。

```csharp
private void backtrace(IList<string> graph, int r)
{
    if (r == N) // valid solution
    {
        List<string> solve = new List<string>();
        for (int i = 0; i < graph.Count; i++)
        {
            solve.Add(graph[i]);
        }
        ans.Add(solve);
        return;
    }

    for(int i = 0; i < N; i++)
    {
        if (pos[r,i] == 0)
        {
            string g = PrintPos(i); // get print string
            SetMatrix(r, i, 1);     // when place a queen, set matrix flag. when place next, check whether the postion is valid
            graph.Add(g);
            backtrace(graph, r + 1);
            graph.Remove(g);        // restore print string status
            SetMatrix(r, i, -1);    // restore matrix flags
        }
    }
}
```

能否放置棋子的条件。为了确定下一个位置是否可以放置棋子，我使用了一个N X N的`int[,]`, 用来模拟棋盘。之所以不使用`bool[,]`, 因为它只有true or false 两个状态。在set/resotre matrix flags的时候不能正确的表述某个格子受到多少个queen是限制。
举个例子：
如上图 int[4,d]这个位置不能放置`queen`，因为有3个`queen`的路径通过这个格子，分别是`int[4,b]`, `int[8,d]` 和`int[3,e]`. 所以要通过int[,]来记录有所少个`queen`的路径通过了这个格子。这样在restore matrix flag的时候，是将上面的值-1,而不是将它设置为true，这样避免了错误的发生。这一思路来源于handler count.

设flag的方式是先设置水平方向和垂直方向。对于斜向，设定为4个方向。为了避免重复，需要跳过`r1 = r` 和 `c1 = c`的情况。

```csharp
private void SetMatrix(int r, int c, int num)
{
    pos[r, c] += num;

    // set vertical & horizon items
    for (int i = 0; i < N; i++)
    {
        if(i != c)
        {
            pos[r, i] += num; // set vertical direction
        }

        if (i != r)
        {
            pos[i, c] += num; // set horizontal direction
        }
    }

    int[] dr = new int[4] { 1, 1, -1, -1 };
    int[] dC = new int[4] { 1, -1, -1, 1 };

    for(int d = 0; d < 4; d++)  // 0 - EN; 1 - NW; 2 - WN; 3 - NE
    {
        int r1 = r;
        int c1 = c;

        while(r1 >= 0 && r1 < N && c1 >= 0 && c1 < N)
        {
            if (r1 != r && c1 != c)
            {
                pos[r1, c1] += num;
            }
            r1 += dr[d];
            c1 += dC[d];
        }
    }
}
```

## 代码 - backtrace

```cssharp
public class Solution
{
    private IList<IList<string>> ans = new List<IList<string>>();
    private int N = 0;
    int[,] pos;
    public IList<IList<string>> SolveNQueens(int n)
    {
        N = n;
        pos = new int[N, N];
        backtrace(new List<string>(), 0);
        return ans;
    }

    private void backtrace(IList<string> graph, int r)
    {
        if (r == N)
        {
            List<string> solve = new List<string>();
            for (int i = 0; i < graph.Count; i++)
            {
                solve.Add(graph[i]);
            }
            ans.Add(solve);
            return;
        }

        for(int i = 0; i < N; i++)
        {
            if (pos[r,i] == 0)
            {
                string g = PrintPos(i);
                SetMatrix(r, i, 1);
                graph.Add(g);
                backtrace(graph, r + 1);
                graph.Remove(g);
                SetMatrix(r, i, -1);
            }
        }
    }

    private string PrintPos(int x)
    {
        char[] c = new char[N];
        for(int i = 0; i < N; i++)
        {
            c[i] = i == x ? 'Q' : '.';
        }
        return new string(c);
    }

    private void SetMatrix(int r, int c, int num)
    {
        pos[r, c] += num;

        // set vertical & horizon items
        for (int i = 0; i < N; i++)
        {
            if(i != c)
            {
                pos[r, i] += num; // set vertical direction
            }

            if (i != r)
            {
                pos[i, c] += num; // set horizontal direction
            }
        }

        int[] dr = new int[4] { 1, 1, -1, -1 };
        int[] dC = new int[4] { 1, -1, -1, 1 };

        for(int d = 0; d < 4; d++)  // 0 - EN; 1 - NW; 2 - WN; 3 - NE
        {
            int r1 = r;
            int c1 = c;

            while(r1 >= 0 && r1 < N && c1 >= 0 && c1 < N)
            {
                if (r1 != r && c1 != c)
                {
                    pos[r1, c1] += num;
                }
                r1 += dr[d];
                c1 += dC[d];
            }
        }
    }
}
```
