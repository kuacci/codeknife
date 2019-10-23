# [Medium][151. Reverse Words in a String](https://leetcode.com/problems/reverse-words-in-a-string/)

Given an input string, reverse the string word by word.

**Example 1:**

> Input: "the sky is blue"
> Output: "blue is sky the"

**Example 2:**

> Input: "  hello world!  "
> Output: "world! hello"
> Explanation: Your reversed string should not contain leading or trailing spaces.

**Example 3:**

> Input: "a good   example"
> Output: "example good a"
> Explanation: You need to reduce multiple spaces between two words to a single space in the reversed string.

Note:

> A word is defined as a sequence of non-space characters.
> Input string may contain leading or trailing spaces. However, your reversed string should not contain leading or trailing spaces.
> You need to reduce multiple spaces between two words to a single space in the reversed string.

Follow up:

For C programmers, try to solve it in-place in O(1) extra space.

## 思路 - StringBuilder

这道题目的要求是将整个单词进行翻转，单词之间由`' '`来间隔。
为了达到翻转的效果，先将s用`' '`来分拆成为string[]. 这样就知道由多个个单词。用string.Trim()方法可以取出头尾的`' '`。同时在翻转的时候，判断是否是`' '`，如果是的话就跳过。
为了提高string拼接的效率，使用StringBuilder.

假设 s里面包含了N个单词，整个算法将N个都次都走了一遍，时间复杂度O(N)。
使用`string[]` 保存了分拆后的s整个内容。使用StringBuidler，保存的是`string[]`中对应元素的地址，没有增加太多的内存开销。空间复杂度 O(N).

## 代码 - StringBuilder

```csharp
public class Solution {
    public string ReverseWords(string s)
    {
        if (string.IsNullOrEmpty(s.Trim())) return "";
        string[] ss = s.Split(" ");
        StringBuilder asn = new StringBuilder();
        for (int i = ss.Length - 1; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(ss[i].Trim()))
                asn.Append(ss[i] + " ");
        }
        asn.Remove(asn.Length - 1, 1);
        return asn.ToString();
    }
}
```

## 思路 - 双指针

上面是借助了Split分拆成string[], 如果在通过遍历string的方式也能完成。不过需要借助辅助`List<char>`。
从string后方开始遍历，第一个指针用来标识单词的结尾。当遇到第一个非' '字符的时候，就是单词的结尾。这个时候用另外一个指针，从i的位置开始走到下一个' '. 这里是作为一个单词的开头。由于判断条件是`if (s[pos] == ' ') break;` 所以此时pos指的是' '。这就是为什么开始赋值`char`的时候，要从`pos + 1`位置开始。

排除多余' '的方法，是通过挪动i来完成了。

## 代码 - 双指针

```csharp
public class Solution {
    public string ReverseWords(string s)
    {
        if (string.IsNullOrEmpty(s.Trim())) return "";

        List<char> chs = new List<char>();

        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == ' ') continue;
            int pos = i;
            for (; pos >= 0; pos--)
            {
                if (s[pos] == ' ') break;
            }

            for (int l = pos + 1; l <= i; l++)
            {
                chs.Add(s[l]);
            }

            i = pos;
            chs.Add(' ');
        }
        if (chs.Count > 0) chs.RemoveAt(chs.Count - 1);

        return new string(chs.ToArray());
    }
}
```

## 思路 - 双指针 - StringBuilder

用StringBuild 代替`List<char>`.

## 代码 - 双指针 - StringBuilder

```charp
public class Solution {
    public string ReverseWords(string s)
    {
        if (string.IsNullOrEmpty(s.Trim())) return "";

        StringBuilder ans = new StringBuilder();

        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == ' ') continue;
            int pos = i;
            for (; pos >= 0; pos--)
            {
                if (s[pos] == ' ') break;
            }

            ans.Append(s, pos + 1, i - pos);
            ans.Append(' ');
            i = pos;

        }
        if (ans.Length > 0) ans.Remove(ans.Length - 1, 1);

        return ans.ToString();
    }
}
```
