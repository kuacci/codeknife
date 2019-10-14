# [34. Find First and Last Position of Element in Sorted Array](https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/)

Given an array of integers nums sorted in ascending order, find the starting and ending position of a given target value.

Your algorithm's runtime complexity must be in the order of O(log n).

If the target is not found in the array, return [-1, -1].

**Example 1:**

> Input: nums = [5,7,7,8,8,10], target = 8
> Output: [3,4]

**Example 2:**

> Input: nums = [5,7,7,8,8,10], target = 6
> Output: [-1,-1]

## 思路 - Binary Search

要求在一个有序数组种找到目标数字的位置，如果这个数字有重复，那么给出这个数字的起始位置和结束位置。如果没有找到则返回[-1,-1]. 这里要求时间复杂度是 O(lgN). 这个条件其实是在提是我们用什么查找算法。基本上可以定位是用Binary Search.
通过Binary 找到位置后，再进行下一步的检查寻找重复数字的开始和结束位置。Start位置从binary search返回的posw位置开始往回查找，End位置则相反。

时间复杂度 ： Binary Search的时间复杂度是 O(lgN), 后面的寻找重复数字的开头和结尾是O(m) (0 <= m <= N).

## 代码 - Binary Search

```csharp
public class Solution {
    public int[] SearchRange(int[] nums, int target) {

        int[] ans = new int[2];
        if(nums == null || nums.Length == 0)
        {
            ans[0] = -1;
            ans[1] = -1;
            return ans;
        }

        int pos = BinarySearch(nums, target, 0, nums.Length - 1);

        if(nums[pos] == target)
        {
            int lo = pos;
            while(lo > 0 && nums[lo - 1] == target) lo -= 1;
            int hi = pos;
            while(hi < nums.Length -1 && nums[hi + 1] == target) hi += 1;

            ans[0] = lo;
            ans[1] = hi;

        }
        else
        {
            ans[0] = -1;
            ans[1] = -1;
        }
        return ans;
    }

    private int BinarySearch(int[] nums, int target, int lo, int hi)
    {
        if(lo >= hi) return lo;

        int mid = lo + (hi - lo) / 2;

        if(nums[mid] == target)
            return mid;
        else if(nums[mid] < target)
            return BinarySearch(nums, target, mid + 1, hi);
        else return BinarySearch(nums, target, lo, mid - 1);
    }
}
```

## 思路 - Binary Search 优化

上面的方法，还存在一些可以优化的地方，时间复杂度 ： Binary Search的时间复杂度是 O(lgN), 后面的寻找重复数字的开头和结尾是O(m) (0 <= m <= N).
最坏的情况下如果重复数字一直延续到数字的开头或者结尾，那么时间复杂度其实是O(N)，已经超出了题设。
为了达到更好的效率，其实可以同样采用Binary Search的方法。这个涉及到Binary Search的返回结果。

```csharp
    private int BinarySearch(int[] nums, double val)
    {
        int l = 0;
        int r = nums.Length;
        int mid = 0;
        while (l < r)
        {
            mid = (l + r) / 2;

            if (nums[mid] < val)
            {
                l = mid + 1;
            }
            else if (nums[mid] > val)
            {
                r = mid;
            }
            else
                return mid;
        }

        return l;
    }
```

可以利用这一点，再找起始数字位置的时候，可以搜索比target略小一点的值，如 `target - 0.1`。相对的，找结束位置的时候，可以寻找`target + 0.1`。

## 代码 - Binary Search - 优化

```csharp
public class Solution {
    public int[] SearchRange(int[] nums, int target) {
        if (nums.Length == 0)
        {
            return new int[] {-1,-1};
        }

        var index = BinarySearch(nums, target);
        if (index > nums.Length - 1 || nums[index] != target)
            return new int[] {-1,-1};

        var start = BinarySearch(nums, target - 0.1);
        var end = BinarySearch(nums, target + 0.1);

        return new int[] {start, end - 1};
    }

    private int BinarySearch(int[] nums, double val)
    {
        int l = 0;
        int r = nums.Length;
        int mid = 0;
        while (l < r)
        {
            mid = (l + r) / 2;

            if (nums[mid] < val)
            {
                l = mid + 1;
            }
            else if (nums[mid] > val)
            {
                r = mid;
            }
            else
                return mid;
        }

        return l;
    }
}
```
