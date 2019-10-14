# [29. Divide Two Integers](https://leetcode.com/problems/divide-two-integers/)

Given two integers dividend and divisor, divide two integers without using multiplication, division and mod operator.

Return the quotient after dividing dividend by divisor.

The integer division should truncate toward zero.

Example 1:

Input: dividend = 10, divisor = 3
Output: 3
Example 2:

Input: dividend = 7, divisor = -3
Output: -2
Note:

Both dividend and divisor will be 32-bit signed integers.
The divisor will never be 0.
Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−231,  231 − 1]. For the purpose of this problem, assume that your function returns 231 − 1 when the division result overflows.

## 思路 - 趟过的那些坑

这道题目的要求是实现除法，并且要求不能使用 操作符 `*`, `/` 和 `%`. 一开始看，觉得这个问题还比较简单，深入做下去感觉哪里都是坑。

1. overflow : 这里Integer给出来的范围是`[−2^31,  2^31 − 1]`. 即 `[-2147483648 - 2147483647]`. Int.MinValue的绝对值比Int.MaxValue的绝对值多1。
2. 计算商的时候会超时。

### overflow

除数和被除数有正负之分，所以在计算前要先判断最后的结果是正还是负。然后降除数和被除数都转换成同一个方向，再进行计算。如果转换成正数，就有可能出现溢出的问题。例如一个数为int.MinValue (-2147483648)， 转换成它的绝对值就是(2147483648),这个在int32中是会溢出的。原因在于，计算机中存储有符号数字的时候采用补码。正数的二进制数与它的补码相同，而负数就要进过换算。正数变复数的换算的过程是：

1. 按位取反，最高位作为符号位。
2. 最低位加1.

例如 7 转换为 -7 的过程。

1. 7 的二进制补码为 (0111)
2. 按位取反，(1000)
3. 最低位+1，(1001)
4, 结果为十进制的-7.

从负数转换为正数的过程是一个相反的过程。

1. 最低位-1.
2. 按位取反.

那么在转换-2147483648的时候会遇到问题。

1. -2147483648的二进制为1111 1111 1111 1111 1111 1111 1111 1111, 16进制(FFFF FFFF).
2. 最低位-1后为 1111 1111 1111 1111 1111 1111 1111 1110.
3. 按位取反，最高位作为符号位变成了0000 0000 0000 0000 0000 0000 0000 0001，并不是想要的数字。这是溢出造成的。

### overflow - 解决方案

既然int.MinValue 转换成int.MaxValue有问题，但是int.MaxValue转换成int.MinValue没有问题。那么就可以反着来。先判断最终结果是正还是负。然后将被除数和除数都转换成负数。

```csharp
    bool sign = dividend > 0 ^ divisor > 0;
    if (dividend > 0) dividend = -dividend;
    if(divisor > 0) divisor = -divisor;
```

### 商的计算

由于不能使用 `*` `/` `%`. 那么就使用`-`来计算商。从被减数中减去一个减数，商+1, 一直减到被减数>=减数为止。例如 ：

100 / 10. 用 10 来减 100，减了10次以后100被减为0，说明商为10.

但是这种方式存在着问题，每次只-1的算法，如果减少很小，而被减数很大，如：
-2147483648 / 1 的情况下，就要减 2147483648 次。这个时候会超时。

为了解决这个方法，使用必须成倍的减少。由于不能用`*`, 为了达到这个效果，可以使用位移 `<`，每次做左位移，相当于*2的效果。
不过在做这个左位移的时候，要判断是否超出int.MinValue的边界。

``` csharp
    while(dividend <= divisor)
    {
        int div = divisor;
        int count = -1;
        while(dividend < div << 1       // 判断 *2 后是否会比被除数大
        && div > int.MinValue >> 1)   // 判断 *2 以后是否会超出边界
        {
            div = div << 1;
            count = count << 1; // 计算次数
        }
        ans = ans + count;  // 合并到商
        dividend -= div;
    }
```

## 代码 - 用减法代替除法

```csharp
public int Divide(int dividend, int divisor)
{
    bool sign = dividend > 0 ^ divisor > 0;
    int ans = 0;

    if (dividend > 0) dividend = -dividend;
    if(divisor > 0) divisor = -divisor;


    while(dividend <= divisor)
    {
        int div = divisor;
        int count = -1;
        while(dividend < div << 1 && div > int.MinValue >> 1)
        {
            div = div << 1;
            count = count << 1;
        }
        ans = ans + count;
        dividend -= div;
    }

    if(sign)
        return ans;
    else
    {
        if (ans == int.MinValue) return int.MaxValue;
        else return -ans;
    }

}
```
