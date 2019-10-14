# [763. Partition Labels](https://leetcode.com/problems/partition-labels/)

A string S of lowercase letters is given. We want to partition this string into as many parts as possible so that each letter appears in at most one part, and return a list of integers representing the size of these parts.

Example 1:

```text
Input: S = "ababcbacadefegdehijhklij"
Output: [9,7,8]
Explanation:
The partition is "ababcbaca", "defegde", "hijhklij".
This is a partition so that each letter appears in at most one part.
A partition like "ababcbacadefegde", "hijhklij" is incorrect, because it splits S into less parts.
```

Note:

```text
1. S will have length in range [1, 500].
2. S will consist of lowercase letters ('a' to 'z') only.
```

## 思路

题目要求是按照重复的字符串将string进行分段。 重复的字符只会出现再一个分段中。例如“ababcbaca”中的任何以恶字符都不会出现再其他分段中。

由于这个特点，我的想法是先创建一个Dictionary用来记录出现的字符结束的位置。

然后扫描这个string的每一个字符。同时跟当前出现的字符的结束位置做比较。当最远的结束位置与当前的i相等的时候进行分段。

## 代码

```csharp
public class Solution {
    public IList<int> PartitionLabels(string S) {
        IList<int> part = new List<int>();
        if(string.IsNullOrEmpty(S)) return part;
        char[] ch = S.ToCharArray();
        Dictionary<char,int> charPos = InitCharPos(ch);

        int start = 0;
        int end = 0;

        for(int i = 0; i < ch.Length; i++)
        {
            end = Math.Max(end, charPos[ch[i]]);

            if(i == end)
            {
                part.Add(end - start + 1);
                start = Math.Min(i + 1, ch.Length - 1);
            }
        }

        return part;
    }

    private Dictionary<char,int> InitCharPos(char[] ch)
    {
        Dictionary<char,int> charPos = new Dictionary<char,int>();

        for(int i = 0; i < ch.Length; i++)
        {
            if(charPos.ContainsKey(ch[i]))
                charPos[ch[i]] = i;
            else
                charPos.Add(ch[i],i);
        }

        return charPos;
    }
}
```
