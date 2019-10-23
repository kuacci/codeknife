# [Medium][16. 3Sum Closest](https://leetcode.com/problems/3sum-closest/)

Given an array nums of n integers and an integer target, find three integers in nums such that the sum is closest to target. Return the sum of the three integers. You may assume that each input would have exactly one solution.

**Example:**

> Given array nums = [-1, 2, 1, -4], and target = 1.
> The sum that is closest to the target is 2. (-1 + 2 + 1 = 2).

## 思路 - 双指针

这题的要求是计算一个跟target最接近的结果。这里的思路跟[3Sum](../15.%203Sum)很类似。首先将数组进行排序，是的数组左边小右边大。用`for (int i = 0; i < nums.Length - 2; i++)`来做第一个加数。双指针的方式就是用一个left和一个right分别指向数组的左端和右端，作为另外2个被加数来做3数之和。
所以i的起始位置为0， 终止位置为 num.Length - 3. 因为它的左边最少还有left和right2个指针。每次内循环结束之后 left要重现位于i的左侧，而right要回到数组的右侧。

要计算最接近的值，我选择先计算三数之和与target的差距(`gap`). 如果gap = 0 那么直接返回。如果有gap， 那么需要判断当前计算出来的g与全局的gap的绝对值，取最小的一个。用一个flag来记录这个gap是正值还是负值。最后将结果输出。

## 代码 - 双指针

```csharp
public class Solution {
    public int ThreeSumClosest(int[] nums, int target) {

        int gap = int.MaxValue;
        bool flag = true;   // true : +; false : -
        Array.Sort(nums);

        for(int i = 0; i < nums.Length -2; i++)
        {
            if(gap == 0) break;

            int left = i + 1;
            int right = nums.Length -1;

            while(left < right)
            {
                int g = nums[i] + nums[left] + nums[right] - target;

                if(g == 0) {
                    gap = 0;
                    break;
                }
                else if(g > 0)
                {
                    if(g < gap)
                    {
                        gap = g;
                        flag = true;
                    }
                    right--;
                }
                else    // g < 0
                {
                    g = g * -1;
                    if(g < gap)
                    {
                        gap = g;
                        flag = false;
                    }
                    left++;
                }
            }
        }

        int ans = 0;
        if(flag) {
            ans = target + gap;
        }
        else{
            ans = target - gap;
        }

        return ans;
    }
}
```
