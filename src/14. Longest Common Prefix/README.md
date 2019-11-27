# [Easy][14. Longest Common Prefix](https://leetcode.com/problems/longest-common-prefix/)

Write a function to find the longest common prefix string amongst an array of strings.

If there is no common prefix, return an empty string "".

**Example 1:**

Input: ["flower","flow","flight"]
Output: "fl"

**Example 2:**

Input: ["dog","racecar","car"]
Output: ""
Explanation: There is no common prefix among the input strings.
Note:

All given inputs are in lowercase letters a-z.

## 思路

求最长的共同前缀。先将第一列string作为标准进行对比，后面的每一列跟上面第一列的第`i`个字符进行对比，如果都相等的情况下比较下一个字符`i + 1`. 否则就终止这个过程返回结果。

## 代码

```csharp
public class Solution {
    public string LongestCommonPrefix(string[] strs) {
        if(strs == null || strs.Length == 0) return string.Empty;
        if(strs.Length == 1) return strs[0];

        StringBuilder ans = new StringBuilder();
        for(int i = 0; i < strs[0].Length; i++)
        {
            for(int j = 1; j < strs.Length; j++)
            {

                if(i < strs[j].Length && strs[j][i] == strs[0][i]) continue;
                else return ans.ToString();
            }
            ans.Append(strs[0][i]);
        }

        return ans.ToString();
    }
}
```
