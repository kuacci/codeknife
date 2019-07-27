public class Solution {
    public int NthUglyNumber(int n) {

        if(n == 1) return 1;

        int[] nums = new int[n];

        int p2 = 0;
        int p3 = 0;
        int p5 = 0;

        nums[0] = 1;

        for(int i = 1; i < n; i++)
        {
            int n2 = nums[p2] * 2;
            int n3 = nums[p3] * 3;
            int n5 = nums[p5] * 5;

            nums[i] = min(min(n2,n3),n5);

            if(nums[i] / 2 == nums[p2]) p2 +=1;
            if(nums[i] / 3 == nums[p3]) p3 +=1;
            if(nums[i] / 5 == nums[p5]) p5 +=1;

        }

        return nums[n-1];

    }

    public int min(int a, int b)
    {
        return a < b ? a : b;
    }
    
}