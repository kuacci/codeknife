public class Solution {
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        
        if(l1 == null) return l2;
        if(l2 == null) return l1;
        
        ListNode head = new ListNode(0);
        ListNode tmp = head;
        int cflag = 0;
        
        while(true)
        {
            int n1 = l1 == null ? 0 : l1.val;
            int n2 = l2 == null ? 0 : l2.val;
            n1 = n1 + n2 + cflag;
            cflag = n1 / 10;
            tmp.val = n1 % 10;
            
            l1 = l1 == null ? null : l1.next;
            l2 = l2 == null ? null : l2.next;
            
            if(l1 == null && l2 == null && cflag == 0) break;
            
            tmp.next = new ListNode(0);
            tmp = tmp.next;
        }

        return head;
        
    }
}
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }
