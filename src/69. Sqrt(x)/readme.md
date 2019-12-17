# [Easy][69. Sqrt(x)](https://leetcode.com/problems/sqrtx/)

Implement int sqrt(int x).

Compute and return the square root of x, where x is guaranteed to be a non-negative integer.

Since the return type is an integer, the decimal digits are truncated and only the integer part of the result is returned.

Example 1:

```text
Input: 4
Output: 2
```

**Example 2:**

```text
Input: 8
Output: 2
Explanation: The square root of 8 is 2.82842..., and since
             the decimal part is truncated, 2 is returned.
```

## 思路 - 二分法

这题是要求实现计算方差。最简单的方法当然是从1一直加上去一个个的试哪个值最接近x。但是这样做会很慢。相对比较快速接近正确值的方法就是二分法，

当值比较小的时候，方法差会比较解决它的中间值，例如 `Math.Sqrt(4) = 2`. 当x增大的时候就会偏的比较远，例如100的中间值是50，而它的方差在10.这种情况下需要求多次中间值，直到它的左边界和有边界相差1. 相差1的原因是，这道题目的精度要求控制在个位数，且取`Math.Floor`. 这里要注意的是数值的溢出。由于输入是int，在进行运算的时候可能会溢出。例如传入的是`int.MaxValue`, 在进行加，乘运算的时候会得到不可预期的值。为了解决这个问题需要采用decimal进行运算。但是在求`mid`值的时候, 因为精度要求的关系，可以再取整。

时间复杂度：O(LogN)
空间复杂度：O(1)

## 代码 - 二分法

```csharp
public class Solution {
    public int MySqrt(int x)
    {
        if (x == 0 || x == 1) return x;


        decimal left = 0;
        decimal right = x;

        while (left < right - 1)
        {
            decimal mid = Math.Floor((right - left) / (decimal)2 + left);
            decimal p = mid * mid;
            if (mid * mid == x) return (int)mid;

            if (p > x)
            {
                right = mid;
            }
            else
            {
                left = mid;
            }
        }

        return (int)left;
    }
}
```

## 思路 - 二分法 - 优化 1

上面执行的速度够不快，再计算mid的时候`Math.Floor((right - left) / (decimal)2 + left);`太花时间。而且它是再整个计算重占比较核心地位的运算。可以通过位移的方式来获得 right / 2的效果， `long mid = left + right >> 1;`. 不过因为decimal不支持位操作，所以要更换为float.

## 代码 - 二分法 - 优化 1

```csharp
public class Solution {
    public int MySqrt(int x)
    {
        if (x == 0 || x == 1) return x;
        long left = 0;
        long right = x;

        while (left < right - 1)
        {
            long mid = left + right >> 1;
            long p = mid * mid;
            if (p == x) return (int)mid;

            if (p > x)
                right = mid;
            else
                left = mid;
        }

        return (int)left;
    }
}
```
