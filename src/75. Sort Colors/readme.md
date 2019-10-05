# [75. Sort Colors](https://leetcode.com/problems/sort-colors/)

Given an array with n objects colored red, white or blue, sort them in-place so that objects of the same color are adjacent, with the colors in the order red, white and blue.

Here, we will use the integers 0, 1, and 2 to represent the color red, white, and blue respectively.

**Note:** You are not suppose to use the library's sort function for this problem.

**Example:**

> Input: [2,0,2,1,1,0]
> Output: [0,0,1,1,2,2]

**Follow up:**

* A rather straight forward solution is a two-pass algorithm using counting sort.
First, iterate the array counting number of 0's, 1's, and 2's, then overwrite array with total number of 0's, then 1's and followed by 2's.
* Could you come up with a one-pass algorithm using only constant space?

## 思路 - 双指针

这里是一道典型的排序题。由于数字出现的范围小，重复的多，一般来说可以计数排序。但是题目要求不能使用计数排序，而且必须一次遍历。那么这种情况下可以使用双指针。用`red` 指针指向数字0的位置，`blue`指向数字2的位置。
用i来遍历数组，当遇到`0`或者`2`的时候，原地与`red`或者`blue`进行交换。`red`自加啊，或者blue自减。在交换的过程中，i交换过来的数字可能还是0或者2,所有i需要自减(i--)，这样多做一次检测，避免错漏。

## 代码 - 双指针

```csharp
public class Solution {
    public void SortColors(int[] nums) {

        int red = 0;
        int blue = nums.Length - 1;

        for(int i = 0; i < nums.Length; i++)
        {
            int val = nums[i];

            if(val == 0 && i > red)
            {
                swap(nums, red++, i--);
            }
            if(val == 2 && i < blue)
            {
                swap(nums, blue--, i--);
            }
        }
    }

    private void swap(int[] nums, int x, int y)
    {
        int temp = nums[x];
        nums[x] = nums[y];
        nums[y] = temp;
        return;
    }
}
```

## 思路 - 双指针 - 优化

上面的算法，i终止于 nums.Length - 1. 但是仔细想想，其实没有必要走到底。当i和blue指针交汇之后，即 i > blue即可停止。blue用来标识2的位置，如果之前出现过2，那么数组右侧就会存在至少1个2，那么不用走到底，也是有序的。如果 没有出现2，blue的初始位置是指向nums.Length - 1。那么i还是会走到数组的底部，不会有遗漏。

## 代码 - 双指针 - 优化

```csharp
public class Solution {
    public void SortColors(int[] nums) {

        int red = 0;
        int blue = nums.Length - 1;

        for(int i = 0; i <= blue; i++)
        {
            int val = nums[i];

            if(val == 0 && i > red)
            {
                swap(nums, red++, i--);
            }
            if(val == 2 && i < blue)
            {
                swap(nums, blue--, i--);
            }
        }
    }

    private void swap(int[] nums, int x, int y)
    {
        int temp = nums[x];
        nums[x] = nums[y];
        nums[y] = temp;
        return;
    }
}
```
