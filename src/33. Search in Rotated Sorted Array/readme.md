# [Medium][33. Search in Rotated Sorted Array](https://leetcode.com/problems/search-in-rotated-sorted-array/)

Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.

(i.e., [0,1,2,4,5,6,7] might become [4,5,6,7,0,1,2]).

You are given a target value to search. If found in the array return its index, otherwise return -1.

You may assume no duplicate exists in the array.

Your algorithm's runtime complexity must be in the order of O(log n).

**Example 1:**

> Input: nums = [4,5,6,7,0,1,2], target = 0
> Output: 4

**Example 2:**

> Input: nums = [4,5,6,7,0,1,2], target = 3
> Output: -1

## 思路 - 寻找Povit

这道题目是在数组中找到一个值，看看是否存在。一个比较正常的查找。不过问题在于有序的数组可能沿着某个位置被翻转了。

`[0,1,2,4,5,6,7]` 沿着int[4]这个位置被翻转成了 `[4,5,6,7,0,1,2]`。 所以先寻找到Povit，确认了有序数组的情况。然后再用二分法找到是否存在target。

寻找Povit有几个cases：

1. Povit 在 0， 数组没有发生翻转。
2. Povit 不在 0， 左边界会比有边界要高。先用二分，如果`int[mid] > int[lo]`, povit在翻转位置的高位，还要往右走。如果`int[mid] < int[lo]` 则在位置的低位，往左走。直到`int[mid - 1]  > int[mid]`, 此时的mid就是Povit.

找到Povit之后剩下的就是二分查找了。

## 代码 - 寻找Povit

```csharp
public class Solution
{
    public int Search(int[] nums, int target)
    {
        if (nums.Length == 0) return -1;
        if (nums.Length == 1) return nums[0] == target ? 0 : -1;

        int pivot = FindPivot(nums);
        int lo = 0;
        int hi = 0;

        if (pivot == 0)
        {
            lo = 0;
            hi = nums.Length - 1;
        }
        else if (target >= nums[pivot] && target <= nums[nums.Length - 1])
        {
            lo = pivot;
            hi = nums.Length - 1;
        }
        else if (target >= nums[0] && target <= nums[pivot - 1])
        {
            lo = 0;
            hi = pivot - 1;
        }
        else
            return -1;

        return BinarySearch(nums, target, lo, hi);
    }

    private int FindPivot(int[] nums)
    {
        int left = nums[0];
        int right = nums[nums.Length - 1];

        if(left < right) return 0;

            int lo = 0;
            int hi = nums.Length - 1;
            int mid = 0;

            while (lo <= hi)
            {
                mid = lo + (hi - lo) / 2;

                if (mid > 0 && nums[mid] < nums[mid - 1])
                    return mid;

                if (nums[mid] <= right)
                {
                    hi = mid - 1;
                }
                else if (nums[mid] >= left)
                {
                    lo = mid + 1;
                }
            }

            return mid;


        }

        private int BinarySearch(int[] nums, int target, int lo, int hi)
        {
            if (lo > hi) return -1;

            int mid = lo + (hi - lo) / 2;

            if (nums[mid] == target)
                return mid;
            else if (nums[mid] > target)
            {
                return BinarySearch(nums, target, lo, mid - 1);
            }
            else    // nums[mid] < target
            {
                return BinarySearch(nums, target, mid + 1, hi);
            }
        }
    }
```
