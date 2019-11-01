# [Medium][81. Search in Rotated Sorted Array II](https://leetcode.com/problems/search-in-rotated-sorted-array-ii/)

Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.

(i.e., [0,0,1,2,2,5,6] might become [2,5,6,0,0,1,2]).

You are given a target value to search. If found in the array return true, otherwise return false.

**Example 1:**

> Input: nums = [2,5,6,0,0,1,2], target = 0
> Output: true

**Example 2:**

> Input: nums = [2,5,6,0,0,1,2], target = 3
> Output: false

**Follow up:**

> This is a follow up problem to Search in Rotated Sorted Array, where nums may contain duplicates.
> Would this affect the run-time complexity? How and why?

## 思路 - 二分法

这道题的思路跟 [# 33. Search in Rotated Sorted Array](src/33.%20Search%20in%20Rotated%20Sorted%20Array) 非常相似。不同的地方在于有重复的内容。所以需要多一个去重的步骤。

由于这个题目时存在2段有序数的可能性。所以要先判断mid的左边有序还是右边有序。判断依据时左右两边边界比nums[mid]大还是小。但如由于重复数的存在，当`nums[left] == nums[mid]`出现时，就无法判断了。这个时候，left就往右移动一位。

一开始我想通过`while(nums[left] == nums[mid]) left++;`，一直移动多个位置直到不等。然而发现可能left会一直移动到mid的右端的可能性。代码量会增加。例如`11131`的情况。为了减少代码量，每次只移动一位，然后再重新计算.

```csharp
while(left <= right){
    int mid = (left + right) / 2;
    if(nums[mid] == target) return true;

    if(nums[left] == nums[mid])
    {
        left++;
        continue;
    }
    // ....
}
```

由于可能存在2段有序数组，通过边界值来跨过中间翻转的位置。

```csharp
//前半部分有序
if(nums[left] < nums[mid])
{
    //target在前半部分
    if(nums[mid] > target && nums[left] <= target)
    {
        right = mid - 1;
    }
    else    // 在右侧寻找
    {
        left = mid + 1;
    }
}
else    // 右侧有序
{
    // target 在右侧
    if(nums[mid] < target && nums[right] >= target)
    {
        left = mid + 1;
    }
    else    // 在左侧寻找
    {
        right = mid - 1;
    }
}
```

## 代码 - 二分法

```csharp
public class Solution {
    public bool Search(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return false;
        int left = 0, right = nums.Length - 1;

        while(left <= right){
            int mid = (left + right) / 2;
            if(nums[mid] == target) return true;

            if(nums[left] == nums[mid])
            {
                left++;
                continue;
            }
            //前半部分有序
            if(nums[left] < nums[mid])
            {
                //target在前半部分
                if(nums[mid] > target && nums[left] <= target)
                {
                    right = mid - 1;
                }
                else    // 在右侧寻找
                {
                    left = mid + 1;
                }
            }
            else    // 右侧有序
            {
                // target 在右侧
                if(nums[mid] < target && nums[right] >= target)
                {
                    left = mid + 1;
                }
                else    // 在左侧寻找
                {
                    right = mid - 1;
                }
            }
        }
        return false;
    }

}
```

## 思路 - 寻找翻转点

这个数组可能沿着某个点翻转，变成了2个有序的数组。那么可以用先找到这个翻转的点，就能知道target在哪一段有序数组中。再进行二分法查找即可。

寻找翻转点的逻辑再`FindPivot`中。为了去重，如果左右两数相等，那么无法判断时放入左边还是右边。做法时两边分别做尝试，总有一款适合你。如果两边都没找到一个翻转点，那么就可能时是一个有序数组。可以直接做二分了。

```csharp
 else // nums[low] == nums[high]
{
    int mid = (low + high) / 2;

    var pivot = FindPivot(nums, low, mid);

    if (pivot != -1)
    {
        return pivot;
    }

    return FindPivot(nums, mid, high);
}
```

所谓的翻转点有这样的特征 `nums[low] > nums[high]`. 只要找到low的位置即可。

```csharp
if (high - low == 1)
{
    return nums[low] > nums[high] ? low : -1;
}
```


## 代码 - 寻找翻转点

```csharp
public class Solution {
    public bool Search(int[] nums, int target) {
        if (nums == null || nums.Length == 0)
        {
            return false;
        }

        int n = nums.Length;
        int pivot = FindPivot(nums, 0, n - 1);

        int low = 0;
        int high = n - 1;

        if (pivot != -1)
        {
            if (target >= nums[0])
            {
                high = pivot;
            }
            else
            {
                low = pivot + 1;
            }
        }

        while (low <= high)
        {
            int mid = (low + high) / 2;

            if (nums[mid] == target)
            {
                return true;
            }
            else if (nums[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        return false;
    }

    private static int FindPivot(int[] nums, int low, int high)
    {
        if (low == high)
        {
            return -1;
        }

        if (high - low == 1)
        {
            return nums[low] > nums[high] ? low : -1;
        }

        if (nums[low] < nums[high])
        {
            return -1;
        }
        else if (nums[low] > nums[high])
        {
            int mid = (low + high) / 2;

            if (nums[mid] >= nums[low])
            {
                return FindPivot(nums, mid, high);
            }
            else
            {
                return FindPivot(nums, low, mid);
            }
        }
        else // nums[low] == nums[high]
        {
            int mid = (low + high) / 2;

            var pivot = FindPivot(nums, low, mid);

            if (pivot != -1)
            {
                return pivot;
            }

            return FindPivot(nums, mid, high);
        }
    }
}
```
