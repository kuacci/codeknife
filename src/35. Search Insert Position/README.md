# [Easy][35. Search Insert Position](https://leetcode.com/problems/search-insert-position/)

Given a sorted array and a target value, return the index if the target is found. If not, return the index where it would be if it were inserted in order.

You may assume no duplicates in the array.

**Example 1:**

> Input: [1,3,5,6], 5
> Output: 2

**Example 2:**

> Input: [1,3,5,6], 2
> Output: 1

**Example 3:**

> Input: [1,3,5,6], 7
> Output: 4

**Example 4:**

> Input: [1,3,5,6], 0
> Output: 0

## 思路 - 二分法

1. 先确认`target`是否在`nums`范围之外，如果是的话，按照他的情况返回0或者nums.Length.
2. 采用二分法的方式向中间逼近。
   1. 如果`nums[mid] == target`, 说明已经找到了。
   2. 如果`nums[mid] > target`, 而`nums[mid - 1] < target`, 说明target介于2个数之间，那么在数组中不存在这个数，按照题设，要返回插入位置的值，即mid.
   3. 否则的话按照二分法的思路往左移或者往右移动
3. 如果上面都不成立，当 `l > r`的时候退出。不过这种情况是不会发生的。

时间复杂度：O(LogN), 二分法的时间复杂度
空间复杂度：O(1)

## 代码 - 二分法

```csharp
public class Solution {
    public int SearchInsert(int[] nums, int target) {

        if(nums[0] > target) return 0;
        if(nums[nums.Length - 1] < target) return nums.Length;

        int l = 0, r = nums.Length - 1;
        while(l <= r)
        {
            int mid = (l + r) / 2;
            if(nums[mid] == target) return mid;
            else if(mid > 0 && nums[mid] > target && nums[mid - 1] < target)
            {
                return mid;
            }
            else if(nums[mid] > target)
            {
                r = mid - 1;
            }
            else
            {
                l = mid + 1;
            }
        }
        return -1;
    }
}
```
