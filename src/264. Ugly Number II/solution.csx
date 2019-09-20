public class Solution {
    public int NthUglyNumber(int n) {

        if(n < 1) return 0;

        int[] nums = new int[n];

        int p2 = 0;
        int p3 = 0;
        int p5 = 0;

        nums[0] = 1;

        for(int i = 1; i < n; i++)
        {
            nums[i] = Math.Min(Math.Min(nums[p2] * 2,nums[p3] * 3),nums[p5] * 5);

            if(nums[i] / 2 == nums[p2]) p2 ++;
            if(nums[i] / 3 == nums[p3]) p3 ++;
            if(nums[i] / 5 == nums[p5]) p5 ++;
        }

        return nums[n-1];
    }
}