# [Easy][202. Happy Number](https://leetcode.com/problems/happy-number/)

Write an algorithm to determine if a number is "happy".

A happy number is a number defined by the following process: Starting with any positive integer, replace the number by the sum of the squares of its digits, and repeat the process until the number equals 1 (where it will stay), or it loops endlessly in a cycle which does not include 1. Those numbers for which this process ends in 1 are happy numbers.

Example:

```text
Input: 19
Output: true
Explanation:
12 + 92 = 82
82 + 22 = 68
62 + 82 = 100
12 + 02 + 02 = 1
```

## 思路 - 脑力破解

这道题定义了一种快乐数，就是说对于某一个正整数，如果对其各个位上的数字分别平方，然后再加起来得到一个新的数字，再进行同样的操作，如果最终结果变成了1，则说明是快乐数，如果一直循环但不是1的话，就不是快乐数，那么现在任意给我们一个正整数，让我们判断这个数是不是快乐数，题目中给的例子19是快乐数。样本不够多，所以要自己多加一些样本。

```text
2 :
2^2 = 4
**4^2**     = 16
1^2 + 6^2   = 1 + 36 = 37 <====== repeat from here ( 6 & 1)
3^2 + 7^2   = 9 + 49 = 58
5^2 + 8^2   = 25 + 64 = 89
8^2 + 9^2   = 64 + 81 = 125
1^2 + 2^2 + 5^2 = 1 + 4 + 25 = 30
**3^2**     = 9
**9^2 **    = 81
8^2 + 1^2   = 64 + 1 = 65
6^2 + 5^2   = 36 + 25 = 61
6^2 + 1^2   = 36 + 1 = 37 < ====== repeat from here ( 6 & 1)
3^2 + 7^2   = 9 + 49 = 58
```

通过对2的计算，发现2最后会进入一个循环，从 1^2 + 6^2 到 6^2 + 1^2 会一直重复。所以2不是happy number. 同时再整个计算的过程中发现，3，4，9也同样出现再了路径上。这几个数字都会出现进入循环的情况。
增加几个样本，发现[2，3，4，5，9]都会进入循环。

所以思路是，按照happy number的计算方式实现代码使其进入一个循环，如果计算出来的是1，返回true.循环的终止条件是进入`[2，3，4，5，9]`中的一个数字。不需要再增加样本，是因为多次计算发现，最后大数都会收敛到个位。

之所以称之为脑力破解，完全是因为靠心算找出终止条件的数字。不过因为逻辑非常简单，所以LeetCode上面超过了98%

## 代码 - 脑力破解

```csharp
public class Solution {
    public bool IsHappy(int n)
    {

        if (n < 1) return false;
        if (n == 1) return true;

        int t = n;
        while (true)
        {
            List<int> lst = GenerateListFromNumber(t);
            t = 0;
            foreach (var i in lst)
            {
                t = t + (int)(Math.Pow(i, 2));
            }
            if (t == 1) return true;
            if (t == 2 || t == 3 || t == 4 || t == 5 || t == 9) break;
        }

        return false;
    }

    private List<int> GenerateListFromNumber(int n)
    {
        List<int> lst = new List<int>();
        int t = n;

        while (t >= 10)
        {
            lst.Add(t % 10);
            t = t / 10;
        }
        lst.Add(t);

        return lst;
    }
}
```

## 思路 - 存储重复数

上面是通过心算完成break的条件。如果不经过心算要怎么办。一个方法就是记录曾经出现过的计算结果。如果计算结果再次出现，必然是进入到了循环中。

## 代码 - 存储重复数

```csharp
public class Solution {
    public bool IsHappy(int n)
    {

        if (n < 1) return false;
        if (n == 1) return true;
        List<int> reminder = new List<int>();
        int t = n;
        while (true)
        {
            if (t == 1) return true;
            if (reminder.Contains(t))
                break;
            else
                reminder.Add(t);

            List<int> lst = GenerateListFromNumber(t);
            t = 0;
            foreach (var i in lst)
            {
                t = t + (int)(Math.Pow(i, 2));
            }
        }
        return false;
    }

    private List<int> GenerateListFromNumber(int n)
    {
        List<int> lst = new List<int>();
        int t = n;

        while (t >= 10)
        {
            lst.Add(t % 10);
            t = t / 10;
        }
        lst.Add(t);

        return lst;
    }
}
```

## 思路 - 快慢指针

快慢指针是检查是否形成闭环的一种方法。在计算happy number的时候，非happy number会进入一个死循环，那么可以用快慢指针检查出来。快慢指针值得的是，两个指针都沿着一个方向走，快指针走2步，慢指针走1步。如果有闭环，那么快指针必然绕一圈吼，从后面追上慢指针。

这里利用这个原理，一个记录器记录计算2次的值，另外一个记录计算1次的值。如果重合就break掉循环，如果是1，则返回true.

## 代码 - 快慢指针

```csharp
public class Solution {
    public bool IsHappy(int n)
    {

        if (n < 1) return false;
        if (n == 1) return true;
        int slow = n;
        int fast = n;
        while (true)
        {
            slow = FindNext(slow);
            fast = FindNext(fast);
            fast = FindNext(fast);
            if (slow == 1 || fast == 1) return true;
            if (slow == fast) break;
        }
        return false;
    }

    private int FindNext(int n)
    {
        List<int> lst = GenerateListFromNumber(n);
        int ans = 0;
        foreach (var i in lst)
        {
            ans += (int)(Math.Pow(i, 2));
        }
        return ans;
    }

    private List<int> GenerateListFromNumber(int n)
    {
        List<int> lst = new List<int>();
        int t = n;

        while (t >= 10)
        {
            lst.Add(t % 10);
            t = t / 10;
        }
        lst.Add(t);

        return lst;
    }
}
```
