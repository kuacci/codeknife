# [Medium][1011. Capacity To Ship Packages Within D Days](https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/)

A conveyor belt has packages that must be shipped from one port to another within D days.

The i-th package on the conveyor belt has a weight of weights[i].  Each day, we load the ship with packages on the conveyor belt (in the order given by weights). We may not load more weight than the maximum weight capacity of the ship.

Return the least weight capacity of the ship that will result in all the packages on the conveyor belt being shipped within D days.

**Example 1:**

> Input: weights = [1,2,3,4,5,6,7,8,9,10], D = 5
> Output: 15
> Explanation:
> A ship capacity of 15 is the minimum to ship all the packages in 5 days like this:
> 1st day: 1, 2, 3, 4, 5
> 2nd day: 6, 7
> 3rd day: 8
> 4th day: 9
> 5th day: 10

Note that the cargo must be shipped in the order given, so using a ship of capacity 14 and splitting the packages into parts like (2, 3, 4, 5), (1, 6, 7), (8), (9), (10) is not allowed.

**Example 2:**

> Input: weights = [3,2,2,4,1,4], D = 3
> Output: 6
> Explanation:
> A ship capacity of 6 is the minimum to ship all the packages in 3 days like this:
> 1st day: 3, 2
> 2nd day: 2, 4
> 3rd day: 1, 4

**Example 3:**

> Input: weights = [1,2,3,1,1], D = 4
> Output: 3
> Explanation:
> 1st day: 1
> 2nd day: 2
> 3rd day: 3
> 4th day: 1, 1

**Note:**

* 1 <= D <= weights.length <= 50000
* 1 <= weights[i] <= 500

## 思路 - 二分法

这道题目的意思是用一艘货船来运送货物，这艘货船的容量是需要我们来计算的，并且要求得最小的容量。数组weight每个元素代表货物的重量。装货的时候只能按顺序装货。装满就出发，并且算一天。要求天数不能超过D。其实就是按数组的顺序对数字相加，当 sum >= 货船容量的时候，count++，要求count < D. 求出 货船最小容量。

由此可知，货船容量是存在一个可选范围的。最小值必须要大于weight数组中最大的元素的值。例如，一个货物最大的重量是50000，那么货船的容量必须>=50000， 否则可能存在货物装不走的情况。
货船容量最大不会超过所有货物的总重量sum(weight[]). 例如，D=1的情况下，货船需要一次性把所有货物都拉着，它的容量应该等于所有货物之和。

得知货船容量的可选范围之后，其中一个方法就是在这个范围里面依次实验，看看是否能够符合D天只能运走的可能性。但是考虑到依次加一(cap++)并不是最优解。比较快的方法是用二分法，能够在范围内迅速找出正常的值。

## 代码 - 二分法

```csharp
using System;

public class Solution
{
    public int ShipWithinDays(int[] weights, int D)
    {

        int lo = 0; // 最小值必须要大于weight数组中最大的元素的值
        int hi = 0; // 货船容量最大不会超过所有货物的总重量
        int mid = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            lo = Math.Max(lo, weights[i]);
            hi += weights[i];
        }

        if (D <= 1) return hi;

        while (lo <= hi)
        {
            mid = (lo + hi) / 2;

            if (helper(weights, D, mid))
            {
                // 满足要求，则缩小capacity
                hi = mid - 1;
            }
            else
            {
                // 无法满足D天运送的要求，提高capacity
                lo = mid + 1;
            }
        }

        return lo;
    }

    // 检验假设的capacity是否能满足D天内运送完成的可能性
    private bool helper(int[] weights, int D, int cap)
    {
        int count = 1;
        int sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (sum + weights[i] <= cap)
            {
                sum += weights[i];
            }
            else
            {
                sum = weights[i];
                count++;
            }
        }
        return count <= D;
    }
}
```
