using System;
using System.Collections.Generic;
public class TreasureHuntSystem
{
    public static int MaxTreasureValue(int[] treasures)
    {
        if (treasures.Length == 1)
        {
            return treasures[0];
        }
        for (int i = 2; i < treasures.Length; i++)
        {
            treasures[i] = Max(treasures[i - 1], treasures[i] + treasures[i - 2]);
        }
        return treasures[treasures.Length - 1];
    }

    private static int Max(int a, int b)
    {
        return a > b ? a : b;
    }
}
// 单元测试

public class TreasureHuntSystemTests
{
    public static void TestMaxTreasureValue()
    {
        Console.WriteLine("3.魔法宝箱探险--------------------------");
        // 在这⾥编写测试⽤例
        int[] treasures = { 3, 1, 5, 2, 4 };
        var ans = TreasureHuntSystem.MaxTreasureValue(treasures);
        Console.WriteLine("\t可获得的最大宝物总价值为：" + ans);
    }
}

/*
 
时间复杂度：
    采用动态规划的思想，对于每个宝箱，只有拿与不拿两种选择。遍历数组复杂度为N，每次操作常数时间，故时间复杂度为O(N)；

空间复杂度：
    直接在原数组上进行动态规划，空间复杂度为O(1)；

进阶挑战：
    1.如果我们允许玩家使⽤⼀次"魔法钥匙"，可以安全地打开任意两个相邻的宝箱⽽不触发陷阱，你会如何修改你的算法？
        如果允许使用一次魔法钥匙，则破坏了动态规划中子问题的不相关性，不能使用简单的动态规划。
        此时可以采用根据条件，选择性动态规划的方法。先正常使用原方法遍历每个宝箱；
            如果选择了当前宝箱，则开辟一个分支，单独计算在此处使用道具后接着动态规划的结果，并记录最大值。
            如果没有选择当前宝箱，则继续按照原方法遍历。
    
    2.在游戏的⾼级关卡中，有些宝箱可能包含负值（表⽰陷阱会扣除玩家的分数）。你的算法如何处理这种情况？
        增加关于宝箱本身的判定，若为负值，则将当前位的treasure值设置为前一个值的大小即可。

创意思考：
    1.可以将该思路融入天赋系统的设计，即玩家不能选择相邻的两个天赋，由此来控制最终效果。

    2.可以将“宝箱”的概念拓展到“关卡”，将箱中宝物的概念拓展为“增益buff”，设计成一个RougeLike游戏。
    玩家可以提前看到所有关卡的奖励，并根据自己对buff的需求以及“魔法钥匙”的道具情况自由选择是否要打这一关并获得其奖励。
    奖励中可以包含武器、buff、道具甚至“魔法钥匙”。
    玩家经过选择的路径后，可以挑战最终关底boss。
 
 */