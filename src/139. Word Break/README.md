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

## 思路 - backtrack - 2

上面的思路创建了太多的string, 对于空间复杂度的要求比较高。可以用双指针的方式，传入的pos作为起始的指针，然后计算偏移量i，如果pos和i之间的word存在于dictionary中，则进行递归。

时间复杂度：O(N^N). 最坏的情况下如string = `"aaaaaaab"`, `["a","aa","aaa","aaaa",...]`, 每走一遍都要从"a"开始分隔，一个的尝试N遍。
空间复杂度：O(N). 递归最深进入N层。

## 代码 - backtrack - 2

```csharp
public class Solution {
    public bool WordBreak(string s, IList<string> wordDict)
    {
        return WordBreakBackTrack(s, wordDict, 0);
    }

    private bool WordBreakBackTrack(string s, IList<string> wordDict, int pos)
    {
        if (pos == s.Length) return true;

        for (int i = pos; i < s.Length; i++)
        {
            if(wordDict.Contains(s.Substring(pos, i - pos + 1)) && WordBreakBackTrack(s, wordDict, i + 1))
            {
                return true;
            }
        }
        return false;
    }
}
```

## 思路 - backtrack + memo

上面的思路中，有些位置的拆分是重复进行的。举个例子："aaab", ["a","aa","aaa","aaaa"].
由于i的步进是1，第一遍被分割的结果是
a|a|a|b
第二遍的分割结果
aa|a|a|b
第三遍
aaa|b
.....
可以看出来第一遍遍历的时候，后面几个分割位置，在第二遍喝第三遍的遍历中也重复出现过。可以将这些位置的结果记录下来。可以避免同一个位置的多次计算。为了达到这个目的，需要用一个`Dictionary<int, bool> memo`来记录子问题的结果。int是分割的位置，bool是当前这个位置的结果。

时间复杂度：O(N^2), 递归树的最大规模是N^2.
空间复杂度：O(N)，分割的位置最多为N.

## 代码 - backtrack + memo

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
