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
