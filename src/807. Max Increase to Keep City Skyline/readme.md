# [Medium][807. Max Increase to Keep City Skyline](https://leetcode.com/problems/max-increase-to-keep-city-skyline/)

In a 2 dimensional array grid, each value grid[i][j] represents the height of a building located there. We are allowed to increase the height of any number of buildings, by any amount (the amounts can be different for different buildings). Height 0 is considered to be a building as well.

At the end, the "skyline" when viewed from all four directions of the grid, i.e. top, bottom, left, and right, must be the same as the skyline of the original grid. A city's skyline is the outer contour of the rectangles formed by all the buildings when viewed from a distance. See the following example.

What is the maximum total sum that the height of the buildings can be increased?

Example:
Input: grid = [[3,0,8,4],[2,4,5,7],[9,2,6,3],[0,3,1,0]]
Output: 35
Explanation:
The grid is:
[ [3, 0, 8, 4],
  [2, 4, 5, 7],
  [9, 2, 6, 3],
  [0, 3, 1, 0] ]

The skyline viewed from top or bottom is: [9, 4, 8, 7]
The skyline viewed from left or right is: [8, 7, 9, 3]

The grid after increasing the height of buildings without affecting skylines is:

gridNew = [ [8, 4, 8, 7],
            [7, 4, 7, 7],
            [9, 4, 8, 7],
            [3, 3, 3, 3] ]

## 思路

这道题的意思是用int[][]组成一个N * M的矩阵，每一个元素代表着大楼的高度从上往下看投射一个一维数组，从右往左看投射另外一个数组。这两个数组分别纪录2个方向上投射下来的最大值。再次扫描这些元素的时候，元素内小于两组值中的较小一个，那么增加到这个数字，并不会影响skyline。记录下来最大可以增加的值，并且返回。

例如上面的这个例子，从上往下扫描，skyline的最大值是

|9|4|8|7|
|:-:|:-:|:-:|:-:|
|-|-|-|-|
|3|0|**8**|4|
|2|**4**|5|**7**|
|**9**|2|6|3|
|0|3|1|0|

从左往右扫描，skyline最大值是

|8|-|3|0|**8**|4|
|:-:|:-:|:-:|:-:|:-:|:-:|
|7|-|2|4|5|**7**|
|9|-|**9**|2|6|3|
|3|-|0|**3**|1|0|

grid[0][0] = 3，Math.Min(9,8) - grid[0][0] = 5; 这个元素上大厦可以增加5。 grid[0][1] = 0，Math.Min(4,8) - grid[0][1] = 4；这个元素上大厦可以增加4。

使用遍历二维数组的方式，第一次分别计算出两个方向的最大值存在两个方向上的一维数组中。第二遍遍历，计算差值。

## 代码

``` csharp
public class Solution {
    public int MaxIncreaseKeepingSkyline(int[][] grid) {
        if(grid == null || grid.Length == 0 || grid.Length == 1) return 0;

        int heigh = grid.Length;
        int weight = grid[0].Length;

        int[] maxHei = new int[heigh];
        int[] maxWei = new int[weight];

        int inc = 0;

        for(int i = 0; i < heigh; i++)
        {
            for(int j = 0; j < weight; j++)
            {
                maxHei[i] = Math.Max(maxHei[i],grid[i][j]);
                maxWei[j] = Math.Max(maxWei[j],grid[i][j]);
            }
        }

        for(int i = 0; i < heigh; i++)
        {
            for(int j = 0; j < weight; j++)
            {
                inc = inc + Math.Min(maxHei[i], maxWei[j]) - grid[i][j];
            }
        }

        return inc;
    }
}
```
