# [Easy][172. Factorial Trailing Zeroes](https://leetcode.com/problems/factorial-trailing-zeroes/)

Given an integer n, return the number of trailing zeroes in n!.

**Example 1:**

> Input: 3
> Output: 0
> Explanation: 3! = 6, no trailing zero.

**Example 2:**

> Input: 5
> Output: 1
> Explanation: 5! = 120, one trailing zero.

Note: Your solution should be in logarithmic time complexity.

## 思路 - 计算5的倍数

这道题的要求是计算N!的结果中有多少个0作为结尾。即，计算有多个10相乘。能构成10的倍数的数字比较特殊，如1000，100，10，5 * 2. 其中无论1000，100，10都可以分解为 `5 * 2 * n`.

除此之外，还有一些数字也能构成10的倍数，如 `25 * 4`, `125 * 8`. 都可以分解为 `5 * 2`的倍数。

所以总结出来经验是，要计算N!有多少个0结尾，要计算出`5 * 2`的个数. 由于2及2的倍数出现的次数远高于5的次数，所以只要计算5的个数即可。

在计算5的个数的时候要注意大数下包含的小数。例如100 / 5 = 20， 里面包含了 20个5，同时还包含了`25 = 5 ^ 2`。 所以不但要计算出以5为结尾的数量，5的倍数也要计算出来。

## 代码 - 计算5的倍数

```csharp
public class Solution {
    public int TrailingZeroes(int n)
    {
        int five = 0;

        while (n >= 5)
        {
            n /= 5;
            five += n;
        }

        return five;
    }
}
```
