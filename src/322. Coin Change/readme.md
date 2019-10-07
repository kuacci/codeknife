# [322. Coin Change](https://leetcode.com/problems/coin-change/)

You are given coins of different denominations and a total amount of money amount. Write a function to compute the fewest number of coins that you need to make up that amount. If that amount of money cannot be made up by any combination of the coins, return -1.

**Example 1:**

> Input: coins = [1, 2, 5], amount = 11
> Output: 3
> Explanation: 11 = 5 + 5 + 1

**Example 2:**

> Input: coins = [2], amount = 3
> Output: -1

Note:
You may assume that you have an infinite number of each kind of coin.

## 思路 - DP - 递归算法

受到[这里](https://leetcode-cn.com/problems/coin-change/solution/dong-tai-gui-hua-tao-lu-xiang-jie-by-wei-lai-bu-ke/)的启发先试试一个带记录数组的递归。

## 代码 - DP - 递归算法

```csharp
public class Solution {
    private int[] dpt;
    public int CoinChange(int[] coins, int amount)
    {
        if (coins.Length == 0 || amount < 0) return -1;
        dpt = new int[amount + 1];

        for (int i = 0; i < dpt.Length; i++)
        {
            dpt[i] = -2;
        }
        Array.Sort(coins);

        int ans = helper(coins, amount);
        return ans;
    }


    private int helper(int[] coins, int amount)
    {
        if (amount < 0) return -1;
        if (amount == 0) return 0;
        if (dpt[amount] != -2) return dpt[amount];

        int counts = -1;
        for (int i = 0; i < coins.Length; i++)
        {
            if (coins[i] == amount)
            {
                dpt[amount] = 1;
                counts = 1;
                break;
            }
            else if (coins[i] > amount)
            {
                if (dpt[amount] == -2)
                    dpt[amount] = -1;
                break;
            }
            else
            {
                counts = helper(coins, amount - coins[i]);
                if (counts == -1)
                {
                    if (dpt[amount] == -2)
                        dpt[amount] = -1;
                    continue;
                }
                counts += 1;

                dpt[amount] = dpt[amount] > 0 ? Math.Min(counts, dpt[amount]) : counts;
            }
        }

        return dpt[amount];
    }
```

## 思路 - DP - 最优子结构

研究上面的结果之后，总结出最优子结构 ：

1. if(amount == coin[i]) count = 1;
2. counts = helper(coins, amount - coins[i]);

从记录数组的左侧开始算，`change[i]`代表`amount == i` 的时候，总过需要多少给coin的情况。第一位`changes[0]`, 目的是为了第一个`change[coin[i]]`的值为1. 下一个`change[i]`的值则根据`changes[i] = Math.Min(changes[i], changes[i - coins[j]] + 1)`获得。
因为既然是最优子结构，那么分解的子任务是最优解，才能保证当前值是最优解。

## 代码 - DP - 最优子结构

```csharp
public class Solution {
    public int CoinChange(int[] coins, int amount)
    {
        if (coins.Length == 0 || amount < 0) return -1;
        if (amount == 0) return 0;

        Array.Sort(coins);
        int[] changes = new int[amount + 1];
        Array.Fill(changes, amount + 1);
        changes[0] = 0;

        for (int i = 1; i < changes.Length; i++)
        {
            for (int j = 0; j < coins.Length; j++)
            {
                if (i >= coins[j])
                    changes[i] = Math.Min(changes[i], changes[i - coins[j]] + 1);
            }
        }
        return changes[changes.Length - 1] == amount + 1 ? -1 : changes[changes.Length - 1];
    }

}
```

## 思路 - backtrack

并不是所有时候都需要用dp. 这种情况下，能凑成coins并且有多个组合的情况下，一定是面额最多的那种组合数目最少。
所有先从最大面额开始试，被最多面额减掉后剩下的数量进入下一次递归，coin的面额降一档。
如果到最后没有完全分配完，说明这种配置不合理，回溯到上一个组合，大面额的coin减少一个，再次进入循环。

BTW， 这种方式再leetcode上面打败了99%的算法。。。

## 代码 - backtrace

```csharp
public int CoinChange(int[] coins, int amount)
{
    if (coins.Length == 0 || amount < 0) return -1;
    if (amount == 0) return 0;

    Array.Sort(coins);
    change(coins, coins.Length - 1, amount, 0);
    return ans == int.MaxValue ? -1 : ans;
}

int ans = int.MaxValue;
private void change(int[] coins, int pos, int amount, int count)
{
    if (pos < 0) return;
    for (int j = amount / coins[pos]; j >= 0; j--)
    {
        if (amount - j * coins[pos] == 0)
        {
            ans = Math.Min(count + j, ans);
            break;
        }
        else if (count + j < ans)
            change(coins, pos - 1, amount - j * coins[pos], count + j);
        else return;
    }
}

```
