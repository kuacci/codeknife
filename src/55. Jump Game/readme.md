# [55. Jump Game](https://leetcode.com/problems/jump-game/)

Given an array of non-negative integers, you are initially positioned at the first index of the array.

Each element in the array represents your maximum jump length at that position.

Determine if you are able to reach the last index.

**Example 1:**

> Input: [2,3,1,1,4]
> Output: true
> Explanation: Jump 1 step from index 0 to 1, then 3 steps to the last index.

**Example 2:**

> Input: [3,2,1,0,4]
> Output: false
> Explanation: You will always arrive at index 3 no matter what. Its maximum jump length is 0, which makes it impossible to reach the last index.

## 思路 - 双指针

用`lo`记录当前的指针，用`hi`记录从`lo`开始所能达到的最远的位置，即 `nums[lo] + lo`。然后用计算从`hi`能达到的最远位置，即`num[hi] + hi`。`hi` 递减直到`lo`位置，这样计算出来从`lo`到`hi`所能达到的最远距离。
一直重复这个动作，直到`num[lo] + lo`无法移动为止。即 `num[lo] == 0`. 如果在移动的过程中发现`num[hi] + hi`到达或者超过边界`nums.Length - 1`，就是成功了，返回true.

这个写法虽然看起来有两个循环，然后内循环的距离非常短，介于`lo - nums[lo] + lo` 之间， lo的下一个起跳点是在hi的范围之内。从而保证了数组里面的每个元素被访问的次数是1-2次。所以，时间复杂度为O(N), 空间复杂度为O(1).

## 代码 - 双指针

```csharp
public class Solution {
    public bool CanJump(int[] nums) {

        if(nums.Length == 0) return false;

        int lo = 0;
        while(lo <= nums.Length - 1)
        {
            if(lo + nums[lo] >= nums.Length - 1) return true;
            if(nums[lo] == 0) break;

            int hi = lo + nums[lo];
            int max = 0;
            int mPos = 0;

            for(; hi > lo; hi --)
            {
                if(hi + nums[hi] > max)
                {
                    max = hi + nums[hi];
                    if(max >= nums.Length - 1) return true;
                    mPos = hi;
                }
            }
            lo = mPos;
        }

        return false;

    }
}
```

## 思路 - 双指针 - 优化

上面算法的计算出hi然后倒着走算下一步。另外一种思路是lo一直顺着走，每一步都计算出它最远的移动距离。如果这个最远移动的距离超过了`nums.Length - 1`，那么就成功了。如果`lo`移动到了一个位置，这个位置是之前计算出来的最远距离无法到达的，那么就超出了最远移动范围，说明无法到达这个位置，返回false.
由于移动的是`lo`, 每个元素最多访问一次，时间复杂度为O(N). 虽然同为O(N), 但是要优于上面的方法。

## 代码 - 双指针 - 优化

```csharp
public class Solution {
    public bool CanJump(int[] nums) {

        int hi = 0;
        for(int lo = 0; lo < nums.Length; lo++)
        {
            if(hi >= nums.Length - 1) return true;
            if(lo > hi) return false;
            hi = Math.Max(hi, lo + nums[lo]);
        }

        return true;

    }
}

```
