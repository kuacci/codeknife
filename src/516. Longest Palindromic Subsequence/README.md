# [Medium][516. Longest Palindromic Subsequence](https://leetcode.com/problems/longest-palindromic-subsequence/)

Given a string s, find the longest palindromic subsequence's length in s. You may assume that the maximum length of s is 1000.

**Example 1:**
> Input:
> "bbbab"
> Output:
> 4
> One possible longest palindromic subsequence is "bbbb".

**Example 2:**
> Input:
> "cbbd"
> Output:
> 2
> One possible longest palindromic subsequence is "bb".

## 思路 - dp

本题的要求是计算最长的回文子序列的长度，中间如果某些字符不是回文子序列的一部分的，可以跳过。例如例子中`bbbab`的字符`a`，中断了回文字符串`bbbb`,可以跳过这个字符`a`，从而得到一个最长的字符串. 这种可以跳过的叫做子序列`subsequence`, 要作为一个整体不能跳过的叫做子串`substring`.

处理这种问题的时候，要把复杂的问题分解问比较简单的小问题。

1. 单个字符。单个字符的时候，本身就是回文字。
2. 双字符。两个字符的时候，如果左右两个字符相等，那么就是回文字，否则就不是。
3. 三个字符。从3个字符开始情况就开始增多了。'aba', 'aab', 'abb'.
   * `aab`和`abb`的两种情况可以总结为一种。他的可以分为一个单字节+一个双字节, 即 `a` + `bb`. 分割的时候可以把左边的单字节也可以把右边的当成单字节。
   * 'aba' 这种情况。他是一个回文字且他的长为3.达成这种情况的条件是`s[left] == s[right]`.
4. 多个字符。多个字符的时候是相当于三个字符的升级版，也是分为2种情况，**两头相等**的和**两头不相等的**。
   * 两头不等的情况：`a***b`. 既然是左右两边不相等，就要看各走一变看看是不是有组成回文的可能性，用右边作比较，左边向右揭开一个`*`. `ab**b`, 比较这里是不是回文的可能性，计算回文的长度。同样用左边进行比较，右向左揭开一个`*`. `a**ab`, 比较这里是不是回文的可能性，计算回文的长度。
   * 两头相等的情况：`a***a`. 既然是两头相等，回文的长度+2， 那么找到之前`***`所记录的回文的长度+2就可以了。

所有总结出来规律：

1. 需要用一个`dp[][] table`记录曾经遍历过的字符串的回文的长度。用`left`来表示子序列的左下标，用`right`来表示子序列的右下标.`dp[left][right]`记录的是left到right之间的回文的长度。
2. 初始值为l`eft = right`， 即子序列为单个字符的情况,回文长度为1.`dp[left][right] = 1;// (left == right)`.
3. 两个以上的字符的公式为：

时间复杂度：O(N^2)，循环两层嵌套。
空间复杂度：O(N^2),用dp table记录了每次运算的结果。

```csharp
if(s[left] == s[right])
{
    dp[left][right] = dp[left + 1][right - 1] + 2;
}
else
{
    dp[left][right] = Math.Max(dp[left][right - 1], dp[left + 1][right]);
}
```

整个过程的visualization可以[参考这里](https://docs.google.com/presentation/d/1KhxVVgI8jzc-g7unDNKFiHY6XDNVSK6LNsadxB14K3U/edit?usp=sharing)。

## 代码 - dp

```csharp
public class Solution {
    public int LongestPalindromeSubseq(string s) {
        if(s.Length == 0) return 0;
        int N = s.Length;
        int[][] dp = new int[N][];
        for(int i = 0; i < N; i++)
        {
            dp[i] = new int[N];
        }

        for(int Len = 1; Len <= N; Len++)
        {
            for(int left = 0; left <= N - Len; left ++)
            {
                int right = Len + left - 1;
                if(left == right)
                {
                    dp[left][right] = 1;
                    continue;
                }

                if(s[left] == s[right])
                {
                    dp[left][right] = dp[left + 1][right - 1] + 2;
                }
                else
                {
                    dp[left][right] = Math.Max(dp[left][right - 1], dp[left + 1][right]);
                }
            }
        }

        return dp[0][N - 1];
    }
}
```

## 思路 - dp - 优化空间复杂度O(N)

从上面的计算可以得出一个规律，能用到的数组只有3列。

1. 当前长度`Len = l`，正在运算并且赋值的一列,相当于`dp[left][right]`。
2. 比当前长度少1的一列，`Len = l - 1`。这个是在左右两个不等的情况下，取左边进一格以及右边退一格的两种情况的最大值。即：`dp[left][right] = Math.Max(dp[left][right - 1], dp[left + 1][right]);`
3. 比当前长度少2的一列，`Len = 1 -2`。这个是在左右两边相等的情况下，左右向中间逼近一格的子序列，然后加上两边的长度。即：`dp[left][right] = dp[left + 1][right - 1] + 2;`

为了降维，定义3个数组：

``` csharp
// replace
// int[][] dp = new int[N][];

int[] dp0 = new int[N]; // Len = l
int[] dp1 = new int[N]; // Len = l - 1
int[] dp2 = new int[N]; // Len = l - 2
```

计算的过程被替换为下面的代码。因为right是由left计算得来的，`int right = Len + left - 1;`，这里可以使用left来表示rigth. 所有在替换的过程中不需要给right增加下标了。

```csharp
if(s[left] == s[right])
{
    dp0[left] = dp2[left + 1] + 2;
}
else
{
    dp0[left] = Math.Max(dp1[left], dp1[left + 1]);
}
//if(s[left] == s[right])
//{
//    dp[left][right] = dp[left + 1][right - 1] + 2;
//}
//else
//{
//    dp[left][right] = Math.Max(dp[left][right - 1], dp[left + 1][right]);
//}

```

完成一轮计算之后，要将结果进行交互。当前计算的dp0，在完成这一轮循环后，将变成dp1,因为他以及不再是当前一行了，而是`Len = l - 1`. 同理，现在的dp1，下一轮要交换到`dp2`. `dp0`将会重新初始化。不过考虑到下一轮的计算会把`dp0`重新计算一次，`dp0`这个时候怎么赋值都可以。

```csharp
int[] tmp = dp2;
dp2 = dp1;
dp1 = dp0;
dp0 = tmp;
```

## 代码 - dp - 优化空间复杂度O(N)

```csharp
public class Solution {
    public int LongestPalindromeSubseq(string s) {
        if(s.Length == 0) return 0;
        int N = s.Length;
        int[] dp0 = new int[N];
        int[] dp1 = new int[N];
        int[] dp2 = new int[N];

        for(int Len = 1; Len <= N; Len++)
        {
            for(int left = 0; left <= N - Len; left ++)
            {
                int right = Len + left - 1;
                if(left == right)
                {
                    dp0[left] = 1;
                    continue;
                }

                if(s[left] == s[right])
                {
                    dp0[left] = dp2[left + 1] + 2;
                }
                else
                {
                    dp0[left] = Math.Max(dp1[left], dp1[left + 1]);
                }
            }
            int[] tmp = dp2;
            dp2 = dp1;
            dp1 = dp0;
            dp0 = tmp;
        }

        return dp1[0];
    }
}
```
