# [LeetCode] 2. Add two numbers

You are given two **non-empty** linked lists representing two non-negative integers. The digits are stored in **reverse order** and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.

You may assume the two numbers do not contain any leading zero, except the number 0 itself.

**Example:**

>**Input:** (2 -> 4 -> 3) + (5 -> 6 -> 4)  
**Output:** 7 -> 0 -> 8  
**Explanation:** 342 + 465 = 807  

## 思路

递归实现时，只需要考虑如下四种情形：

1. Left 与 Right 都为 Null，即 base condition 最终退出递归的条件。
2. Left 与 Right 都为 Null 但 Carry 不为 0，即 base condition 最终退出递归的条件。
3. Left 为 Null，但 Right 不为 Null
4. Right 为 Null，但 Left 不为 Null

## 代码

```csharp
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
```
