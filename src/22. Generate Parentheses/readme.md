# [Medium][22. Generate Parentheses](https://leetcode.com/problems/generate-parentheses/)

Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.

For example, given n = 3, a solution set is:

```text
[
  "((()))",
  "(()())",
  "(())()",
  "()(())",
  "()()()"
]
```

## 思路 - 回溯算法

这道题目跟[17. Letter Combinations of a Phone Number](../17.%20Letter%20Combinations%20of%20a%20Phone%20Number)很类似. 要通过回溯算法遍历所有的可能性。区别点是：

1. 以'('开头。
2. '('和')'是成对出现的。
3. '('的数目不能少于')'。

这里使用一个`char[] dest`来暂存输出的结果，可以确定开头是'('，所有先将'('放入到dest中。
回溯算法依次先放入'('，然后再')'。如果有出现'('数目少于')'的情况就要退回。 `if(lpos < rpos) return;`
当'char[] dest'填充完毕之后输出到`IList<string> ans`中。

## 代码 - 回溯算法

```csharp
public class Solution {

    private IList<string> ans = new List<string>();

    public IList<string> GenerateParenthesis(int n) {

        if(n == 0) return ans;

        char[] dest = new char[2 * n];
        dest[0] ='(';
        helper(dest, n, 1, 0);
        return this.ans;
    }

    private void helper(char[] dest, int n, int lpos, int rpos)
    {
        if(lpos < rpos) return;
        if(lpos >= n && rpos >= n)
        {
            this.ans.Add(new string(dest));
            return;
        }

        if(lpos < n)
        {
            dest[lpos + rpos] = '(';
            helper(dest, n, lpos + 1, rpos);
        }
        if(rpos < n)
        {
            dest[lpos + rpos] = ')';
            helper(dest, n, lpos, rpos + 1);
        }

    }
}
```
