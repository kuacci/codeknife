# [Medium][890. Find and Replace Pattern](https://leetcode.com/problems/find-and-replace-pattern/)

You have a list of words and a pattern, and you want to know which words in words matches the pattern.

A word matches the pattern if there exists a permutation of letters p so that after replacing every letter x in the pattern with p(x), we get the desired word.

(Recall that a permutation of letters is a bijection from letters to letters: every letter maps to another letter, and no two letters map to the same letter.)

Return a list of the words in words that match the given pattern.

You may return the answer in any order.

Example 1:

```text
Input: words = ["abc","deq","mee","aqq","dkd","ccc"], pattern = "abb"
Output: ["mee","aqq"]
Explanation: "mee" matches the pattern because there is a permutation {a -> m, b -> e, ...}.
"ccc" does not match the pattern because {a -> c, b -> c, ...} is not a permutation,
since a and b map to the same letter.
```

Note:

* 1 <= words.length <= 50
* 1 <= pattern.length = words[i].length <= 20

## 思路

题目的要求是给出一组string， 判断 string的规律是否跟给定的pattern一致。我的想法是使用一个Dictionary<char,char> 来建立一张对应关系表。
例如 pattern ["abb"] 和 ["mee"]
第一个字符[a] 和 [m]，有对应关系。在Dictionary中保存 [a] --> [m].
第二个字符[b] 和 [e]，有对应关系，在Dictionary中保存 [b] --> [e].
第三个字符[b] 和 [e], 检测到Dictionary中[b]对应的字符是[e]， 则在遍历完整个string之后，返回true.

同样的，pattern["abb"] 和 ["abc"].
第三个字符中Dictionary中[b]对应的字符应该是[b]， 而第三个字符为[c]，那么pattern is not match. 返回false.

这里要注意的一个坑是[ccc]这样的string。 在添加对应关系的时候，必须判断value是否已经存在。否则可能会产生对各key对应一个value的情况，从而产生偏差。

## 代码

```csharp
public class Solution {
    public IList<string> FindAndReplacePattern(string[] words, string pattern) {

        List<string> ans = new List<string>();

        foreach(string word in words)
        {
            if(IsMatch(word, pattern))
                ans.Add(word);
        }

        return ans;

    }

    private bool IsMatch(string word, string pattern)
    {
        char[] ws = word.ToCharArray();
        char[] ps = pattern.ToCharArray();

        Dictionary<char,char> map = new Dictionary<char,char>();

        for(int i = 0; i < ws.Length; i++)
        {
            if(!map.ContainsKey(ps[i]))
            {
                if(map.ContainsValue(ws[i]))
                    return false;
                else
                    map.Add(ps[i],ws[i]);
            }
            else
            {
                if(map[ps[i]] != ws[i]) return false;
            }
        }

        return true;
    }
}
```
