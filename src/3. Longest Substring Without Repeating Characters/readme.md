# 3. Longest Substring Without Repeating Characters

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
            len = LenghtOfLongest(ch, i);
            max = max > len ? max : len;
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
                int len = r - l;
                max = max > len ? max : len;
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
