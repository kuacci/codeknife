# 6. ZigZag Conversion

The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows like this: (you may want to display this pattern in a fixed font for better legibility)

> P   A   H   N
> A P L S I I G
> Y   I   R

And then read line by line: "`PAHNAPLSIIGYIR`"

Write the code that will take a string and make this conversion given a number of rows:

> string convert(string s, int numRows);

**Example 1:**

> Input: s = "PAYPALISHIRING", numRows = 3
> Output: "PAHNAPLSIIGYIR"

**Example 2:**

> Input: s = "PAYPALISHIRING", numRows = 4
> Output: "PINALSIGYAHRPI"
> Explanation:
>
> P     I    N
> A   L S  I G
> Y A   H R
> P     I

## 思路 - Sort By Row

按照题目的要求，先new 多个List<char>数组，然后从上往下将字母放到对应的List<char>中，放到底部之后，方向向上，直到顶部。如此往返直到字符串都填充完。

## 代码 - Sort By Row

```csharp
public class Solution
    {
        public string Convert(string s, int numRows)
        {

            if (numRows == 1) return s;

            List<char>[] zlst = new List<char>[numRows];
            for (int i = 0; i < zlst.Length; i++)
            {
                zlst[i] = new List<char>();
            }

            bool revert = false;

            char[] chs = s.ToCharArray();
            int r = 0;
            int top = 0;
            int button = numRows - 1;

            for (int i = 0; i < chs.Length; i++)
            {
                zlst[r].Add(chs[i]);

                if (revert)  // from button to top
                {
                    if (r == top)
                    {
                        r++;
                        revert = false;
                    }
                    else
                    {
                        r--;
                    }
                }
                else
                {
                    if (r == button)
                    {
                        r--;
                        revert = true;
                    }
                    else
                    {
                        r++;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < numRows; i++)
            {
                sb.Append(zlst[i].ToArray());
            }
            return sb.ToString();
        }
    }
```
