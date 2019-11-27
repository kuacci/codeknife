# [Medium][134. Gas Station](https://leetcode.com/problems/gas-station/)

There are N gas stations along a circular route, where the amount of gas at station i is gas[i].

You have a car with an unlimited gas tank and it costs cost[i] of gas to travel from station i to its next station (i+1). You begin the journey with an empty tank at one of the gas stations.

Return the starting gas station's index if you can travel around the circuit once in the clockwise direction, otherwise return -1.

Note:

* If there exists a solution, it is guaranteed to be unique.
* Both input arrays are non-empty and have the same length.
* Each element in the input arrays is a non-negative integer.

**Example 1:**

```text
Input:
gas  = [1,2,3,4,5]
cost = [3,4,5,1,2]

Output: 3

Explanation:
Start at station 3 (index 3) and fill up with 4 unit of gas. Your tank = 0 + 4 = 4
Travel to station 4. Your tank = 4 - 1 + 5 = 8
Travel to station 0. Your tank = 8 - 2 + 1 = 7
Travel to station 1. Your tank = 7 - 3 + 2 = 6
Travel to station 2. Your tank = 6 - 4 + 3 = 5
Travel to station 3. The cost is 5. Your gas is just enough to travel back to station 3.
Therefore, return 3 as the starting index.
```

## 思路 - 差值计算

题设给出两个数组 `gas[]` 和 `cost[]`. 从某一个点开始从左往右遍历，如果走到头，又回到左边，一直要走到起点位置。每走一步获得`gas[]`的汽油并且消耗`cost[]`的汽油。判断是否能走一圈。

这里的问题难点在于：

1. 汽油的获得和消耗。
2. 是否存在解。
3. 走到头要转回来。

对于1#， 每走一个都存在消耗和获得，可以将`gas[]` 和 `cost[]`的数字合并到`gas[]`中。这样走到某一个i所获得的收益就一目了然。

```charp
for(int i = 0; i < gas.Length; i++)
{
    gas[i] -= cost[i];
}
```

对于是否 存在解，可以将所有的`gas[]` 和 `cost[]`加起来看看是否`>=0`就能知道。

```charp
int sum = 0;
for(int i = 0; i < gas.Length; i++)
{
    gas[i] -= cost[i];
    sum += gas[i];
}
if(sum < 0) return -1;
```

对于3#。 采用双指针的方法进行计算。由于这个时候`gas[]`里面存放的是当前一步的收益或者损失，已经是差值。所有可以放置两个指针left和right. left是作为起始位置，right作为现在已经走过的步数。right先扩张，如果left 到 right之间的值 > 0, 说明以left为起点走到right是剩余汽油的，可以继续向右。如果这个时候`sum < 0`, 代表这个时候的left已经不足以满足汽油的需求了，left++, 直到sum重新 >= 0为止。这个计算一直持续到right == gas.Length - 1. 这个时候返回left。

时间复杂度：O(N), 计算差值的时候gas和 cost遍历一次O(N), 用双指针寻找起始位置的时候，gas[]的元素会被touch 1 - 2次, O(2N). 所有为O(N).
空间复杂度：O(1), 借助了临时变量。

## 代码 - 差值计算

```csharp
public class Solution {
    public int CanCompleteCircuit(int[] gas, int[] cost) {
        if(gas.Length == 0) return -1;

        int sum = 0;
        for(int i = 0; i < gas.Length; i++)
        {
            gas[i] -= cost[i];
            sum += gas[i];
        }
        if(sum < 0) return -1;

        int left = 0;
        int right = 0;
        sum = gas[0];
        while(true)
        {
            if(sum >= 0)
            {
                right ++;
                if(right >= gas.Length) break;
                sum += gas[right];
            }
            else
            {
                if(left == right)
                {
                    left ++;
                    right ++;
                    sum = gas[right];
                }
                else
                {
                    sum -= gas[left];
                    left++;
                }
            }
        }

        return left;
    }
}
```

## 思路 - 差值计算 - 优化

上面的算法能够到达一个效果，如果有多个加油站可以完成绕圈的任务，能够选择出来最靠近左侧的加油站。不过题设并没有这个要求。所有可以做的更加优化一些。
能否完成绕圈还是又一个遍历做完之后，总的剩余的油是否 `>= 0`来确定。加油站的选取采用另外一种方式，用curTank记录从某一点i开始加油后行驶到当前这一点的剩余油量。如果`curTank <= 0` 说明从i开始行使无法开到当前的点`i'`. 这个时候不是从 `i + 1` 重新进行计算，而是从 `i' + 1` 作为新的起点，并且将curTank清空为0. 一直形式到最后一个加油站结束。

## 代码 - 差值计算 - 优化

```csharp
public class Solution {
    public int CanCompleteCircuit(int[] gas, int[] cost) {
        if(gas.Length == 0) return -1;

        int curTank = 0;
        int totalGas = 0;
        int pos = 0;

        for(int i = 0; i < gas.Length; i++)
        {
            int d = gas[i] - cost[i];
            curTank += d;
            totalGas += 0;
            if(curTank < 0)
            {
                curTank = 0;
                pos = i + 1;
            }
        }

        return totalGas >= 0 ? pos : -1;
    }
}
```
