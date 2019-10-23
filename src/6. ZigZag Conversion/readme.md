# [Medium][6. ZigZag Conversion](https://leetcode.com/problems/zigzag-conversion/)

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

按照题目的要求，先new 多个`List<char>`数组，然后从上往下将字母放到对应的`List<char>`中，放到底部之后，方向向上，直到顶部。如此往返直到字符串都填充完。

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

## 思路 - Visted by row

上面的思路是按照Z字回型的方式，但是有个缺点空间复杂度比较高。有没有更高效的方法呢。仔细观察一下，numRows和每个row保存的字符所在`char[]`的位置应该是有联系的。将输入的string，转换成一个`char[]`数组。研究一下numRows和`char[]`的位置关系。

当 numRows=1时候，直接输出。

![img](image\figure1.jpg)

当 numRows=2时，上下两列输出的`char[]`位置非常有规律，交替出现。通过观察numRow=2 和 numRow=3的情况，第一列和最后一列的`char[]`位置是有规律的。但是中间的列的规律还不是特别明亮。

![img](image\figure2.jpg)
![img](image\figure3.jpg)

继续增加样本，可以看出来更多的规律。

![img](image\figure4.jpg)

1. 第一列最后一列出现的位置间隔是 : 2 * numRow - 2
2. 第一列启示位置为0，最后一列起始位置为 numRow - 1
3. 中间的列位置间隔，每2个数的间隔之和与第一列/最后一列的间隔数相同。

找到间隔规律之后，思考一下可以得出他们的位置的计算公式。

1. row 0 : index =  2 * numRow - 2
2. row numRow - 1 : index = 2 * numRow - 2 + ( numRow - 1)
3. 有2个值交替出现 ： `index = 2 * numRow - 2 - i` 和 `index = 2 * numRow - 2 + i`

## 代码 - Visted by row

```csharp
public class Solution {
    public string Convert(string s, int numRows) {
        if (numRows == 1) return s;

        StringBuilder ret = new StringBuilder();
        char[] ch = s.ToCharArray();
        int n = ch.Length;
        int cycleLen = 2 * numRows - 2;

        for (int i = 0; i < numRows; i++) {
            for (int j = 0; j + i < n; j += cycleLen) {
                ret.Append(ch[j + i]);
                if (i != 0 && i != numRows - 1 && j + cycleLen - i < n)
                    ret.Append(ch[j + cycleLen - i]);
            }
        }
        return ret.ToString();
    }
}

```
