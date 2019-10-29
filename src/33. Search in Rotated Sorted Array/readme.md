# 33. Search in Rotated Sorted Array

Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
(i.e., [0,1,2,4,5,6,7] might become [4,5,6,7,0,1,2]).
You are given a target value to search. If found in the array return its index, otherwise return -1.
You may assume no duplicate exists in the array.
Your algorithm's runtime complexity must be in the order of O(log n).

Example 1:

```text
Input: nums = [4,5,6,7,0,1,2], target = 0
Output: 4
```

Example 2:

```text
Input: nums = [4,5,6,7,0,1,2], target = 3
Output: -1
```

## 思路

因为有时间复杂度的要求，故只能采用二分查找算法。经分析后可知，旋转后的数组特点就是：元素在某一区间内是有序的。

当 `nums[left] < nums[mid]` 时，mid 左半边一定是有序的。否则右半边有序。此时仅在有序的序列中继续执行二分算法，直到 `target == nums[mid]`。

```csharp
    public int Search(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) >> 1;

            if (target == nums[mid])
            {
                return mid;
            }

            if (nums[left] < nums[mid]) // left part is in-order
            {
                if (target >= nums[left] && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                // right part is in-order

                if (target > nums[mid] && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return -1;
    }
```
