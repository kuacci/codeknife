public static void Main()
{
    // Example: Given nums = [2, 7, 11, 15], target = 9,

    // Because nums[0] + nums[1] = 2 + 7 = 9, return [0, 1].

    int[] nums = new int[] { 11, 2, 7, 2, 11, 15 };

    var result = new Solution().TwoSum(nums, 9);

    System.Console.WriteLine("The numbers are:");
    foreach (var index in result)
    {
        System.Console.Write($"{nums[index]}, ");
    }
}

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        var dic = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (dic.ContainsKey(target - nums[i]))
            {
                return new int[] { i, dic[target - nums[i]] };
            }
            else
            {
                // prevent the duplicates in nums
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], i);
                }
            }
        }
        return new int[] { 0, 0 };
    }
}

Main();