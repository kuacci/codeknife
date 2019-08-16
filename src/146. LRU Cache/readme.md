# LRU Cache

Design and implement a data structure for Least Recently Used (LRU) cache. It should support the following operations: get and set.
get(key) - Get the value (will always be positive) of the key if the key exists in the cache, otherwise return -1.

set(key, value) - Set or insert the value if the key is not already present. When the cache reached its capacity, it should invalidate the least recently used item before inserting a new item.

## 参考

* <http://bangbingsyb.blogspot.com/2014/11/leetcode-lru-cache.html>

## 思路

1. 使用单链表将所有的 entry 连接起来。并使用 dictionary 来保存 key 与 entry 间的对应关系。
2. 当调用 Set(key, value) 时:
    a. 若 key 不存在时，将 value 封装成 CacheEntryNode 并 append 至链表尾部。即 _last。
    b. 若 key 存在时，将 key 对应的 CacheEntryNode 移至链表尾部。
3. 当调用 Get(key) 时，从 Dictionary 中找到对应的 CacheEntryNode，并将其移至链表尾部。最后返回 CacheEntryNode.Value。

总之在 LRUCache 中总是把最近更新过的 CacheEntryNode 放到链表尾部，并将 _last 指向它。则 _first 指向的为链表头部节点，它为最不活跃节点。整个链表是按时间顺序排列的，当发现 Capacity 不足时就清除链表头部节点。
