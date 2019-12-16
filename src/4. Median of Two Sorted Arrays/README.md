# [Hard][4. Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/)

There are two sorted arrays nums1 and nums2 of size m and n respectively.

Find the median of the two sorted arrays. The overall run time complexity should be O(log (m+n)).

You may assume nums1 and nums2 cannot be both empty.

**Example 1:**

> nums1 = [1, 3]
> nums2 = [2]
> the median is 2.0

**Example 2:**

> nums1 = [1, 2]
> nums2 = [3, 4]
> The median is (2 + 3)/2 = 2.5

## 思路 - 辅助数组 - 时间复杂度O(m+n)

中位数的定义在[这里](https://baike.baidu.com/item/%E4%B8%AD%E4%BD%8D%E6%95%B0). 在一个有序数组中，中位数正好在他的中间位置。所有只要将两个有序数组拼接位一个有序数组，然后求他的中间位置的值即可。

时间复杂度：O(m+n).
空间复杂度：O(m+n).

## 代码 - 辅助数组 - 时间复杂度O(m+n)

```csharp
public class Solution {
    public double FindMedianSortedArrays(int[] nums1, int[] nums2) {
        int[] nums3 = new int[nums1.Length + nums2.Length];

        int n1 = 0, n2 = 0, n3 = 0;

        while(n3 < nums3.Length)
        {
            if(n1 == nums1.Length)
            {
                nums3[n3] = nums2[n2];
                n2++;
            }
            else if(n2 == nums2.Length)
            {
                nums3[n3] = nums1[n1];
                n1 ++;
            }
            else
            {
                if(nums1[n1] < nums2[n2])
                {
                    nums3[n3] = nums1[n1];
                    n1++;
                }
                else
                {
                    nums3[n3] = nums2[n2];
                    n2++;
                }
            }
            n3++;
        }

        return (nums3[(nums3.Length - 1) / 2] + nums3[nums3.Length / 2]) * 1.0 / 2;

    }
}
```
