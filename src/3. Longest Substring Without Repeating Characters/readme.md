# [Medium][3. Longest Substring Without Repeating Characters](https://leetcode.com/problems/longest-substring-without-repeating-characters/)

Given a string, find the length of the longest substring without repeating characters.

Example 1:

```text
Input: "abcabcbb"
Output: 3
Explanation: The answer is "abc", with the length of 3.
```

Example 2:

```text
Input: "bbbbb"
Output: 1
Explanation: The answer is "b", with the length of 1.
```

Example 3:

```text
Input: "pwwkew"
Output: 3
Explanation: The answer is "wke", with the length of 3.
             Note that the answer must be a substring, "pwke" is a subsequence and not a substring.
```

## 思路 - 暴力破解

这个是效率比较差的方法，从左边开始扫描数组，将已经扫描过的char放到一个List中，往右移动的时候检查是否已经被记录过。如果没有则继续。如果已经过，截断子串，左边的指针往右移动一个。

## 代码 - Brute Force

```csharp
public class Solution {
    public int LengthOfLongestSubstring(string s) {
        int max = 0;
        int len = 0;
        char[] ch = s.ToCharArray();

        for(int i = 0; i < ch.Length; i++)
        {
            max = Math.Max(max,  LenghtOfLongest(ch, i));
        }
        return max;
    }

    private int LenghtOfLongest(char[] ch, int pos)
    {
        List<char> recorder = new List<char>();
        recorder.Add(ch[pos]);
        for(int i = pos + 1; i < ch.Length; i++)
        {
            if(recorder.Contains(ch[i])) break;
            recorder.Add(ch[i]);
        }
        return recorder.Count;
    }
}
```

## 思路 - sliding window

同样需要记录以及读取过的char.但是当遇到重复的char的时候，并不需要将右侧的指针移动到最左端。因为已经验证过，移动过的char内没有重复的，所有只需要将左侧的指针移动一个，继续进行比较即可。

## 代码 - sliding window

```csharp
public class Solution {
    public int LengthOfLongestSubstring(string s)
    {
        int max = 0;
        int l = 0;
        int r = 0;
        char[] ch = s.ToCharArray();
        List<char> clst = new List<char>();

        while (l < ch.Length && r < ch.Length)
        {
            if (!clst.Contains(ch[r]))
            {
                clst.Add(ch[r++]);
                max = Math.Max(max, r - l);
            }
            else
            {
                clst.Remove(ch[l++]);
            }
        }
        return max;
    }
}
```

## 思路 - 优化 sliding window

sliding window的方式还有可以改进的空间。当遇到重复的字符的时候，l并不需要一步一步的增加。而是可以一次跳到重复的字符所在位置的+1.

![img](image/1.jpg)

如何记录重复字符所出现的位置，可以使用一个Dictionary来记录字符出现时候，r所在的位置。因为需要l移动到重复字符的下一个位置，所以要存储的位置是r+1.

![img](image/2.jpg)

如果是为了记录字符所在的位置。比Dictionary更加高效的方式是可以用一个int数组。用一个int[128]来对应字符的ASCII码。

![img](image/3.jpg)

## 代码 - sliding window [Dictionary]

```csharp
public class Solution {
    public int LengthOfLongestSubstring(string s)
    {
        int max = 0;

        char[] ch = s.ToCharArray();
        Dictionary<char, int> cpos = new Dictionary<char, int>();

        for(int l = 0, r = 0; r < s.Length; r++)
        {
            if(cpos.ContainsKey(ch[r]))
            {
                l = l > cpos[ch[r]] ? l : cpos[ch[r]];
            }

            max = max > r - l + 1 ? max : r - l + 1;
            cpos[ch[r]] = r + 1;

        }

        return max;
    }
}
```

## 代码 - sliding window [int arrar]

```csharp
public class Solution {
    public int LengthOfLongestSubstring(string s)
    {
        int max = 0;

        char[] ch = s.ToCharArray();
        int[] index = new int[128];


        for(int l = 0, r = 0; r < s.Length; r++)
        {

            l = l > index[ch[r]] ? l : index[ch[r]];
            max = max > r - l + 1 ? max : r - l + 1;
            index[ch[r]] = r + 1;

        }

        return max;
    }
}
```
