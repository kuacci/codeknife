# [Medium][1079. Letter Tile Possibilities](https://leetcode.com/problems/letter-tile-possibilities/)

You have a set of tiles, where each tile has one letter tiles[i] printed on it.  Return the number of possible non-empty sequences of letters you can make.

**Example 1:**

>Input: "AAB"
Output: 8
Explanation: The possible sequences are "A", "B", "AA", "AB", "BA", "AAB", "ABA", "BAA".

**Example 2:**

>Input: "AAABBC"
Output: 188

**Note:**

```text
1. <= tiles.length <= 7
2. tiles consists of uppercase English letters.
```

## 思路 - 动态规划

本题的目的是给定一个string，这个string全部为大写字母，要求给出所有字母的排列组合的数目。
一开始我的想法是先做[全排序](https://zh.wikipedia.org/zh-hans/%E5%85%A8%E6%8E%92%E5%88%97%E7%94%9F%E6%88%90%E7%AE%97%E6%B3%95)，然后计算全排序的数目。 后来想想不对，这种效率不太高。全排序算法适合于给定的一串array进行排列，在需要输出每次的排列会比较适用。这里的题目要求，并不需要输出，而是计算排列组合的数目，那么可以使用比较取巧的办法。

由于这里明确了string里面包含的全部为大写字母。那么可以用一个int[] 来代替这个string转化出来的 char[] 。int[] arr = new int[26]; 这里每个元素代表26个字母中的一个，例如 arr[0] 代表 A出现的次数， arr[25] 代表Z出现的次数。

```text
"AAABBC" =>
arr[0] = 3 // 3个A
arr[1] = 2 // 2个B
arr[2] = 1 // 1个C
arr[3] ~~ arr[25] = 0 // D - Z 出现的次数为0
```

数据结构选用好之后是如何进行计算。 观察了example之后，可以确认每次计算的结果可以基于上一次计算的结果，很适合使用dp算法。

例如 ：

```text
"AAB", "ABA", "BAA" 的分布可以基于
"AA", "AB", "BA"的分布。而他的分布是基于
"A", "B"的分布。
```

那么接下来是去重问题。使用for循环的时候，同一个轮循环只对这个字母取一次值，这样就不会出现重复的情况。

```csharp
for(int i = 0; i < arr.Length; i++)
{
    if(arr[i] == 0) continue;   // 如果这个字母不存在，或者已经被全取，则检测下一个字母

    sum += 1;
    arr[i]--;   // 字母已经被选取，数量-1
    sum += dp(arr);
    arr[i]++;   // 恢复这个字母的数量，下一次循环可能会用到。
}
```

## 代码

``` csharp
public class Solution {

    public int NumTilePossibilities(string tiles) {
        if(string.IsNullOrEmpty(tiles)) return 0;

        // 统计出现的字母
        int[] arr = new int[26];
        foreach(char c in tiles.ToCharArray())
        {
            arr[c - 'A']++;
        }

        return dp(arr);

    }

    private int dp(int[] arr)
    {
        int sum = 0;

        for(int i = 0; i < arr.Length; i++)
        {
            if(arr[i] == 0) continue;

            sum += 1;
            arr[i]--;   // 字母已经被选取，数量-1
            sum += dp(arr);
            arr[i]++;   // 恢复这个字母的数量，下一次循环可能会用到。
        }

        return sum;
    }

}
```
