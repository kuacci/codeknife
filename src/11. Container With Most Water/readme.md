# [Medium][11. Container With Most Water](https://leetcode.com/problems/container-with-most-water/)

Given n non-negative integers a1, a2, ..., an , where each represents a point at coordinate (i, ai). n vertical lines are drawn such that the two endpoints of line i is at (i, ai) and (i, 0). Find two lines, which together with x-axis forms a container, such that the container contains the most water.

Note: You may not slant the container and n is at least 2.

![img](image/question_11.jpg)

The above vertical lines are represented by array [1,8,6,2,5,4,8,3,7]. In this case, the max area of water (blue section) the container can contain is 49.

**Example:**

> Input: [1,8,6,2,5,4,8,3,7]
> Output: 49

## 思路

以数组中的两个值为两条边的高度，这两条竖线之间的距离作为宽，求出最大的蓄水体积。这个里面比较有意思的地方是，水位高度是由最短的那条边决定的，而不是最高的那条。同时宽度（两条边）的距离也要作为参考。暴力算法是2个for循环分别记录2条边的高度，依次计算最大值。
比较优化的算法是用双指针的思路。用left和right分别记录左边界和有边界，计算水的体积。然后较短的一条边向中间移到，再次进行计算。直到left和right重合到一起。

## 代码

```csharp
public class Solution {
    public int MaxArea(int[] height) {
        if (height.Length == 0) return 0;
        if (height.Length == 1) return height[0];

        int left = 0;
        int right = height.Length - 1;
        int max = 0;

        while(left < right)
        {
            max = Math.Max(max, (int)(Math.Min(height[left], height[right]) * (right - left)));

            if (height[left] < height[right])
                left++;
            else
                right--;
        }

        return max;
    }
}
```
