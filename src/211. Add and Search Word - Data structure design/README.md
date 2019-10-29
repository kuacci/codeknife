# [Medium][211. Add and Search Word - Data structure design](https://leetcode.com/problems/add-and-search-word-data-structure-design/)

Design a data structure that supports the following two operations:

> void addWord(word)
> bool search(word)

search(word) can search a literal word or a regular expression string containing only letters a-z or .. A . means it can represent any one letter.

**Example:**

> addWord("bad")
> addWord("dad")
> addWord("mad")
> search("pad") -> false
> search("bad") -> true
> search(".ad") -> true
> search("b..") -> true

**Note:**
You may assume that all words are consist of lowercase letters a-z.

## 思路 - Trie

这里的思路与[208. Implement Trie (Prefix Tree)](../208.%20Implement%20Trie%20(Prefix%20Tree))很相似。通过Trie字典树来做索引。

如果是精确匹配，那么还是通过Trie自带的检索方式。那么问题在于模糊查询'.'的处理，当遇到检索的字符是'.'的时候，应该匹配所有的字符。这个时候需要用BFS的方式，借助Queue将所有的可能性都包涵进来，再匹配下一个字符。

## 代码 - Trie

```csharp
public class WordDictionary
{

    /** Initialize your data structure here. */
    private WordDictionary[] nodes;
    private bool isEnd;
    private int capacity = 26;
    public WordDictionary()
    {
        nodes = new WordDictionary[capacity];
        isEnd = false;
    }

    /** Adds a word into the data structure. */
    public void AddWord(string word)
    {
        WordDictionary node = this;
        for (int i = 0; i < word.Length; i++)
        {
            if (node.nodes[word[i] - 'a'] == null)
                node.nodes[word[i] - 'a'] = new WordDictionary();
            node = node.nodes[word[i] - 'a'];
        }
        node.isEnd = true;
    }

    /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
    public bool Search(string word)
    {
        WordDictionary node = this;
        Queue<WordDictionary> q = new Queue<WordDictionary>();
        q.Enqueue(this);
        for (int i = 0; i < word.Length; i++)
        {
            if (q.Count == 0) return false;
            char key = word[i];
            if(key == '.')
            {
                int count = q.Count;
                while (count > 0)
                {
                    count--;
                    WordDictionary qn = q.Dequeue();
                    foreach (var item in qn.nodes)
                    {
                        if (item == null) continue;
                        q.Enqueue(item);
                    }
                }
            }
            else
            {
                int count = q.Count;
                while (count > 0)
                {
                    count--;
                    WordDictionary qn = q.Dequeue();
                    if(qn.nodes[key - 'a'] != null)
                    {
                        q.Enqueue(qn.nodes[key - 'a']);
                    }
                }
            }
        }
        while (q.Count > 0)
        {
            WordDictionary qn = q.Dequeue();
            if (qn.isEnd) return true;
        }
        return false;
    }
}

/**
 * Your WordDictionary object will be instantiated and called as such:
 * WordDictionary obj = new WordDictionary();
 * obj.AddWord(word);
 * bool param_2 = obj.Search(word);
 */
```
