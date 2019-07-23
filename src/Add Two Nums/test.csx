#load "solution.csx"

public static void Main()
{
    int[] leftVals = { 2, 4, 3 };
    int[] rightVals = { 5, 6, 4 };

    var leftList = new ListNode(leftVals);
    var rightList = new ListNode(rightVals);

    var result = new Solution().AddTwoNumbers(leftList, rightList);

    PrintList(result);
}

Main();

public static void PrintList(ListNode list)
{
    var builder = new StringBuilder();

    var temp = list;

    while (temp != null)
    {

        if (temp.Next == null)
        {
            builder.Append($"{temp.Value}");
        }
        else
        {
            builder.Append($"{temp.Value} -> ");
        }

        temp = temp.Next;
    }

    System.Console.WriteLine(builder.ToString());

}