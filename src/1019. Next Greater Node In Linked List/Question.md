# [LeetCode] 1019. Next Greater Node In Linked List

We are given a linked list with head as the first node.  Let's number the nodes in the list: node_1, node_2, node_3, ... etc.

Each node may have a next larger value: for node_i, next_larger(node_i) is the node_j.val such that j > i, node_j.val > node_i.val, and j is the smallest possible choice.  If such a j does not exist, the next larger value is 0.

Return an array of integers answer, where answer[i] = next_larger(node_{i+1}).

Note that in the example inputs (not outputs) below, arrays such as [2,1,5] represent the serialization of a linked list with a head node value of 2, second node value of 1, and third node value of 5.

Example: 1

>**Input:** [2,1,5]
**Output:** [5,5,0]

Example: 2

>**Input:** [2,7,4,3,5]
**Output:** [7,0,5,5,0]

Example: 3

>**Input:** [1,7,5,1,9,2,5,1]
**Output:** [7,9,9,9,0,5,0,0]

## 思路 - 递归

题目要求寻找右侧第一个比它大的值。

例如[1,7,9,9]. 第一个元素[1], 右边第一个比它大的是[7],虽然最大的数是[9]. 第二个元素[7]，右边第一个比他大的是[9]. 第三个元素[9],右边的元素是[9]，没有再比他大的数，所以是[0]. 第4个元素[0], 右边已经没有元素了，所以返回的是[0]. 正确的返回值应该是[7,9,0,0].

用全局变量保存遍历过的最大数值，初始值为0。解题思路是用递归先走到最右侧，进入递归的条件是head.next != null，及还没有走到底。
创建一个新的NodeList 对象，用来保存计算过的右侧遇到的第一个比它大的数。
最后用这个NodeList对象创建一个int[] 作为返回值。

## 代码

``` csharp
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
    int max = 0;

    public int[] NextLargerNodes(ListNode head) {
        if(head == null) return new int[0];
        if(head.next == null) return new int[1] {0};

        ListNode newHead = helper(head);

        int[] answers = new int[count];

        for(int i = 0; i < count; i++)
        {
            answers[i] = newHead.val;
            newHead = newHead.next;
        }

        return answers;
    }

    private ListNode helper(ListNode node)
    {
        if(node == null) return null;

        ListNode newHead = new ListNode(0);
        ListNode nextNode = null;

        // recrusive call, move to the tail of the LinkNode
        if(node.next != null)
        {
            nextNode = helper(node.next);
        }

        if(node.val < max)
        {
            // if node.val is less then max, then check the right side and search the 1st value lager than its value
            // if reach to the end then break
            // if next value lager than its value then break
            // if next value is max, then break
            int tmp = node.val;
            while(node != null && tmp >= node.val && node.val < max)
                node = node.next;
            newHead.val = node == null ? max : node.val;
        }
        else // node.val >= max, the val is 0 and update max
        {
            newHead.val = 0;
            max = node.val;
        }

        count ++; // caculate int[] length
        if(nextNode != null)
            newHead.next = nextNode;

        return newHead;
    }
}
```

## 思路 - monotonic stack

上述的思路中有个问题。当寻找右侧的第一个比他大的值的时候，总是要从右侧第一个开始遍历。如果运气非常不好，每次都需要找到最右端才能确认具体的值。例如 ：

>**Input:**[9,8,7,6,5,4,3,2,1]
**Output:**[0,0,0,0,0,0,0,0,0]

为了优化这个过程，可以使用[monotonic stack](https://endlesslethe.com/monotone-queue-and-stack-tutorial.html). 思路是构建一个有序的堆栈，栈底最大，栈顶最小。比较时，首先比较栈顶的值是否大于当前值。如果小于这个值，则查找结束。如果大于这个值，则弹出栈顶的值，继续比较，直到找到一个比它大的值。然后再将当期值压入栈。如果一直弹到栈底为空，那么说明遍历过的右侧没有一个值比它的值大，则查找结果为0.当期值应该是最大值，压入栈。

这个方法可以很快的找到右侧曾经遍历过的大小值，不需要依次遍历到底。

```csharp
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

```
