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
```text

## Source Code

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
