# [402. Remove K Digits](https://leetcode.com/problems/remove-k-digits/)

Given a non-negative integer num represented as a string, remove k digits from the number so that the new number is the smallest possible.

Note:

* The length of num is less than 10002 and will be ≥ k.
* The given num does not contain any leading zero.

**Example 1:**

> Input: num = "1432219", k = 3
> Output: "1219"
> Explanation: Remove the three digits 4, 3, and 2 to form the new number 1219 which is the smallest.

**Example 2:**

> Input: num = "10200", k = 1
> Output: "200"
> Explanation: Remove the leading 1 and the number is 200. Note that the output must not contain leading zeroes.

**Example 3:**

> Input: num = "10", k = 2
> Output: "0"
> Explanation: Remove all the digits from the number and it is left with nothing which is 0.

## 思路 - 贪心算法 - Stack

这道题的要求是移除掉K个数字之后，保持数字最小，前驱0会被忽略掉。
那么问题来了，如何移除K个数字能够得到最小值。先观察一下下面的数字 ：

|数字|K|结果|
|:-:|:-:|:-|
|123456|K = 1|12345|
|123456|K = 2|1234|
|123456|K = 3|123|

当数字是顺序递增的时候，应该是remove掉最右端的数字，即删除最大的数字。然而这并不是真相的全部。如下面的数字：

|数字|K|结果|
|:-:|:-:|:-|
|124356|K = 1|12356|
|124356|K = 2|1235|
|124356|K = 3|123|

比较发现，只有在逆序的数字出现的时候，删除掉逆序的数字才会使得数字最小。如，124356，4在2和3之间形成了一个突出的山峰，4和3出现了逆序，删除掉4会比删除6能得到更小的值。
我把这个称之为"消除突出部"。可以看更多例子来观察这个结论：

|数字|K|结果|
|:-:|:-:|:-|
|134256|K = 1|13256|
|134256|K = 2|1256|

另外如果引入了0，情况会更加复杂一些

|数字|K|结果|
|:-:|:-:|:-|
|12300456|K = 1|1200456|
|12300456|K = 2|100456|
|12300456|K = 3|00456 = 456|

有0存在的情况下，也遵循“消除突出部”的思路，对于3的位置，由于0的出现会让3变成突出部。同时如果前面的数字能被消除导致前导0的出现，这种情况会是数字快速见效。所有签到0出现的情况下，不能输出。

为了实现这个算法：

1. 采用Stack来记录前面出现过的数字。要保证Stack里面的数字有序，最小值在上面，最大值在下面。
2. 如果`nums[i] < stack.Peek`, 则压栈。反之，则弹出。弹出的元素就是要删除的元素，所有k--.
3. 结束条件是k == 0或者已经走到nums的结尾。
4. Stack里面的值是倒序的，装入返回值之前要再反序输出。

时间复杂度 ：O(2N) = O(N). 虽然套着2个循环，外循环为N, 内循环的Stack其实是会访问有限次. 如果是顺序的数组，Stack会一直增加而不进行pop. 如果是逆序的则会持续Pop. Stack的高度不会增加。这里Stack的循环会保证一个元素最多被访问2次，所有是O(N).
空间复杂度：O(N).

## 代码 - 贪心算法 - Stack

```csharp
public class Solution {
    public string RemoveKdigits(string num, int k)
    {
        if (k == 0) return num.ToString();
        char[] nums = num.ToString().ToCharArray();
        if (nums.Length <= k) return "0";

        StringBuilder ans = new StringBuilder();
        Stack<char> stack = new Stack<char>();
        int i = 0;

        for(; i < nums.Length && k > 0; i++)
        {
            while(stack.Count > 0 && stack.Peek() > nums[i] && k > 0)
            {
                stack.Pop();
                k--;
            }
            if (nums[i] != '0' || stack.Count > 0)
                stack.Push(nums[i]);
        }
        while(k > 0)
        {
            stack.Pop();
            k--;
        }

        char[] chs = new char[stack.Count];

        for (int j = chs.Length - 1; j >= 0; j--)
        {
            chs[j] = stack.Pop();
        }

        ans.Append(chs);
        if(i < nums.Length) ans.Append(nums, i, nums.Length - i);

        return ans.Length == 0 ? "0" : ans.ToString();
    }
}
```
