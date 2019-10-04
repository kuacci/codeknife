# [LeetCode] 215. Kth Largest Element in an Array

Find the kth largest element in an unsorted array. Note that it is the kth largest element in the sorted order, not the kth distinct element.

**Example 1:**

>Input: [3,2,1,5,6,4] and k = 2
Output: 5

**Example 2:**

>Input: [3,2,3,1,2,4,5,5,6] and k = 4
Output: 4

Note:  
You may assume k is always valid, 1 ≤ k ≤ array's length.

## 思路 - 快速排序

找到第K个最大的数，最开始的想法是先对数组做排序， 然后取k - 1的位置即刻。仔细想想这样做毕竟比较慢。因为需要拿到的是第k大的数，并不需要先做排序。回忆一下各大排序算法的原来，快速排序的特定每次partition都能够排定一个数的准确位置。那么我可以参考快排的partition方法，每次排定一个数字，并且返回他的位置p。如果p所在这个位置大于k, 那么第k大的数字应该在这个p的右侧。把p作为低位，重新调用partition。直到k - 1这个位置的被找到。

时间复杂度 : O(nlgn)

## 代码 - 快速排序

``` csharp
public class Solution {
    public int FindKthLargest(int[] nums, int k) {

        if(nums == null || nums.Length == 0) return -1;
        if(nums.Length == 1) return nums[0];

        int part = 0;
        int lo = 0;
        int hi = nums.Length - 1;

        while(true)
        {
            if(lo >= hi) break;

            part = Partition(nums, lo, hi);

            if(part == k - 1) break;

            if(part < k - 1)
            {
                part += 1;
                lo = part;
            }
            else if (part > k - 1)
            {
                part -= 1;
                hi = part;
            }
        }

        return nums[part];

    }

    private int Partition(int[] nums, int lo, int hi)
    {
        int i = lo;
        int j = hi + 1;
        int pivot = nums[lo];

        while(true)
        {
            while(nums[++i] > pivot) if(i==hi) break;
            while(nums[--j] < pivot) if(j==lo) break;

            if(i >= j) break;
            swap(nums, i, j);
        }

        swap(nums, lo, j);
        return j;

    }

    private void swap(int[] nums, int i, int j)
    {
        if (i == j) return;
        int t = nums[i];
        nums[i] = nums[j];
        nums[j] = t;
    }
}
```

## 思路 - 堆排序

快排有点像是碰运气，如果排到的位置正好是k - 1，那么排序就会停下来返回值。这种算法稳定性不太好。排序算法中另外一种排序，可以每次确定一个最大值，即使堆排序。这里是借助堆排序进行k - 1次排序。然后返回nums[0]的值。注：每次Heapify之后，最大的值位于nums[0]的位置。

时间复杂度 : O(nlgn).

## 代码 - 堆排序

```csharp
public class Solution
{
    public int FindKthLargest(int[] nums, int k)
    {
        if (nums.Length == 0) return int.MinValue;
        if (nums.Length == 1) return nums[0];
        return heapSort(nums, k);
    }

    private int Len = 0;
    private int heapSort(int[] nums, int k)
    {
        Len = nums.Length;
        BuildHeap(nums);

        for (int i = nums.Length - 1; i > 0 && --k > 0; i--)
        {
            Swap(nums, 0, i);
            Len--;
            Heapify(nums, 0);
        }

        return nums[0];
    }

    private void BuildHeap(int[] nums)
    {
        for (int i = nums.Length / 2; i >= 0; i--)
        {
            Heapify(nums, i);
        }
    }

    private void Heapify(int[] nums, int i)
    {
        int left = i * 2 + 1;
        int right = i * 2 + 2;
        int max = i;

        if (left < Len && nums[left] > nums[max])
            max = left;
        if (right < Len && nums[right] > nums[max])
            max = right;

        if (max != i)
        {
            Swap(nums, i, max);
            Heapify(nums, max);
        }
    }
    private void Swap(int[] nums, int x, int y)
    {
        int tmp = nums[x];
        nums[x] = nums[y];
        nums[y] = tmp;
    }
}
```
