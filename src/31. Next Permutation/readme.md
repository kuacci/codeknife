# [Medium][31. Next Permutation](https://leetcode.com/problems/next-permutation/)

Implement next permutation, which rearranges numbers into the lexicographically next greater permutation of numbers.

If such arrangement is not possible, it must rearrange it as the lowest possible order (ie, sorted in ascending order).

The replacement must be in-place and use only constant extra memory.

Here are some examples. Inputs are in the left-hand column and its corresponding outputs are in the right-hand column.

> 1,2,3 → 1,3,2
> 3,2,1 → 1,2,3
> 1,1,5 → 1,5,1

## 思路

这是一道数学题，首先要分析题目的要求和逻辑，然后再用代码来实现就简单多了。

这里的要求是给出一个数组，要求扫描这个数组之后：

1. 如果这个数组是降序的，那么把他转成升序。
2. 如果1#，不成立，那么装换成以个比之前稍大一点的数字。
3. 算法要求must be in-place and use only constant extra memory. 所有用`hashtable`之类的是不可行的。

例如 `1,2,3`这是一个升序。要转换成一个稍微大一点的数字，即`1,3,2`. 虽然它也可以转换成较大数`3,1,2`或者`3,2,1`，但是这里要求是最小的那个较大数,`132 < 312 < 321`。

我们需要从右往左边扫描数组，找到一个`nums[i] > nums[i - 1]`的位置。这是找到一个违反降序的点。如果能走到头，找不到这个点就是1#，对数组转换成升序。
如果找到了，就是2#的情况。要从`i ~ nums.Length - 1`的范围内找到一个比nums[i - 1]的值稍稍大一点的值，与之进行交换，这样就可以转换成一个比之前要大的数字。

![image](image/figure1.png)

如何找到一个稍微大一点的数字。下面的这个例子看的比较清楚，`1，5，8，4，7，6，5，3，1`.nums[i - 1]出现在数字4这里，它最后会跟7右侧的某个数字交换。这个时候需要向右侧寻找一个比4小的数字的位置，假设这个位置为j. 由于右侧的数字欧式降序的，当找到这个数字的位置的时候，j - 1的位置的值就是比i - 1位置的值稍稍大一点的数字。将 i - 1 和 j - 1的数字交换。右侧的数字依然是降序有序。将他们做reverse操作，就可以得到一个最小的排列。

步骤如下图：

![image](image/figure2.gif)

时间复杂度 ：O(N), 最差的情况下是对元素扫描两遍。
空间复杂度 ：O(1), 没有额外的空间。

## 代码

```csharp
public class Solution {
    public void NextPermutation(int[] nums)
    {
        int i = nums.Length - 2;

        while (i >= 0 && nums[i + 1] <= nums[i]) i--;

        if(i >= 0)
        {
            int j = nums.Length - 1;
            while (j >= 0 && nums[j] <= nums[i]) j--;
            Swap(nums, i, j);
        }

        Reverse(nums, i + 1);
    }
    private void Reverse(int[] nums, int start)
    {
        int end = nums.Length - 1;
        while (start < end)
            Swap(nums, start++, end--);
    }
    private void Swap(int[] nums, int x, int y)
    {
        int tmp = nums[x];
        nums[x] = nums[y];
        nums[y] = tmp;
    }
}
```
