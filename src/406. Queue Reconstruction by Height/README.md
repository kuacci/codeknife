# [Medium][406. Queue Reconstruction by Height](https://leetcode.com/problems/queue-reconstruction-by-height/)

Suppose you have a random list of people standing in a queue. Each person is described by a pair of integers (h, k), where h is the height of the person and k is the number of people in front of this person who have a height greater than or equal to h. Write an algorithm to reconstruct the queue.

**Note:**
The number of people is less than 1,100.

**Example：**

```text
Input:
[[7,0], [4,4], [7,1], [5,0], [6,1], [5,2]]

Output:
[[5,0], [7,0], [5,2], [6,1], [4,4], [7,1]]
```

## 思路 - 贪心算法

题目的要求是重组数组的排序。按照int的定义为`int[h,k]`， h代表身高，k代表着前面有多少人比他高。所有可以理解为按照身高进行降序排序，然后再按照k升序排序。
所有先将数组按照上面的方式进行排序。然后再按照k的位置插入。

```csharp
Array.Sort(people, (x, y) => x[0] == y[0]
                            ? x[1] - y[1]       // 如果身高相等，按照k的位置进行升序排序，因为插入的时候是按照K进行插入，要确保`k <= list.Lenght - 1`
                            : y[0] - x[0]);     // 如果身高不相等，按照身高逆序排序，将高的放在前面

```

## 代码 - 贪心算法

```csharp
public class Solution {
    public int[][] ReconstructQueue(int[][] people)
    {
        Array.Sort(people, (x, y) => x[0] == y[0]
                            ? x[1] - y[1]       // 如果身高相等，按照k的位置进行升序排序，因为插入的时候是按照K进行插入，要确保`k <= list.Lenght - 1`
                            : y[0] - x[0]);     // 如果身高不相等，按照身高逆序排序，将高的放在前面
        List<int[]> list = new List<int[]>();
        foreach (var p in people)
        {
            list.Insert(p[1], p);   // 按照k的位置插入
        }
        return list.ToArray();
    }
}
```
