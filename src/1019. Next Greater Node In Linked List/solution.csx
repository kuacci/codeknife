/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */

public class Solution {
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        if(l1 == null) return l2;
        if(l2 == null) return l1;
        
        l1 = RevertNode(l1);
        l2 = RevertNode(l2);
        
        ListNode head = new ListNode(0);
        ListNode tmp = head;
        int cflag = 0;
        
        while(true)
        {
            int val1 = l1 == null ? 0 : l1.val;
            int val2 = l2 == null ? 0 : l2.val;
            
            int val = val1 + val2 + cflag;
            tmp.val = val % 10;
            cflag = val / 10;
            
            l1 = l1 == null ? null : l1.next;
            l2 = l2 == null ? null : l2.next;
            
            if(l1 == null && l2 == null && cflag == 0) break;
            
            tmp.next = new ListNode(0);
            tmp = tmp.next;
        }
        
        head =  RevertNode(head);
        return head;
        
    }
    
    private ListNode RevertNode(ListNode l)
    {
        if(l == null || l.next == null) return l;
        
        ListNode newheader = RevertNode(l.next);
        
        l.next.next = l;
        l.next = null;
        
        return newheader;
    }
}

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }