# [Easy][26. Remove Duplicates from Sorted Array](https://leetcode.com/problems/remove-duplicates-from-sorted-array/)

Given a sorted array nums, remove the duplicates in-place such that each element appear only once and return the new length.

Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.

**Example 1:**

Given nums = [1,1,2],

Your function should return length = 2, with the first two elements of nums being 1 and 2 respectively.

It doesn't matter what you leave beyond the returned length.

**Example 2:**

Given nums = [0,0,1,1,1,2,2,3,3,4],

Your function should return length = 5, with the first five elements of nums being modified to 0, 1, 2, 3, and 4 respectively.

It doesn't matter what values are set beyond the returned length.
Clarification:

Confused why the returned value is an integer but your answer is an array?

Note that the input array is passed in by reference, which means modification to the input array will be known to the caller as well.

Internally you can think of this:

```csharp
// nums is passed in by reference. (i.e., without making a copy)
int len = removeDuplicates(nums);

// any modification to nums in your function would be known by the caller.
// using the length returned by your function, it prints the first len elements.
for (int i = 0; i < len; i++) {
    print(nums[i]);
}
```

## 思路 - 双指针 + 原地替换

这题目的要求是去重。如果有重复的数字，需要后后面不重复的数字挪道前面来，然后返回一个新的数组的长度，将不重复的数字全部输出。例如`[1,2,2,3]`, 不重复的数字一共有3个，所有新的数组长度是3.如何将不重复的数字替换上来呢？因为重复的是数字2,操作的时候是将不重复的3替换上来，形成新的数组内容为`[1,2,3,3]`. 返回新数组长度是3，截断为`[1,2,3]`.
在例如`[0,0,1,1,1,2,2,3,3,4]`, 最后的结果应该是`[0,1,2,3,4,2,2,3,3,4]`, length = 4;

做法是使用双指针。初始化的时候双指针分别指向前后一个数字, `left = 0, right = 1`. 开始的时候先比较`nums[left]` 是否与`nums[right]`相等。如果相等，right继续移动，直到两数不等的时候，将right所指向的数字复制道left+1的位置。将left指向下一个位置，然后继续进行比较。

时间复杂度：O(N), 数组遍历一次。
空间复杂度：O(1), 原地替换。

## 代码 - 双指针 + 原地替换

```csharp
public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if(nums.Length <= 1) return nums.Length;
        int ans = 1;
        for(int left = 0, right = 1; right < nums.Length; right++)
        {
            if(nums[left] != nums[right])
            {
                ans ++;
                nums[++left] = nums[right];
            }
        }
        return ans;
    }
}
```
