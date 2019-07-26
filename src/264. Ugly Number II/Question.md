# [LeetCode] 264. Ugly Number II

Write a program to find the n-th ugly number.

Ugly numbers are positive numbers whose prime factors only include 2, 3, 5.

**Example:**

>**Input:** n = 10
**Output:** 12
**Explanation:** 1, 2, 3, 4, 5, 6, 8, 9, 10, 12 is the sequence of the first 10 ugly numbers.

Note:  

>1 is typically treated as an ugly number.
2 n **does not exceed 1690.**

## 思路 - 翻转链表

Ugly Number的定义为，2，3，5中的一个数的倍数，1定义为Ugly Number. 例如，4 = **2** * 2. 3 = **3** * 3. 15 = **5** * 3  等等。 7 不是 ugly number, 因为7不能被2，3，5中任意一个数整除。

| 2 | 3 | 4 | 5 | 6 | 8 | 9 | 10| 12 |
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|2 * 1|3 * 1|2 * 2|5 * 1|2 * 3|2 * 4|3 * 3|5 * 2|2 * 6|

比较上面的规律，可以看到2的倍率增长快，中间穿插着3和5，他们的倍率增长不一样。可以用P1, P2, P3分别存储2，3，5当前的倍数。每次计算下一个Ugly Number的时候， 2， 3，5乘上他自己对应的倍数Px。计算结果取最小值为当前的倍数。被选取的那个数，倍数+1. 一直计算到n个Ugly Number为止。

## 代码

``` csharp
public class Solution {
    public int NthUglyNumber(int n) {

        if(n == 1) return 1;

        int[] nums = new int[n];

        int p2 = 0;
        int p3 = 0;
        int p5 = 0;

        nums[0] = 1;

        for(int i = 1; i < n; i++)
        {
            int n2 = nums[p2] * 2;
            int n3 = nums[p3] * 3;
            int n5 = nums[p5] * 5;

            nums[i] = min(min(n2,n3),n5);

            if(nums[i] / 2 == nums[p2]) p2 +=1;
            if(nums[i] / 3 == nums[p3]) p3 +=1;
            if(nums[i] / 5 == nums[p5]) p5 +=1;
        }

        return nums[n-1];
    }

    public int min(int a, int b)
    {
        return a < b ? a : b;
    }
}
```
