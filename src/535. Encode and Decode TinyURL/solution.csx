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