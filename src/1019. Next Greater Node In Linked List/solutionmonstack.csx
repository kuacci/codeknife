/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */

public class Solution {

    int count = 0;

    public int[] NextLargerNodes(ListNode head) {
        if(head == null) return new int[0];
        if(head.next == null) return new int[1] {0};

        helper(head);
        int[] resutl = new int[count];

        int i = 0;
        while(head != null)
        {
            resutl[i++] = head.val;
            head = head.next;
        }

        return resutl;
    }

    private Stack<int> monStack = new Stack<int>();

    private void helper(ListNode node)
    {
        if(node == null) return;

        // travel to the tail of the ListNode
        if(node.next != null) 
        {
            helper(node.next);
        }

        count ++;

        while(monStack.Count > 0)
        {
            int max = monStack.Peek();

            if(max > node.val)
            {
                monStack.Push(node.val);
                node.val = max;
                return;
            }
            else // max <= node.val
            {
                monStack.Pop();
            }
        }

        monStack.Push(node.val);
        node.val = 0;
    }
}

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }