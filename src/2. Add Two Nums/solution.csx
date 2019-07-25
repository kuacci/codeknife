public class Solution
{
    public ListNode AddTwoNumbers(ListNode left, ListNode right)
    {
        return CalculateRecursively(left, right, 0);
    }

    private ListNode CalculateRecursively(ListNode left, ListNode right, int carray)
    {

        if (left == null && right == null)
        {
            return carray == 0 ? null : new ListNode(carray);
        }

        int total = 0;

        if (left != null)
        {
            total += left.Value;
        }

        if (right != null)
        {
            total += right.Value;
        }

        total += carray;

        var val = total % 10;

        return new ListNode(val)
        {
            Next = CalculateRecursively(left?.Next, right?.Next, total / 10)
        };
    }
}

public class ListNode
{
    public int Value { get; }
    public ListNode Next { get; set; }
    public ListNode(int val)
    {
        this.Value = val;
    }

    public ListNode(int[] vals)
    {
        this.Value = vals[0];
        var temp = this;

        for (int index = 1; index < vals.Length; index++)
        {
            if (temp.Next == null)
            {
                temp.Next = new ListNode(vals[index]);
            }

            temp = temp.Next;
        }
    }
}
