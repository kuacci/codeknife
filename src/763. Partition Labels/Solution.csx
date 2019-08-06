public class Solution {
    public IList<int> PartitionLabels(string S) {
        IList<int> part = new List<int>();
        if(string.IsNullOrEmpty(S)) return part;
        char[] ch = S.ToCharArray();
        Dictionary<char,int> charPos = InitCharPos(ch);

        int start = 0;
        int end = 0;

        for(int i = 0; i < ch.Length; i++)
        {
            end = Math.Max(end, charPos[ch[i]]);

            if(i == end)
            {
                part.Add(end - start + 1);
                start = Math.Min(i + 1, ch.Length - 1);
            }
        }

        return part;
    }

    private Dictionary<char,int> InitCharPos(char[] ch)
    {
        Dictionary<char,int> charPos = new Dictionary<char,int>();

        for(int i = 0; i < ch.Length; i++)
        {
            if(charPos.ContainsKey(ch[i]))
                charPos[ch[i]] = i;
            else
                charPos.Add(ch[i],i);
        }

        return charPos;
    }
}