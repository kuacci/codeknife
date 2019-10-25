public class Solution
{
    public int Search(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) >> 1;

            if (target == nums[mid])
            {
                return mid;
            }

            if (nums[left] < nums[mid]) // left part is in-order
            {
                if (target >= nums[left] && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                // right part is in-order

                if (target > nums[mid] && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return -1;
    }
}

public static void Main()
{
    int[] nums = { 4, 5, 6, 7, 8, 1, 2, 3 };
    int target = 8;

    int index = (new Solution()).Search(nums, target);

    System.Console.WriteLine(index);
}

Main();