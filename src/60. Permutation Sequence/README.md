# [Medium][60. Permutation Sequence](https://leetcode.com/problems/permutation-sequence/)

The set [1,2,3,...,n] contains a total of n! unique permutations.

By listing and labeling all of the permutations in order, we get the following sequence for n = 3:

1. "123"
2. "132"
3. "213"
4. "231"
5. "312"
6. "321"

Given `n` and `k`, return the kth permutation sequence.

**Note:**

Given n will be between 1 and 9 inclusive.
Given k will be between 1 and n! inclusive.

**Example 1:**

> Input: n = 3, k = 3
> Output: "213"

**Example 2:**

> Input: n = 4, k = 9
> Output: "2314"

## 思路 - 数学规律

一种思路是按照顺序用[全排序](src/46.%20Permutations)，然后到第k个的时候返回。
另外一种思路是通过计算的方式获得。题设给出来的条件是：

1. 数字是从 1 - n, 无重复，`1 <= n <= 9`。
2. 全排列的数目是n!
3. 要第k个有序数字。

我们先来看看全排列的组合，寻找他们的规律：

n = 1 : 1
n = 2 : `[1] * factor[1] + [2] * factor[1]` = 2
n = 3 : `[1] * factor[2] + [2] * factor[2] + [3] * factor[2] = 3 * 2 = 6`
n = 4 : .... = 4 * 6 = 24
....

解释 ：
n = 1 的时候，只有1种选择 1.
n = 2 的时候，高位可以选择1,或者2.先确定高位为[1]，那么剩下一个数字就是n = 1时候的可共选择数目是n=1时候的数目，所以n = 2时候的数目是 2 * factor[1].
n = 3 的时候，情况跟2的时候类型，高位可以选择1，2，3共3种情况。选定高位以后就剩下2位数字做全排列，所以对应的数目是factor的数目。
n = 4 以此类推。

因此先计算n!的数目，可以保存在factor数组中。为了方便计算，factor[0]保存1，最高位为n - 1, 而不需要计算到n!.

```csharp
int[] factor = new int[n];
factor[0] = 1;
for (int i = 1; i < n; i++)
{
    factor[i] = factor[i - 1] * i;
}
```

每一位选多少，也是有规律的。如果k等于1，就是第一位。那么应该是最小排序。`例如n = 3, k = 1, ans = 123.`
当高位选为[1]的时候，k的范围是`1-2`.这一点正好是factor[2]的范围。当高位为[2]时候，k的范围是`3-4`, 为`[3]`时候，则是`5-6`.
所以可以先确定高位的范围用`nums[index - 1] = k / factor[index - 1];`，然后依次求出低位的值。

```csharp
int[] nums = new int[n];
int index = n;
while(index > 0)
{
    nums[index - 1] = k / factor[index - 1];
    k = k % factor[index - 1];
    index--;
}
```

确定了每位的数字时候，就开始按照顺序生成数字了。这里利用一个`List<int>`对数字进行排序。

```csharp
List<int> lst = new List<int>();
for (int i = 1; i < n; i++)
{
    lst.Add(i);
}
lst.Add(n);
```

用nums中的值作为List的索引来取他里面的值。这样就可以生成第k个有序值了。

## 代码 - 数学规律

```csharp
public class Solution {
    public string GetPermutation(int n, int k)
    {
        int[] factor = new int[n];
        factor[0] = 1;
        List<int> lst = new List<int>();
        for (int i = 1; i < n; i++)
        {
            factor[i] = factor[i - 1] * i;
            lst.Add(i);
        }
        lst.Add(n);
        k -= 1;

        int[] nums = new int[n];
        int index = n;
        while(index > 0)
        {
            nums[index - 1] = k / factor[index - 1];
            k = k % factor[index - 1];
            index--;
        }
        StringBuilder ans = new StringBuilder();
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            ans.Append(lst[nums[i]]);
            lst.RemoveAt(nums[i]);
        }
        return ans.ToString();
    }
}
```
