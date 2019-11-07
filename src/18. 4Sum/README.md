# [Medium][18. 4Sum](https://leetcode.com/problems/4sum/)

Given an array nums of n integers and an integer target, are there elements a, b, c, and d in nums such that a + b + c + d = target? Find all unique quadruplets in the array which gives the sum of target.

**Note:**

The solution set must not contain duplicate quadruplets.

**Example:**

Given array nums = [1, 0, -1, 0, -2, 2], and target = 0.

```text
A solution set is:
[
  [-1,  0, 0, 1],
  [-2, -1, 1, 2],
  [-2,  0, 0, 2]
]
```

## 思路 - 双指针

这里可以认为是[3Sum](src/15.%203Sum)的进阶版。按照3Sum的思路，外围套2个循环，作为第4，3个数字.里面的第2和第1个数字用双指针。
另外一个问题是去重。先要计算过一次数字，这样才开始去重。这样做是为了能够涵盖符合target的组合中存在重复数字的情况，例如`nums = [0, 0, 0, 0] target = 0`。

内层的双指针循环，则使用另外的方式去重。如果，已经计算过一次，并且还重复的情况下，`left++`或者`right--`， 最后再检查是`if(left >= right) break;`.

## 代码 - 双指针

```csharp
public class Solution {
    public IList<IList<int>> FourSum(int[] nums, int target) {
        IList<IList<int>> ans = new List<IList<int>>();
        if(nums.Length <= 3) return ans;

        Array.Sort(nums);

        for(int i = 0; i < nums.Length - 3; i++)
        {
            if(i > 0 && nums[i] == nums[i - 1]) continue;
            for(int j = i + 1; j < nums.Length - 2; j++)
            {
                if(j > i + 1 && nums[j] == nums[j - 1]) continue;

                int left = j + 1;
                int right = nums.Length - 1;

                while(true)
                {
                    while(left > j + 1 && left < nums.Length && nums[left] == nums[left - 1]) left++;
                    while(right < nums.Length - 1 && right > 0 && nums[right] == nums[right + 1]) right--;
                    if(left >= right) break;

                    int count = nums[i] + nums[j] + nums[left] + nums[right];
                    if(count == target)
                    {
                        IList<int> an = new List<int>();
                        an.Add(nums[i]);
                        an.Add(nums[j]);
                        an.Add(nums[left]);
                        an.Add(nums[right]);
                        ans.Add(an);
                        left++;
                        right--;
                    }
                    else if(count < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }
        }
        return ans;

    }
}
```
