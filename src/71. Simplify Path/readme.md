# [Medium][71. Simplify Path](https://leetcode-cn.com/problems/simplify-path/)

Given an absolute path for a file (Unix-style), simplify it. Or in other words, convert it to the canonical path.

In a UNIX-style file system, a period . refers to the current directory. Furthermore, a double period .. moves the directory up a level. For more information, see: Absolute path vs relative path in Linux/Unix

Note that the returned canonical path must always begin with a slash /, and there must be only a single slash / between two directory names. The last directory name (if it exists) must not end with a trailing /. Also, the canonical path must be the shortest string representing the absolute path.

**Example 1:**

> Input: "/home/"
> Output: "/home"
> Explanation: Note that there is no trailing slash after the last directory name.

**Example 2:**

> Input: "/../"
> Output: "/"
> Explanation: Going one level up from the root directory is a no-op, as the root level is the highest level you can go.

**Example 3:**

> Input: "/home//foo/"
> Output: "/home/foo"
> Explanation: In the canonical path, multiple consecutive slashes are replaced by a single one.

Example 4:

> Input: "/a/./b/../../c/"
> Output: "/c"

**Example 5:**

> Input: "/a/../../b/../c//.//"
> Output: "/c"

**Example 6:**

> Input: "/a//b////c/d//././/.."
> Output: "/a/b/c"

## 思路 - Stack

这道题目题目主要是对'/'的处理。遇到'/'的时候有几种可能性:

1. 前面没有值，先记录'/'.
2. 前面的字符是'/', 忽略当前的'/'.
3. 前面已经有值了，这个值是合法的，压入栈。
4. 前面已经有值了，这个值是'/.', 忽略，重新来一遍。
5. 前面已经有值了，这个值是'/..', 弹出（如果栈不为空的话).

Stack是用来记录每一层的内容，以'/'开头，字母为盘符。

## 代码 - Stack

```csharp
public class Solution {
    public string SimplifyPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return string.Empty;

        StringBuilder ans = new StringBuilder();

        Stack<string> stack = new Stack<string>();
        List<char> tmp = new List<char>();
        string part = "";
        char[] chs = path.ToCharArray();
        tmp.Add('/');

        for (int i = 1; i < chs.Length; i++)
        {
            if (chs[i] == '/')
            {
                if (tmp.Count == 0) tmp.Add(chs[i]);
                else if (tmp[tmp.Count - 1] == '/') continue;   // remove duplicate "//"
                else
                {
                     part = new string(tmp.ToArray());

                    if (part == "/..")  // move to upper lvl
                    {
                        if(stack.Count > 0) stack.Pop();
                    }
                    else if (part != "/.")   // ignore "/."
                    {
                        stack.Push(part);
                    }
                    tmp.Clear();
                    tmp.Add('/');
                }
            }
            else
                tmp.Add(chs[i]);
        }
        part = new string(tmp.ToArray());
        if (part == "/..")
        {
            if (stack.Count > 0) stack.Pop();
        }
        else if (part != "/.")
        {
            stack.Push(part);
        }

        if (stack.Count > 0 && stack.Peek() == "/")
            stack.Pop();

        string[] res = stack.ToArray();
        for (int i = res.Length - 1; i >= 0; i--)
            ans.Append(res[i]);

        return ans.Length == 0 ? "/" : ans.ToString();
    }
}
```
