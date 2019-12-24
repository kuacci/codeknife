# [Medium][17. Letter Combinations of a Phone Number](https://leetcode.com/problems/letter-combinations-of-a-phone-number/)

Given a string containing digits from 2-9 inclusive, return all possible letter combinations that the number could represent.

A mapping of digit to letters (just like on the telephone buttons) is given below. Note that 1 does not map to any letters.

![image](image/200px-Telephone-keypad2.svg.png)

**Example:**

> Input: "23"
> Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].

**Note:**

Although the above answer is in lexicographical order, your answer could be in any order you want.

## 思路 - 回溯算法

键盘上的数字key，字母作为键值，保存在一个`Dictionary<int, char[]>`中。传入的string转换成数组，依次从里面取出数字，在Dictionary中到对应的char[]数组。用回溯算法遍历所有的可能性，输出到ans中。

## 代码 - 回溯算法

```csharp
public class Solution {

    private Dictionary<int, char[]> dic = new Dictionary<int, char[]>();
    private IList<string> ans = new List<string>();

    public IList<string> LetterCombinations(string digits) {
        if(string.IsNullOrEmpty(digits)) return ans;
        BuildDictionary();
        char[] source = digits.ToCharArray();
        char[] dest = new char[source.Length];

        helper(source, dest, 0);

        return ans;
    }

    private void helper(char[] source, char[] dest, int index)
    {
        if(index >= dest.Length)
        {
            ans.Add(new string(dest));
            return;
        }

        int key = source[index] - '0';
        foreach(var c in dic[key])
        {
            dest[index] = c;
            helper(source, dest, index + 1);
        }
    }

    private void BuildDictionary()
    {
        dic.Add(2, new char[] { 'a', 'b', 'c' });
        dic.Add(3, new char[] { 'd', 'e', 'f' });
        dic.Add(4, new char[] { 'g', 'h', 'i' });
        dic.Add(5, new char[] { 'j', 'k', 'l' });
        dic.Add(6, new char[] { 'm', 'n', 'o' });
        dic.Add(7, new char[] { 'p', 'q', 'r', 's' });
        dic.Add(8, new char[] { 't', 'u', 'v' });
        dic.Add(9, new char[] { 'w', 'x', 'y', 'z' });
    }
}
```

## 思路 - 回溯 - 优化

上述的回溯还存在可以优化的地方。原先使用的是Dicionary的方式存储数字键盘的内容。考虑到输入的数字本来就可以作为索引。所以可以用`string[]`来代替`Dictioary`，用digits某一位的数字来做索引。用这个语句进行数字字符到索引的转换`int c = digits[pos] - '0' - 2;`.例如`abc`存储在`string[]`的0位上。当digits内有个字符的数字是2的时候可以用`'2' - '0' - 2;`转换出索引是'0'.
回溯的思路跟上面基本一致了。

## 代码 - 回溯 - 优化

```csharp
public class Solution {
    string[] map = new string[] { "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
    public IList<string> LetterCombinations(string digits) {
        IList<string> ans = new List<string>();
        if(digits?.Length == 0) return ans;
        Backtrace(ans, digits, 0, new char[digits.Length]);
        return ans;
    }

    private void Backtrace(IList<string> ans, string digits, int pos, char[] chs)
    {
        if(pos == digits.Length)
        {
            ans.Add(new string(chs));
        }
        else
        {
            int c = digits[pos] - '0' - 2;
            for(int i = 0; i < map[c].Length; i++)
            {
                chs[pos] = map[c][i];
                Backtrace(ans, digits, pos + 1, chs);
            }
        }
    }
}
```
