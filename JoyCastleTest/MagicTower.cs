using System;
using System.Collections.Generic;
public class EnergyFieldSystem
{
    public static float MaxEnergyField(int[] heights)
    {
        int maxS = 0;
        for (int i = 0; i < heights.Length; i++)
        {
            for (int j = i + 1; j < heights.Length; j++)
            {
                int temp = (heights[i] + heights[j]) * (j - i);
                maxS = maxS > temp ? maxS : temp;
            }
        }
        return (float)maxS / 2;
    }
}
// 单元测试

public class EnergyFieldSystemTests
{
    public static void TestMaxEnergyField()
    {
        Console.WriteLine("2.魔法能量场----------------------------");
        // 在这⾥编写测试⽤例
        int[] heights = { 1,8,6,2,5,4,8,3,7 };
        var ans = EnergyFieldSystem.MaxEnergyField(heights);
        Console.WriteLine("\t最大能量场面积为：" + ans);
    }
}

/*
 
时间复杂度：
    需要进行1+2+3+……+N次计算，每次计算的复杂度为常数，故总时间复杂度为O(N²)；

空间复杂度：
    只需要一个变量存储最大面积数值，不需要任何其他空间，故空间复杂度为O(1)

进阶挑战：
    1.如果我们允许玩家使⽤魔法道具来临时增加某个位置的塔的⾼度，你会如何修改你的算法？
        计算时对每个高度增加魔法道具提供的值。根据梯形公式，加在上底下底都一样，故直接将
            (heights[i] + heights[j]) * (j - i)
        改为：
            (heights[i] + heights[j] + extraHeight) * (j - i)
        即可。
    2.在游戏的⾼级模式中，某些位置可能有建筑限制（⾼度为0）。你的算法如何处理这种情况？
        算法可以接受数组的值为0，不需要额外处理。
 
*/