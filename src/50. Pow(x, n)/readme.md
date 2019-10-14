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

## 思路 - 递归

从顶部进行分拆。画出一颗递归树。将n每次进行对半分解，有可能存在不能平分的情况，就要左右两边拆开。如下图：

![img](image\figure1.jpg)

这里要考虑的几个问题：

1. 从递归树能看到有很多重复的计算，所以需要将计算结果保存到Dictionary中，后面可以重复使用。
2. 对半拆开的时候要判断`n > 0`. 如果 `n > 0`，的情况，不能对半拆开的，一半要多加1，如果`n < 0`， 则是要 -1.
   例如 5 要拆成 2 和 2 + 1， -5 要拆成 -2 和 (-2 - 1)

## 代码 - 递归

```csharp
    public double MyPow(double x, int n)
    {
        //n = -10;
        double ans = 0;

        ans = helper(new Dictionary<int, double>(), x, n);

        return n > 0 ? ans : 1 / ans;

    }

    private double helper(Dictionary<int, double> storage, double x, int n)
    {
        if (n == 0) return 1;
        if (n == 1 || n == -1) return x;

        if (storage.ContainsKey(n)) return storage[n];

        double left = helper(storage, x, n / 2);
        double right = helper(storage, x, (n % 2) == 0 ? (n / 2) : (n / 2 + (n > 0 ? 1 : -1)));
        storage.Add(n, left * right);
        return storage[n];
    }
```
