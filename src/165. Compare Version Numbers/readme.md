# [165. Compare Version Numbers](https://leetcode.com/problems/compare-version-numbers/)

Compare two version numbers version1 and version2.
If version1 > version2 return 1; if version1 < version2 return -1;otherwise return 0.

You may assume that the version strings are non-empty and contain only digits and the . character.

The . character does not represent a decimal point and is used to separate number sequences.

For instance, 2.5 is not "two and a half" or "half way to version three", it is the fifth second-level revision of the second first-level revision.

You may assume the default revision number for each level of a version number to be 0. For example, version number 3.4 has a revision number of 3 and 4 for its first and second level revision number. Its third and fourth level revision number are both 0.

**Example 1:**

> Input: version1 = "0.1", version2 = "1.1"
> Output: -1

**Example 2:**

> Input: version1 = "1.0.1", version2 = "1"
> Output: 1

**Example 3:**

> Input: version1 = "7.5.2.4", version2 = "7.5.3"
> Output: -1

**Example 4:**

> Input: version1 = "1.01", version2 = "1.001"
> Output: 0
> Explanation: Ignoring leading zeroes, both “01” and “001" represent the same number “1”

**Example 5:**

> Input: version1 = "1.0", version2 = "1.0.0"
> Output: 0
Explanation: The first version number does not have a third level revision number, which means its third level revision number is default to "0"

Note:

> Version strings are composed of numeric strings separated by dots . and this numeric strings may have leading zeroes.
> Version strings do not start or end with dots, and they will not be two consecutive dots.

## 思路

这题是检查版本号的大小，比较典型的字符串处理。
主要的思路是分段的进行数字比较。一个指针指向一个段的起始地址，用另外一个指针向右寻找，直到遇到字串versin的结尾或者遇到'.'. 右侧的指针停止在version的最后或者'.'的前面。
用int.Parse来转成数字。最后是比较这个版本数字的大小。如果相等则进入下一轮的比较，直到2个version都走到底。或者返回的两个版本不一致。
这里要注意的点是：

1. 边界的处理。右侧指针e要停在最后一个合法的int字符上。
2. 下一次开始位置要设置为右侧指针e+2. e的后面可能是'.', start要停在'.'的第一个合法数字字符上。 那么下一个位置也有越界的可能性。如果越界了，说明已经走到version字符串的最后，所以要返回默认值0.

## 代码

```csharp
public class Solution {
    public int CompareVersion(string version1, string version2)
    {
        int startv1 = 0, startv2 = 0;
        char[] chsv1 = version1.ToCharArray();
        char[] chsv2 = version2.ToCharArray();

        while(startv1 < version1.Length || startv2 < version2.Length)
        {
            int ver1 = helper(chsv1, ref startv1);
            int ver2 = helper(chsv2, ref startv2);
            if (ver1 == ver2) continue;
            return ver1 > ver2 ? 1 : -1;
        }
        return 0;
    }

    private int helper(char[] chs, ref int start)
    {
        if (start > chs.Length - 1) return 0;

        int ans = 0;
        int s = start;
        int e = start;

        while (e < chs.Length - 1)
            if (chs[e + 1] == '.') break;
            else e++;

        if(e <= chs.Length - 1)
        {
            ans = int.Parse(new string(chs, s, e - s + 1));
            start = e + 2;
        }

        return ans;
    }
}
```
