# [Medium][12. Integer to Roman](https://leetcode.com/problems/integer-to-roman/)

Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.

> Symbol       Value
> I             1
> V             5
> X             10
> L             50
> C             100
> D             500
> M             1000

For example, two is written as II in Roman numeral, just two one's added together. Twelve is written as, XII, which is simply X + II. The number twenty seven is written as XXVII, which is XX + V + II.

Roman numerals are usually written largest to smallest from left to right. However, the numeral for four is not IIII. Instead, the number four is written as IV. Because the one is before the five we subtract it making four. The same principle applies to the number nine, which is written as IX. There are six instances where subtraction is used:

* I can be placed before V (5) and X (10) to make 4 and 9.
* X can be placed before L (50) and C (100) to make 40 and 90.
* C can be placed before D (500) and M (1000) to make 400 and 900.
Given an integer, convert it to a roman numeral. Input is guaranteed to be within the range from 1 to 3999.

**Example 1:**

> Input: 3
> Output: "III"

**Example 2:**

> Input: 4
> Output: "IV"

**Example 3:**

> Input: 9
> Output: "IX"

**Example 4:**

> Input: 58
> Output: "LVIII"
> Explanation: L = 50, V = 5, III = 3.

**Example 5:**

> Input: 1994
> Output: "MCMXCIV"
> Explanation: M = 1000, CM = 900, XC = 90 and IV = 4.

## 思路 - 1

阿拉伯数字到罗马数字有几个特别的地方，从1 - 9要分为3种情况。

1. 9， 相当于 10 -1. 如 9 = IX
2. 5 ~ 8，相当于 5 + n个1. 如 8 = VIII
3. 4, 相当于 5 - 1， 如 4 = IV
4. 1 ~ 3， 相当于 n个1， 如 3 = III

那么用代码分别标识出来就可以了。

时间复杂度：O(1), 由于给定的数字是1 ~ 3999， 这个级别的数字是常数时间就可以完成的，所以是O(1).
空间复杂度：O(1)

## 代码 - 1

```csharp
public class Solution {
    public string IntToRoman(int num)
    {
        StringBuilder ans = new StringBuilder();
        num = ConvertToRoman(ans, num, 1000, "", "", "M");
        num = ConvertToRoman(ans, num, 100, "M", "D", "C");
        num = ConvertToRoman(ans, num, 10, "C", "L", "X");
        num = ConvertToRoman(ans, num, 1, "X", "V", "I");

         return ans.ToString();
    }

    private int ConvertToRoman(StringBuilder ans, int num, int dim, string sym0, string sym1, string sym2)
    {
        if (num < dim) return num;

        int n = num / dim;
        if (n == 9)
        {
            ans.Append(sym2 + sym0);
        }
        else if (n >= 5)
        {
            ans.Append(sym1);

            for (int i = 6; i <= n; i++)
            {
                ans.Append(sym2);
            }
        }
        else if (n == 4)
        {
            ans.Append(sym2 + sym1);
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                ans.Append(sym2);
            }
        }
        num = num % dim;

        return num;
    }
}
```
