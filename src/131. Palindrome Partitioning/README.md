# [Medium][131. Palindrome Partitioning](https://leetcode.com/problems/palindrome-partitioning/)

Given a string s, partition s such that every substring of the partition is a palindrome.

Return all possible palindrome partitioning of s.

Example:

Input: "aab"
Output:
[
  ["aa","b"],
  ["a","a","b"]
]

## 思路 - backtrack

这里是返回s中各种回文的可能分布。已知单个字符是回文。当`aab`按照单个字符进行分割的时候，有3个回文字符`["a","a","b"]`.
按照2个字符分割的时候，`aab`有几种分割字符`["aa","b"]`和`["a","ab"]`. 显然后面这种不是回文，只有一种是成功的。
按照3个字符来分割的时候，`["aab"]`则不是回文，被排除。

演算完之后发现这种方式比较合适使用backtrack。

## 代码 - backtrack

```csharp
public class Solution {
    public IList<IList<string>> Partition(string s) {

        IList<IList<string>> ans = new List<IList<string>>();
        if(s.Length == 0) return ans;
        PartitionBackTrack(ans, new List<string>(), s, 0);
        return ans;
    }

    public void PartitionBackTrack(IList<IList<string>> ans, IList<string> pal, string s, int left)
    {
        if(left == s.Length)
        {
            List<string> p = new List<string>();
            p.AddRange(pal);
            ans.Add(p);
        }
        else
        {
            for(int right = left; right < s.Length; right++)
            {
                if(IsPalindorme(s, left, right))
                {
                    pal.Add(s.Substring(left, right - left + 1));
                    PartitionBackTrack(ans, pal, s, right + 1);
                    pal.RemoveAt(pal.Count - 1);
                }
            }
        }
    }

    private bool IsPalindorme(string s, int left, int right)
    {
        while(left <= right)
        {
            if(s[left] != s[right]) return false;
            left ++;
            right --;
        }
        return true;
    }
}
```
