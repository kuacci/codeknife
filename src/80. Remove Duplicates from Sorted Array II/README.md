# [Medium][80. Remove Duplicates from Sorted Array II](https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/)

Given a sorted array nums, remove the duplicates in-place such that duplicates appeared at most twice and return the new length.

Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.

**Example 1:**

> Given nums = [1,1,1,2,2,3],
> Your function should return length = 5, with the first five elements of nums being 1, 1, 2, 2 and 3 respectively.
> It doesn't matter what you leave beyond the returned length.

**Example 2:**

> Given nums = [0,0,1,1,1,1,2,3,3],
> Your function should return length = 7, with the first seven elements of nums being modified to 0, 0, 1, 1, 2, 3 and 3 respectively.
> It doesn't matter what values are set beyond the returned length.

**Clarification:**

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

## 思路 - 逐位比较

这题的要求是最多保留2个重复的数字，必须原地替换掉多余的数字，最后输出一个新的数组的长度。由于传进来的数组是一个引用地址，所以在外部也能获得这个数组的内容，并且按照传回去的数组的长度来判断内容是否正确。这就要求了原地替换数组内容。

因为要求最多保留2个重复数组，所以需要2个指针分别指向前两位的值，如果前两位的值和第三位的值一样，就要缩短数组的长度。

```csharp
for(int i = 2; i < nums.Length; i++)
{
    if(nums[i] == nums[i - 1] && nums[i] == nums[i - 2])
    {
        len--;
    }
}
```

完成这一步，就能正确的计算出数组的长度。但是还有一个要求，就是删除重复的数字，而且要求是原地替换。读了3遍以后终于明白它的意思。即，要求将后面的数字全部往前移动，这样覆盖掉前面的数字。

例如下面这个例子，在`nums[2] ~~ nums[4]`这3个位置，重复出现了数字`1`. 做法就是将`nums[5]`直到`nums[7]`的数字往前挪一位。

```text
[0,0,1,1,1,2,3,4]
[0,0,1,1,2,3,4,4]
```

但是这样也有个问题，就是数字的最后一位无法被删除。位了解决这个问题，可以选择无视最后一位的做法。即，在后续的计算过程中，边界值从`nums[7]`减少到`nums[6]`. 反正最后超出新数组长度的部分，也是不看的。为了达到这个效果，需要进行小的修改。

第一层for循环的结束条件从`i < nums.Length;`修改为`i < len;`。结束的位置为新数组的有效长度，而不是Nums的长度。这样，每次挪动数字的时候，最后一位就被忽略，可以解决了最后一位的处理问题。

由于替换了当前位置，所以下一次判断还需要对当前位置重新计算。所以需要调用`i--`.

```csharp
for(int i = 2; i < len; i++)
{
    if(nums[i] == nums[i - 1] && nums[i] == nums[i - 2])
    {
        len--;
        for(int j = i; j < len - 1; j++)
        {
            nums[j] = nums[j + 1];
        }
        i--;
    }
}
```

时间复杂度：O(N) - O(N^2). 取决于重复数字的次数，导致了内循环部分挪动次数不可预期。最好的情况是没有重复数字，那么不需要挪动。最坏的情况是所有的数字都重复，虽然每次挪动长度都会减少1, 但是总的来说最后一位会被移动N次。所有最坏为O(N^2).
空间复杂度：O(1)

## 代码 - 逐位比较

```csharp
public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if(nums.Length <= 2) return nums.Length;
        int len = nums.Length;

        for(int i = 2; i < len; i++)
        {
            if(nums[i] == nums[i - 1] && nums[i] == nums[i - 2])
            {
                for(int j = i; j < len - 1; j++)
                {
                    nums[j] = nums[j + 1];
                }
                len--;
                i--;
            }
        }

        return len;
    }
}
```

## 思路 - 双指针

上面的思路在于频繁的挪动数字的时候会导致时间复杂度的升高。所有可以优化的部分是挪动数字的情况。这个时候可以借助双指针来解决。用一个指针`int i`来做指针扫描数组。再用另外一个指针`int cur`来指向当前数组的位置。如果符合当前数字与之前两个数字不一样的这个条件，`i`和`cur`一起移动，并且增加len的长度。如果`cur`所在位置的数字与前面两个数字均相等的情况，要cur要停留再当前位置，等待`i`位置的数字的替换。直到i移动到最后，那么完成整个过程。

## 代码 - 双指针

```csharp
public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if(nums.Length <= 2) return nums.Length;
        int len = 2;
        int cur = 2;

        for(int i = 2; i < nums.Length; i++)
        {
            nums[cur] = nums[i];
            if(nums[cur] != nums[cur - 1] || nums[cur] != nums[cur - 2])
            {
                cur++;
                len++;
            }
        }

        return len;
    }
}
```
