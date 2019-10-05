# [8. String to Integer (atoi)](https://leetcode.com/problems/string-to-integer-atoi/)

Implement `atoi` which converts a string to an integer.

The function first discards as many whitespace characters as necessary until the first non-whitespace character is found. Then, starting from this character, takes an optional initial plus or minus sign followed by as many numerical digits as possible, and interprets them as a numerical value.

The string can contain additional characters after those that form the integral number, which are ignored and have no effect on the behavior of this function.

If the first sequence of non-whitespace characters in str is not a valid integral number, or if no such sequence exists because either str is empty or it contains only whitespace characters, no conversion is performed.

If no valid conversion could be performed, a zero value is returned.

**Note:**

> * Only the space character ' ' is considered as whitespace character.
> * Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−231,  231 − 1]. If the numerical value is out of the range of representable values, INT_MAX (231 − 1) or INT_MIN (−231) is returned.

**Example 1:**

> Input: "42"
> Output: 42

**Example 2:**

> Input: "   -42"
> Output: -42
> Explanation: The first non-whitespace character is '-', which is the minus sign.
> Then take as many numerical digits as possible, which gets 42.

## 思路

String to integer的几个注意点是：

1. 清除空字符
2. 符号
3. char to int
4. string to int时候的overflow

清除空字符比较容易， 用string.Trim()就可以完成。如果不允许做Trim()，那么可以用一个循环把前面几个空字符去掉。符号也不用多说，判断一下第一个非空字符是否是'+'或者'-'， 如果既不是这2个符号之一又不是数字，就返回0.

```csharp
int pos = 0;
while(pos < str.Length && str[pos] == ' ') pos++;  // 找到不为空的第一个字符串
```

char to int的值得注意的是char直接转换成int是转化了他的ASCII码，所有要得到他的数字的时候，要用 `int ans = ch[i] - '0'`来进行转化。

string to int的时候，要注意overflow， 因为int32 的范围是 `-2147483648 -- 2147483647`. 如果输入的string超过这个值会造成int32的溢出。为了解决这个问题，我使用了更宽范围的decimal,保证不会超出int32范围的时候报错，再判断是否超过了2147483648这个值。使用这个值是因为int.MinValue的绝对值比int.MaxValue多1.这样可以涵盖 int.MinValue的情况。

```csharp
for (; i < ch.Length; i++)
{
    if (Char.IsNumber(ch[i]))
    {
        ans = ans * 10 + ch[i] - '0';
        if (ans * -1 < int.MinValue) break;
    }
    else
    {
        break;
    }
}
```

还有另外一种思路就是try ... catch ... 的方法，如果转换的时候抛出异常就认为溢出了,中断后面的转换。

```csharp
for (; i < ch.Length; i++)
{
    if (Char.IsNumber(ch[i]))
    {
        try{
            ans = ans * 10 + ch[i] - '0';
        }
        catch
        {
            ans = isNegitive ? int.MinValue : int.MaxValue;
            break;
        }
    }
    else
    {
        break;
    }
}
```

## 代码

```csharp
public class Solution
{
    public int MyAtoi(string str)
    {

        if (string.IsNullOrEmpty(str.Trim())) return 0;

        bool isNegitive = false;
        decimal ans = 0;
        char[] ch = str.Trim().ToCharArray();

        int i = 0;

        if (!Char.IsNumber(ch[0]))
        {
            if (ch[0] != '+' && ch[0] != '-')
                return 0;
            else if (ch[0] == '-')
                isNegitive = true;
            else
                isNegitive = false;
            i = 1;
        }


        for (; i < ch.Length; i++)
        {
            if (Char.IsNumber(ch[i]))
            {
                ans = ans * 10 + ch[i] - '0';
                if (ans * -1 < int.MinValue) break;
            }
            else
            {
                break;
            }
        }

        if (isNegitive)
        {
            ans *= -1;
            if (ans < int.MinValue) ans = int.MinValue;
        }
        else
        {
            if (ans > int.MaxValue) ans = int.MaxValue;
        }

        return (int)ans;
    }
}
```
