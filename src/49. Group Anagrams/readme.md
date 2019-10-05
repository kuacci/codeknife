# [49. Group Anagrams](https://leetcode.com/problems/group-anagrams/)

Given an array of strings, group anagrams together.

 **Example:**

```text
Input: ["eat", "tea", "tan", "ate", "nat", "bat"],
Output:
[
  ["ate","eat","tea"],
  ["nat","tan"],
  ["bat"]
]
```

Note:

All inputs will be in lowercase.
The order of your output does not matter.

## 思路

这里要将包含字符一样的string归类到一起。思路是采用一个字典表，将排序以后的string作为key，将它在`IList<IList<string>>`ans中的index作为value. 下一个string先检测是否在这个字典表中，进行相应处理。
字典表的定义采用`Dictionary<string, int> dic = new Dictionary<string, int>();` 而不是采用char[]作为key. 这是因为string和char[]同为应用类型，但是他们Equals的实现不一样。string的Equals 对比的是hashcode, 两个内容一样的string是相等的，这点跟值类型非常相似。而`char[]`的Equals比较的是地址，相同值的两个`char[]` 在做比较时并不相等。所以char[]不能作为key.

## 代码

```csharp
public class Solution
{
    Dictionary<string, int> dic = new Dictionary<string, int>();
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        IList<IList<string>> ans = new List<IList<string>>();

        foreach (var str in strs)
        {
            char[] ch = str.ToCharArray();
            Array.Sort(ch);
            string key = new string(ch);
            if (dic.ContainsKey(key))
            {
                int index = dic[key];
                ans[index].Add(str);
            }
            else
            {
                dic.Add(key, dic.Count);
                IList<string> ansitem = new List<string>();
                ansitem.Add(str);
                ans.Add(ansitem);
            }
        }
        return ans;
    }
}
```
