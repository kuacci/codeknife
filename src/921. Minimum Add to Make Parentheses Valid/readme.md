# 921. Minimum Add to Make Parentheses Valid

Given a string S of '(' and ')' parentheses, we add the minimum number of parentheses ( '(' or ')', and in any positions ) so that the resulting parentheses string is valid.

Formally, a parentheses string is valid if and only if:

* It is the empty string, or
* It can be written as AB (A concatenated with B), where A and B are valid strings, or
* It can be written as (A), where A is a valid string.

Given a parentheses string, return the minimum number of parentheses we must add to make the resulting string valid.

```text
Example 1:

Input: "())"
Output: 1
Example 2:

Input: "((("
Output: 3
Example 3:

Input: "()"
Output: 0
Example 4:

Input: "()))(("
Output: 4
```

Note:

> S.length <= 1000
> S only consists of '(' and ')' characters.

## 思路 - Stack

`(` 和 `)` 要成对的出现，那么就是valid. 如果先出现一个`(`，那么invalid的括号数量要+1. 还有一种可能性是`(`出现次数多，那么没有足够的`)`来匹配。那么就要算出来又多少个`(`.
我的想法是用一个Stack来记录出现过的`(`。当后面出现`)`的时候，从Stack中消去一个`(`。如果Stack是空的情况，那么要最后invalid数数量直接+1.

## 代码 - Stack

```csharp
public class Solution {
    public int MinAddToMakeValid(string S) {

        if(string.IsNullOrEmpty(S)) return 0;
        if(S.Length == 1) return 1;

        char[] pars = S.ToCharArray();
        Stack<char> ls = new Stack<char>();
        //Stack<char> rs = new Stack<char>();
        int count = 0;

        for(int i = 0; i < pars.Length; i++)
        {
            if(pars[i] == '(')
            {
                ls.Push(pars[i]);
            }
            else
            {
                if(ls.Count > 0)
                {
                    ls.Pop();
                }
                else
                {
                    count ++;
                }
            }
        }

        return ls.Count + count;

    }
}
```

## 思路 -- Count

上述的思路借助了Stack来暂存`(`。仔细观察以后发现Stack在这里的作用是类似于计数器。实际上，可以使用int来代替这个Stack的作用来作计数器。

## 代码 -- Count

```csharp
public class Solution {
    public int MinAddToMakeValid(string S) {

        if(string.IsNullOrEmpty(S)) return 0;
        if(S.Length == 1) return 1;

        char[] pars = S.ToCharArray();

        int lCount = 0;
        int rCount = 0;

        for(int i = 0; i < pars.Length; i++)
        {
            if(pars[i] == '(')
            {
                lCount++;
            }
            else
            {
                if(lCount > 0)
                {
                    lCount --;
                }
                else
                {
                    rCount ++;
                }
            }
        }

        return lCount + rCount;

    }
}
```
