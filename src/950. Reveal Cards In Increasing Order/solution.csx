public class Solution {
    public int[] DeckRevealedIncreasing(int[] deck) {
        
        if(deck.Length == 0 || deck.Length == 1) return deck;
        
        int len = deck.Length;
        int[] reveal = InitArray(len);
        Array.Sort(deck);
        bool skip = false;
        
        int pos = 0;
        
        for(int i = 0; i < len - 1; i++)
        {
            while(skip)
            {
                pos = MoveToNext(reveal, pos);
                skip = false;
            }

            reveal[pos] = deck[i];
            skip = true;
            pos = MoveToNext(reveal, pos);
        }
        reveal[pos] = deck[len - 1];
        
        return reveal;

    }
    
    private int[] InitArray(int len)
    {
        int[] reveal = new int[len];
        
        for(int i = 0; i < len; i++)
        {
            reveal[i] = Int32.MinValue;
        }
        
        return reveal;
    }
    private int MoveToNext(int[] reveal, int pos)
    {
        while(true)
        {
            if(pos == reveal.Length - 1)
                pos = 0;
            else
                pos += 1;
            if(reveal[pos] == int.MinValue) break;
        }
        return pos;
    }
}