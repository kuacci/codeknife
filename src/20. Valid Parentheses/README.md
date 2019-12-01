# [Easy][20. Valid Parentheses](https://leetcode.com/problems/valid-parentheses/)

Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

An input string is valid if:

Open brackets must be closed by the same type of brackets.
Open brackets must be closed in the correct order.
Note that an empty string is also considered valid.

**Example 1:**

> Input: "()"
> Output: true

**Example 2:**

> Input: "()[]{}"
> Output: true

**Example 3:**

> Input: "(]"
> Output: false

**Example 4:**

> Input: "([)]"
> Output: false

**Example 5:**

> Input: "{[]}"
> Output: true

## 思路 - Stack + ASCII

利用括号的出现是成对的这一特点，使用栈来保存出现过的括号,如果`s[i]`与栈顶的括号配对，则弹出。最后看stack是否已经空了，如果是空则说明配对成功，反之失败。括号的配对可以利用他们的ASCII码.

```text
( - 40; ) - 41
[ - 91; ] - 93
{ - 123; } - 125
```

时间复杂度：O(N), string一次遍历. Stack的存取是O(1).
空间复杂度：O(N), 用Stack来保存s内容。

## 代码 - Stack + ASCII

```csharp
public class Solution {
    public bool IsValid(string s) {
        // ( - 40; ) - 41
        // [ - 91; ] - 93
        // { - 123; } - 125
        if(s.Length == 0) return true;

        Stack<char> stack = new Stack<char>();

        for(int i = 0; i < s.Length; i++)
        {
            if(stack.Count == 0)
            {
                stack.Push(s[i]);
                continue;
            }
            char peek = stack.Peek();
            if((int)peek == (int)s[i] - 1 || (int)peek == (int)s[i] - 2)
            {
                stack.Pop();
            }
            else
            {
                stack.Push(s[i]);
            }
        }

        return stack.Count == 0;
    }
}
```
