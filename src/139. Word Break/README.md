# [Medium][139. Word Break](https://leetcode.com/problems/word-break/)

Given a non-empty string s and a dictionary wordDict containing a list of non-empty words, determine if s can be segmented into a space-separated sequence of one or more dictionary words.

Note:

The same word in the dictionary may be reused multiple times in the segmentation.
You may assume the dictionary does not contain duplicate words.

**Example 1:**

```text
Input: s = "leetcode", wordDict = ["leet", "code"]
Output: true
Explanation: Return true because "leetcode" can be segmented as "leet code".
```

**Example 2:**

```text
Input: s = "applepenapple", wordDict = ["apple", "pen"]
Output: true
Explanation: Return true because "applepenapple" can be segmented as "apple pen apple".
             Note that you are allowed to reuse a dictionary word.
```

**Example 3:**

```text
Input: s = "catsandog", wordDict = ["cats", "dog", "sand", "and", "cat"]
Output: false
```

## 思路 - backtrack

比较暴力的解法。通过字典表查询s中是否存在某个word. 如果有的话，从s中替换掉这个word,将其替换为空字符。一直递归，直到s所有的字符都被替换成为空则返回true， 否则返回false.

## 代码 - backtrack

```csharp
public class Solution {
    public bool WordBreak(string s, IList<string> wordDict)
    {
        return WordBreakBackTrack(s, wordDict, 0);
    }

    private bool WordBreakBackTrack(string s, IList<string> wordDict, int pos)
    {
        if (string.IsNullOrEmpty(s.Trim())) return true;
        if (pos > wordDict.Count) return false;

        for (int i = pos; i < wordDict.Count; i++)
        {
            int index = s.IndexOf(wordDict[i]);
            if (index > -1)
            {
                char[] chs = s.ToCharArray();
                for (int j = 0; j < wordDict[i].Length; j++)
                    chs[index + j] = ' ';

                bool valid = WordBreakBackTrack(new string(chs), wordDict, pos);
                if (valid) return true;
            }
        }
        return false;
    }
}
```
