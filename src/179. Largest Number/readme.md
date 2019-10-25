# [179. Largest Number](https://leetcode.com/problems/largest-number/)

Given a list of non negative integers, arrange them such that they form the largest number.

**Example 1:**

> Input: [10,2]
> Output: "210"

**Example 2:**

> Input: [3,30,34,5,9]
> Output: "9534330"

Note: The result may be very large, so you need to return a string instead of an integer.

## 思路 - 桶排序

要构建一个值最大的数，就是要求值越大的数字排再越前越好。所有的数中9开头的数一定比8开头的大。例如9 和 8999排列，9在前面能得到最大的数, `98999 > 89999`.
先用一个10个元素的数组`List<string>[] = new List<string>[10]`来存放以0 - 9 开头的数字。
单个bucket里面的数再进行排序。排序的方式是将两个数字拼接起来看哪个大，大的则放在bueckt的前面，保证bucket内部有序。
输出的时候从大数开始输出，就能得到最后的答案。

## 代码 - 桶排序

```csharp
public class Solution {
    public string LargestNumber(int[] nums)
    {
        List<string>[] ans = new List<string>[10];
        for (int i = 0; i < 10; i++)
            ans[i] = new List<string>();

        for (int i = 0; i < nums.Length; i++)
        {
            AddToStringList(ans, nums[i].ToString());
        }

        StringBuilder res = new StringBuilder();

        for (int i = 9; i >= 1; i--)
        {
            List<string> r = ans[i];
            for (int j = 0; j < r.Count; j++)
                res.Append(r[j]);
        }
        if (res.Length == 0)
        {
            if (ans[0].Count == 0) return "";
            else return "0";
        }
        else
        {
            for (int j = 0; j < ans[0].Count; j++)
                res.Append(ans[0][j]);
        }
        return res.ToString();
    }

    private void AddToStringList(List<string>[] ans, string num)
    {
        char[] chs = num.ToCharArray();
        int index = chs[0] - '0';

        List<string> list = ans[index];
        index = list.Count;
        for (int i = 0; i < list.Count; i++)
        {
            if (Compare(num, list[i]) >= 0)
            {
                index = i;
                break;
            }
        }
        list.Insert(index, num);
    }

    private int Compare(string x, string y)
    {
        string order1 = x + y;
        string order2 = y + x;

        for (int i = 0; i  < order1.Length; i++)
        {
            if (order1[i] == order2[i]) continue;
            else if (order1[i] > order2[i]) return 1;
            else return -1;
        }
        return 0;
    }
}
```

## 思路 - Array.Sort

还有一种方式可以借助Array.Sort提供的排序方式来进行排序。这里只要实现自己的`IComparer<string>` 类型就可以。

## 代码 - Array.Sort

```csharp
public class Solution {
    public string LargestNumber(int[] nums)
    {
        string[] strs = new string[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            strs[i] = nums[i].ToString();
        }

        Array.Sort(strs, new LargerNumberComparator());

        if (strs[0] == "0") return "0";
        StringBuilder ans = new StringBuilder();
        for (int i = 0; i < strs.Length; i++)
        {
            ans.Append(strs[i]);
        }

        return ans.ToString();
    }
    private class LargerNumberComparator : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string order1 = x + y;
            string order2 = y + x;

            for (int i = 0; i < order1.Length; i++)
            {
                if (order1[i] == order2[i]) continue;
                else if (order1[i] > order2[i]) return -1;
                else return 1;
            }
            return 0;
        }
    }
}
```
