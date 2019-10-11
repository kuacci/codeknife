# [171. Excel Sheet Column Number](https://leetcode.com/problems/excel-sheet-column-number/)

Given a column title as appear in an Excel sheet, return its corresponding column number.

**For example:**

```text
    A -> 1
    B -> 2
    C -> 3
    ...
    Z -> 26
    AA -> 27
    AB -> 28
    ...
```

**Example 1:**

```text
Input: "A"
Output: 1
Example 2:

Input: "AB"
Output: 28
Example 3:

Input: "ZY"
Output: 701
```

## 思路 - 26 进制

这里是用字母`A` - `Z`来代表数字。`A` = 1, `Z` = 26, 所以相当于26进制。所以AA 相当于 1 * 26 ^ 1 + 1.

## 代码 - 26 进制

```csharp
public class Solution {
    public int TitleToNumber(string s) {
        if(string.IsNullOrEmpty(s)) return 0;

        char[] nums = s.ToCharArray();
        int dim = 26;
        int ans = 0;
        for(int i = nums.Length - 1; i >= 0; i--)
        {
            int d = nums.Length - 1 - i;
            ans += (int)((nums[i] - 'A' + 1) * Math.Pow(dim, d));
        }
        return ans;
    }
}
```

## 代码 - 26 进制 - 从前往后

```csharp
public class Solution {
    public int TitleToNumber(string s) {
        int Ans=0,buf=0;
        for(int i=0;i<s.Length;i++)
        {
            buf=s[i]-'A'+1;
            Ans=Ans*26+buf;
        }
        return Ans;
    }
}
```
