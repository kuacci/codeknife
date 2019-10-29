# [Easy][70. Climbing Stairs](https://leetcode.com/problems/climbing-stairs/)

You are climbing a stair case. It takes n steps to reach to the top.

Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?

Note: Given n will be a positive integer.

**Example 1:**

```text
Input: 2
Output: 2
Explanation: There are two ways to climb to the top.

1. 1 step + 1 step
2. 2 steps
```

**Example 2:**

```text
Input: 3
Output: 3
Explanation: There are three ways to climb to the top.
1. 1 step + 1 step + 1 step
2. 1 step + 2 steps
3. 2 steps + 1 step
```

## 思路 - DP - 最优子结构

这道题是要求计算爬楼梯的可能性，每次只能选择怕一阶或者两阶。

只有1阶的时候，只有一种方式。
只有2阶的时候，只有两种方式。走一步，走两步。
只有3阶的时候, 就不一样了。可以先走一步，那么还剩下2个阶梯，剩下的可能性就是2阶的时候的可能性的总和。要么走2步，剩下1个阶梯，就是只有1阶的时候方法的总和。
以此类推，4阶的时候，是3阶和2阶的总和。5阶的时候是4阶和3阶的总和。所有得出公式 ：

`count(n) = count(n - 1) + count(n -2)`, 看到这个公式，基本就跟[fibonacci-number](https://leetcode.com/problems/fibonacci-number/)一样了。

## 代码 - DP - 最优子结构

```csharp
public class Solution {
    public int ClimbStairs(int n) {
        if(n <= 2) return n;

        int n1 = 1;
        int n2 = 2;
        int ans = 0;
        for(int i = 3; i <= n; i ++)
        {
            ans = n1 + n2;
            n1 = n2;
            n2 = ans;
        }
        return ans;
    }
}
```
