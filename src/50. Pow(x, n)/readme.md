# [50. Pow(x, n)](https://leetcode-cn.com/problems/powx-n/)

Implement pow(x, n), which calculates x raised to the power n (xn).

**Example 1:**

> Input: 2.00000, 10
> Output: 1024.00000

**Example 2:**

> Input: 2.10000, 3
> Output: 9.26100

**Example 3:**

> Input: 2.00000, -2
> Output: 0.25000
> Explanation: 2-2 = 1/22 = 1/4 = 0.25

**Note:**

> -100.0 < x < 100.0
> n is a 32-bit signed integer, within the range [−231, 231 − 1]

## 思路 - 1

最简单的方法是一个for循环让 x 相乘 n 次。但是显然不能用这个方法，超时是可以肯定的。也不能直接调用Math.Pow()方法，不然这题就没有意义了。
最常规的方法是让一对一对的做相乘
例如：

> 2^2 = 2 * 2
> 2^4 = `{ 2 * 2} = x1; x1 * x1;`
> 2^8 = `{ 2 * 2} = x1; x1 * x1 = x2; x2 * x 2`

这里有过问题就是n为2的倍数，并且每次都能被2整除，只有就没有问题。否则就要单独乘一次x。

> 2^3 = `(2 * 2)` * `2`
> 2^5 = 2^4 * 2
> 2^10 = 2^8 * 2^2

## 代码 - 1

```csharp
public class Solution {
    public double MyPow(double x, int n) {
        if(n == 0) return 1;

        double ans = 1;
        for(int i = n; i != 0; i /= 2 )
        {
            if(i % 2 != 0) ans *= x;
                x *= x;
        }

        return n > 0 ? ans : 1 / ans;
    }
}
```

