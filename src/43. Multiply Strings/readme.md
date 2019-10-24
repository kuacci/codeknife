# [Medium][43. Multiply Strings](https://leetcode.com/problems/multiply-strings/)

Given two non-negative integers num1 and num2 represented as strings, return the product of num1 and num2, also represented as a string.

**Example 1:**

> Input: num1 = "2", num2 = "3"
> Output: "6"

**Example 2:**

> Input: num1 = "123", num2 = "456"
> Output: "56088"

**Note:**

> The length of both num1 and num2 is < 110.
> Both num1 and num2 contain only digits 0-9.
> Both num1 and num2 do not contain any leading zero, except the number 0 itself.
> You **must not use any built-in BigInteger library** or **convert the inputs to integer** directly.

## 思路 - 乘法竖式

这道题的目的是做乘法计算，限制使用BigInteger, 也不能直接转换成数字进行乘法处理。

不能使用Big Integer, 又要做乘法，就必须要解决数据溢出的问题。为了解决这个问题，采用int[]的方式来记录每一位的数字。 然后用竖式乘法来将num1 * num2 转换成按位相乘然后累计相加的问题。

累计相加的时候要注意的是进位。用一个`int carry`来记录上一次计算是否进位。

在按位相乘的时候，每次都用一位数对另外一个数组所有的位数都相乘，并且计算进位。为了优化这个过程，考虑到被乘数是一个，变化的是乘数，这个乘数只有1位。所有相同的乘数必然会得出相同的结果。借助一个暂存`Dictionary`, 保存已经计算过的乘积。然后对每一位上的乘积进行相加。

最后的结果，有可能存在前导0，所有在生成返回值之前要处理掉前导0.

## 代码 - 乘法竖式

```csharp
public class Solution {
    public string Multiply(string num1, string num2)
    {
        int[] ns1 = ConvertChsToInts(num1.ToCharArray());
        int[] ns2 = ConvertChsToInts(num2.ToCharArray());
        int[] ans = new int[ns1.Length + ns2.Length + 1];

        Dictionary<int, int[]> store = new Dictionary<int, int[]>();

        for (int i = ns2.Length - 1; i >= 0; i--)
        {
            MultipleXY(store, ans, ns1, ns2[i], ns1.Length + 1 + i);
        }

        int hp = 0;
        while (hp < ans.Length && ans[hp] == 0) hp += 1;
        StringBuilder res = new StringBuilder();
        for (; hp < ans.Length; hp++)
            res.Append(ans[hp]);


        return res.Length == 0 ? "0" : res.ToString();
    }

    private void MultipleXY(Dictionary<int, int[]> store, int[] ans, int[] nums, int mul, int pos)
    {
        int carry = 0;

        int[] tmp;
        if (store.ContainsKey(mul))
            tmp = store[mul];
        else
        {
            tmp = new int[nums.Length + 1];

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                int r = nums[i] * mul + carry;
                carry = r / 10;
                tmp[i + 1] = r % 10;
            }
            tmp[0] = carry;
            store.Add(mul, tmp);
        }

        carry = 0; // clear carry
        for (int i = tmp.Length - 1; i >= 0; i--)
        {
            int r = tmp[i] + ans[pos] + carry;
            carry = r / 10;
            ans[ pos] = r % 10;
            pos--;
        }
        ans[pos] = carry;
    }
    private int[] ConvertChsToInts(char[] chs)
    {
        int[] res = new int[chs.Length];
        for (int i = 0; i < chs.Length; i++)
            res[i] = chs[i] - '0';
        return res;
    }

}
```
