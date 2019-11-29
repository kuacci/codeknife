# [Medium][516. Longest Palindromic Subsequence](https://leetcode.com/problems/longest-palindromic-subsequence/)

Given a string s, find the longest palindromic subsequence's length in s. You may assume that the maximum length of s is 1000.

**Example 1:**
> Input:
> "bbbab"
> Output:
> 4
> One possible longest palindromic subsequence is "bbbb".
you
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