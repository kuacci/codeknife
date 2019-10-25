# [Medium][146. LRU Cache](https://leetcode.com/problems/lru-cache/)

Design and implement a data structure for [Least Recently Used (LRU)](https://en.wikipedia.org/wiki/Cache_replacement_policies#LRU) cache. It should support the following operations: get and put.

get(key) - Get the value (will always be positive) of the key if the key exists in the cache, otherwise return -1.
put(key, value) - Set or insert the value if the key is not already present. When the cache reached its capacity, it should invalidate the least recently used item before inserting a new item.

The cache is initialized with a positive capacity.

Follow up:
Could you do both operations in O(1) time complexity?

**Example:**

```text
LRUCache cache = new LRUCache( 2 /* capacity */ );

cache.put(1, 1);
cache.put(2, 2);
cache.get(1);       // returns 1
cache.put(3, 3);    // evicts key 2
cache.get(2);       // returns -1 (not found)
cache.put(4, 4);    // evicts key 1
cache.get(1);       // returns -1 (not found)
cache.get(3);       // returns 3
cache.get(4);       // returns 4
```

## 思路 - `Dictionary + LinkList`

题目要求实现 LRU 缓存机制，需要在 O(1) 时间内完成如下操作：

* 获取键 / 检查键是否存在
* 设置键
* 删除最先插入的键

既然要求是O(1)，第一个就会想到HashTable或者Dictionary. 既然又要求LRU，那么要使用`List<int>`来更新最近的使用状态。
新增的item放在List的头部，最近访问的item也会移动到头部。删除从尾部开始。

## 代码 - `Dictionary + LinkList`

```csharp
public class LRUCache
{
    private Dictionary<int, int> cache;
    private List<int> lookup;
    public int Count
    {
        get { return this.cache.Count; }
    }

    private int capacity = 0;

    public LRUCache(int capacity)
    {
        cache = new Dictionary<int, int>(capacity);
        this.capacity = capacity;
        this.lookup = new List<int>(capacity);
    }

    public int Get(int key)
    {
        if (cache.ContainsKey(key))
        {
            Refresh(key);
            return cache[key];
        }
        else return -1;
    }

    public void Put(int key, int value)
    {

        if (cache.ContainsKey(key))
        {
            cache[key] = value;
            Refresh(key);
        }
        else
        {
            if (this.Count >= this.capacity)
                Flush();
            cache.Add(key, value);
            lookup.Insert(0, key);
        }
    }

    private void Flush()
    {
        int key = lookup[lookup.Count - 1];
        lookup.Remove(key);
        cache.Remove(key);
    }

    private void Refresh(int key)
    {
        lookup.Remove(key);
        lookup.Insert(0, key);
    }

}

/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */
```

## 思路 - 链表

1. 使用单链表将所有的 entry 连接起来。并使用 dictionary 来保存 key 与 entry 间的对应关系。
2. 当调用 Set(key, value) 时:
    a. 若 key 不存在时，将 value 封装成 CacheEntryNode 并 append 至链表尾部。即 _last。
    b. 若 key 存在时，将 key 对应的 CacheEntryNode 移至链表尾部。
3. 当调用 Get(key) 时，从 Dictionary 中找到对应的 CacheEntryNode，并将其移至链表尾部。最后返回 CacheEntryNode.Value。

总之在 LRUCache 中总是把最近更新过的 CacheEntryNode 放到链表尾部，并将 _last 指向它。则 _first 指向的为链表头部节点，它为最不活跃节点。整个链表是按时间顺序排列的，当发现 Capacity 不足时就清除链表头部节点。

## 代码 - 链表

```csharp

public class Solution
{
    public int Search(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) >> 1;

            if (target == nums[mid])
            {
                return mid;
            }

            if (nums[left] < nums[mid]) // left part is in-order
            {
                if (target >= nums[left] && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                // right part is in-order

                if (target > nums[mid] && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return -1;
    }
}

public static void Main()
{
    int[] nums = { 4, 5, 6, 7, 8, 1, 2, 3 };
    int target = 8;

    int index = (new Solution()).Search(nums, target);

    System.Console.WriteLine(index);
}
```
