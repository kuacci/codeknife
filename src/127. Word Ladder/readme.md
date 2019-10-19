# [127. Word Ladder](https://leetcode.com/problems/word-ladder/)

Given two words (beginWord and endWord), and a dictionary's word list, find the length of shortest transformation sequence from beginWord to endWord, such that:

1. Only one letter can be changed at a time.
2. Each transformed word must exist in the word list. Note that beginWord is not a transformed word.

**Note:**

* Return 0 if there is no such transformation sequence.
* All words have the same length.
* All words contain only lowercase alphabetic characters.
* You may assume no duplicates in the word list.
* You may assume beginWord and endWord are non-empty and are not the same.

**Example 1:**

> Input:
> beginWord = "hit",
> endWord = "cog",
> wordList = ["hot","dot","dog","lot","log","cog"]
> Output: 5
> Explanation: As one shortest transformation is "hit" -> "hot" -> "dot" -> "dog" -> "cog",
> return its length 5.

**Example 2:**

> Input:
> beginWord = "hit"
> endWord = "cog"
> wordList = ["hot","dot","dog","lot","log"]
> Output: 0
> Explanation: The endWord "cog" is not in wordList, therefore no possible transformation.

## 思路 - BFS (超时)

这道题的要求是给出一个开始的单词，一个结束单词，中间过渡用的单词组。目的是将开始单词过度到结束单词，每次只能转一个字母。
"h**i**t" -> "**h**ot" -> "do**t**" -> "**d**og" -> "cog"

![img](image/figure1.png)

仔细思考后发现这是一道无向无权图种寻找最短可达路径的问题，考虑使用BFS的方式来解题。算法种最重要的步骤是找出相邻的节点，就是找到只差一个字母的两个单词。为了能够快速的找到相邻节点，对wordList的单词组做预处理，将单词组种的单词用MASK '*' 替换掉。

![img](image/figure2.png)

将出来好的单词作为key，将他的原词作为value. 如果有多个单词具备同样的预处理词，那么将他们合并到同一个key下面的value中。所以value要采用`List<string>`类型。例如 ：

Hot 和 Dot会具有同样的预处理单词，当他们的第一位被 `'*'` 替代的时候，看起来都是 `'*ot'`

### 算法思路 - BFS (超时)

1. 对 wordlist做预处理，找出所有的通用状态。将通用状态放入到`Dictionary<string, List<string>>`中。
2. begionWord 和 1放到Queue中。 1代表了level,要是到达endWord的最短距离返回值。
3. 对于图的遍历，是要求记录是否已经被访问的。所有采用一个`List<string>` 标识是否已经被访问过。否则会出现闭环。
4. 将匹配到的新单词放入到Queue中，进入下一轮循环。直到Queue为空（无结果），或者搜索到结束单词。

### 算法复杂度 - BFS (超时)

* 时间复杂度 ： `O(M * N)`, M是单词的长度， N是 单词表的总数量。找到所有的辩护需要对每个单词都做M个预处理单词。最坏的情况下是遍历所有的预处理单词。所有是`O(M * N)`
* 空间复杂度：O(M * N), 要在字典表`Dictionary<string, List<string>> map = new Dictionary<string, List<string>>()`中保存所有的预处理单词即 `M * N` 个单词。

## 代码 - BFS (超时)

```csharp
public int LadderLength(string beginWord, string endWord, IList<string> wordList)
{
    char MASK = '*';

    Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
    foreach (string item in wordList)
    {
        char[] chs = item.ToCharArray();
        for (int i = 0; i < chs.Length; i++)
        {
            char tmp = chs[i];
            chs[i] = MASK;
            string key = new string(chs);
            if (!map.ContainsKey(new string(chs)))
            {
                map.Add(key, new List<string>());
            }
            map[key].Add(item);
            chs[i] = tmp;
        }
    }
    Queue<KeyValuePair<string, int>> queue = new Queue<KeyValuePair<string, int>>();
    queue.Enqueue(new KeyValuePair<string, int>(beginWord, 1));
    List<string> visited = new List<string>();
    visited.Add(beginWord);
    while(queue.Count > 0)
    {
        var item = queue.Dequeue();
        char[] chk = item.Key.ToCharArray();
        int lvl = item.Value;
        for (int i = 0; i < chk.Length; i++)
        {
            char tmp = chk[i];
            chk[i] = MASK;
            string key = new string(chk);
            if (!visited.Contains(key) && map.ContainsKey(key))
            {
                visited.Add(key);
                foreach (var k in map[key])
                {
                    if (k == endWord)
                        return lvl + 1;
                    else
                        queue.Enqueue(new KeyValuePair<string, int>(k, lvl + 1));
                }
            }
            chk[i] = tmp;
        }
    }

    return 0;

}
```

## 思路 - 双向 BFS

根据给定字典构造的图可能会很大，而广度优先搜索的搜索空间大小依赖于每层节点的分支数量。假如每个节点的分支数量相同，搜索空间会随着层数的增长指数级的增加。考虑一个简单的二叉树，每一层都是满二叉树的扩展，节点的数量会以 2 为底数呈指数增长。

如果使用两个同时进行的广搜可以有效地减少搜索空间。一边从 beginWord 开始，另一边从 endWord 开始。我们每次从两边各扩展一个节点，当发现某一时刻两边都访问了某一顶点时就停止搜索。这就是双向广度优先搜索，它可以可观地减少搜索空间大小，从而降低时间和空间复杂度。

![img](image/figure3.png)

1. 算法与之前描述的标准BFS相类似。唯一的不同是我们从两个节点同时开始搜索，同时搜索的结束条件也有所变化。
2. 我们现在有两个访问数组，分别记录从对应的起点是否已经访问了该节点.
3. 如果我们发现一个节点被两个搜索同时访问，就结束搜索过程。因为我们找到了双向搜索的交点。过程如同从中间相遇而不是沿着搜索路径一直走。
   > 双向搜索的结束条件是找到一个单词被两边搜索都访问过了。
4. 最短变换序列的长度就是中间节点在两边的层次之和。因此，我们可以在访问数组中记录节点的层次。

## 代码 - 双向 BFS

```csharp
public class Solution {
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        char MASK = '*';
        int ans = 0;
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        foreach (string item in wordList)
        {
            char[] chs = item.ToCharArray();
            for (int i = 0; i < chs.Length; i++)
            {
                char tmp = chs[i];
                chs[i] = MASK;
                string key = new string(chs);
                if (!map.ContainsKey(new string(chs)))
                {
                    map.Add(key, new List<string>());
                }
                map[key].Add(item);
                chs[i] = tmp;
            }
        }

        Queue<KeyValuePair<string, int>> top = new Queue<KeyValuePair<string, int>>();
        top.Enqueue(new KeyValuePair<string, int>(beginWord, 1));
        Queue<KeyValuePair<string, int>> btm = new Queue<KeyValuePair<string, int>>();
        btm.Enqueue(new KeyValuePair<string, int>(endWord, 1));

        Dictionary<string, int> topvisited = new Dictionary<string, int>();
        topvisited.Add(beginWord, 1);
        Dictionary<string, int> btmvisited = new Dictionary<string, int>();
        btmvisited.Add(endWord, 1);

        while (top.Count > 0 || btm.Count > 0)
        {
            // One hop from begin word
            ans = VisitWordNode(map, top, topvisited, btmvisited);
            if (ans > -1)
                return ans;
            // One hop from end word
            ans = VisitWordNode(map, btm, btmvisited, topvisited);
            if (ans > -1)
                return ans;
        }
        return 0;
    }

    private int VisitWordNode(Dictionary<string, List<string>> map, Queue<KeyValuePair<string, int>> queue, Dictionary<string, int> visited, Dictionary<string, int> othvisited)
    {
        var item = queue.Dequeue();
        char[] chk = item.Key.ToCharArray();
        int lvl = item.Value;
        for (int i = 0; i < chk.Length; i++)
        {
            char tmp = chk[i];
            chk[i] = '*';
            string key = new string(chk);
            if (!visited.ContainsKey(key) && map.ContainsKey(key))
            {
                visited.Add(key, lvl);
                foreach (var k in map[key])
                {
                    if (othvisited.ContainsKey(k))
                    {
                        return lvl + othvisited[k];
                    }
                    else
                        queue.Enqueue(new KeyValuePair<string, int>(k, lvl + 1));
                }
            }
            chk[i] = tmp;
        }

        return -1;
    }
}
```
