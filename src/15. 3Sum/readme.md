# 15. 3Sum

Given an array nums of n integers, are there elements a, b, c in nums such that a + b + c = 0? Find all unique triplets in the array which gives the sum of zero.

**Note:**

The solution set must not contain duplicate triplets.

**Example:**

```text
Given array nums = [-1, 0, 1, 2, -1, -4],

A solution set is:
[
  [-1, 0, 1],
  [-1, -1, 2]
]
```

## 思路 - 双指针

首先将数组进行排序，是的数组左边小右边大。用`for (int i = 0; i < nums.Length - 2; i++)`来做第一个加数。双指针的方式就是用一个left和一个right分别指向数组的左端和右端，作为另外2个被加数来做3数之和。
所以i的起始位置为0， 终止位置为 num.Length - 3. 因为它的左边最少还有left和right2个指针。每次内循环结束之后 left要重现位于i的左侧，而right要回到数组的右侧。

用双指针要解决一个去重问题。首先是i的去重。当进行过运算之后，即 i > 0, 开始检测去重问题。如果当前i位置的值跟上一个值是一样，就说明上次已经计算过，要移动到下一个位置。Left 和 Right的去重也是类似的思路。

## 代码 - 双指针

```csharp
public class Solution
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {

        var ans = new List<IList<int>>();
        if (nums.Length < 3) return ans;

        Array.Sort(nums);
        int left = 0;
        int right = nums.Length - 1;

        for (int i = 0; i < nums.Length - 2; i++)
        {
            if (nums[i] > 0) break;
            while (i > 0 && nums[i - 1] == nums[i] && i < nums.Length - 1)
            {
                i += 1; // remove duplicate number
            }

            left = i + 1;
            right = nums.Length - 1;
            while (left < right)
            {
                int sum = nums[i] + nums[left] + nums[right];
                if (sum == 0)
                {
                    ans.Add(new List<int> { nums[i], nums[left], nums[right] });

                    do
                    {
                        left++;
                    }
                    while (left < nums.Length - 2 && nums[left - 1] == nums[left]); // remove duplicate number

                    do
                    {
                        right--;
                    }
                    while (right > left && nums[right] == nums[right + 1]); // remove duplicate number
                }
                else if (sum > 0)
                {
                    right--;
                }
                else
                {
                    left++;
                }
            }
        }
        return ans;
    }
}
```
