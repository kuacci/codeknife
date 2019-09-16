# 1029. Two City Scheduling

There are 2N people a company is planning to interview. The cost of flying the i-th person to city A is costs[i][0], and the cost of flying the i-th person to city B is costs[i][1].

Return the minimum cost to fly every person to a city such that exactly N people arrive in each city.

Example 1:

```text
Input: [[10,20],[30,200],[400,50],[30,20]]
Output: 110
Explanation:
The first person goes to city A for a cost of 10.
The second person goes to city A for a cost of 30.
The third person goes to city B for a cost of 50.
The fourth person goes to city B for a cost of 20.

The total minimum cost is 10 + 30 + 50 + 20 = 110 to have half the people interviewing in each city.
```

Note:

1. 1 <= costs.length <= 100
2. It is guaranteed that costs.length is even.
3. 1 <= costs[i][0], costs[i][1] <= 1000

## 思路

这道题是给出一个二维数组， [i][0] 是飞往A城市的费用， [i][1] 是飞往 B 城市的费用。一个人只能飞往一个城市。同时必须一半人去A城市，另外一半人去B城市。要求总费用最低。
这里面有几个点。一个人要么去A要么去B。去A或者B的人数必须相等。
踩到的坑。我一开始觉得应该哪边钱少就去哪边，后来立刻发现不对。这样无法保证A和B的人数必须相等的条件。为了保证人数相等，必须在初步筛选之后，从人数多的那边选择比较划算的情况重新安排。所谓的比较划算，就是从一个城市挪到另外一个城市成本最低的人。

所以我的方案是分步走。

1. 建立2个List, 分别保存去A和B的人数。判断依据是哪边花钱少去哪边。
2. 建立这个list的时候进行排序。这个排序是为了下一个步骤挪人到另外一个list做准备。依据是，付出的成本最少。
   例如有2个人在A城市的List里面。

$$
\left[
 \begin{matrix}
   200 & 1000 & \\
   400 & 1400 &
  \end{matrix}
\right]
$$

   [200,1000]
   [400,1400]
   1st 放弃去A改去B需要多花 1000 - 200 = 800. 2nd 则需要多花 1400 - 400 = 1000. 显然是让1st 去比较划算。
3. 把人多的一组，从顶开始将人挪到另外一组，直到人数平衡。

## 代码

```csharp

public class Solution {
        public int TwoCitySchedCost(int[][] costs)
        {
            int ans = 0;

            List<int[]> left = new List<int[]>();
            List<int[]> right = new List<int[]>();

            foreach(var c in costs)
            {
                if(c[0] < c[1])
                    Insert(left, c, 0, 1);
                else
                    Insert(right, c, 1, 0);
            }

            while(left.Count != right.Count)
            {
                if(left.Count > right.Count)
                {
                    right.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    left.Add(right[0]);
                    right.RemoveAt(0);
                }
            }

            foreach (var item in left)
            {
                ans += item[0];
            }

            foreach (var item in right)
            {
                ans += item[1];
            }

            return ans;
        }

        private void Insert(List<int[]> lst, int[] ts, int idxa, int idxb)
        {
            int val = ts[idxa] - ts[idxb];

            for (int i = 0; i < lst.Count; i++)
            {
                int cmp = lst[i][idxa] - lst[i][idxb];

                if (val > cmp)
                {
                    lst.Insert(i, ts);
                    return;
                }
            }
            lst.Add(ts);
        }

}
```
