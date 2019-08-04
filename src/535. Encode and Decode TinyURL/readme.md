# 535. Encode and Decode TinyURL

> Note: This is a companion problem to the System Design problem: Design TinyURL.

TinyURL is a URL shortening service where you enter a URL such as `https://leetcode.com/problems/design-tinyurl` and it returns a short URL such as `http://tinyurl.com/4e9iAk`.

Design the encode and decode methods for the TinyURL service. There is no restriction on how your encode/decode algorithm should work. You just need to ensure that a URL can be encoded to a tiny URL and the tiny URL can be decoded to the original URL.

## 思路

题目的目的是要将长的URL经过编码的方式，变成较短地址的URL。当接收到短URL的时候，能够通过同样的解码方式，计算出原来的长URL。
我的想法是接收到URL的时候，计算出它的string的[hashcode](https://docs.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=netframework-4.8)。这个hashcode在进程的生命周期内是唯一的值。考虑到这个题目没有涉及到跨进程生命周期的情况，使用GetHashCode()所得到的Hashcode值已经足够作为标识了。
获得hashcode之后，将他作为Key保存在一个Dictionary中。URL则作为value。
在解码的时候，将key传入，取出value即可。

## 代码

``` csharp
public class Codec {
    private Dictionary<string, string> codecMap = new Dictionary<string, string>();
    string longPrefix = "https://leetcode.com/problems/";
    string shortPrefix = "http://tinyurl.com/";
    // Encodes a URL to a shortened URL
    public string encode(string longUrl) {

        string key = Convert.ToString(longUrl.GetHashCode(),16);
        if(!codecMap.ContainsKey(key))
            codecMap.Add(key, longUrl);
        return shortPrefix + key;
    }

    // Decodes a shortened URL to its original URL.
    public string decode(string shortUrl) {
        string key = shortUrl.Substring(shortUrl.IndexOf(shortPrefix) + shortPrefix.Length);
        string longUrl = "";
        if(codecMap.ContainsKey(key))
            longUrl = codecMap[key];
        return longUrl;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.decode(codec.encode(url));
```
