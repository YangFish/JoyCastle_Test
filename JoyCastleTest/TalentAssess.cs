using System;
using System.Collections.Generic;
public class TalentAssessmentSystem
{
    public static double FindMedianTalentIndex(int[] fireAbility, int[] iceAbility)
    {
        int n = fireAbility.Length;
        int m = iceAbility.Length;
        double left = kMinNum(0, n - 1, 0, m - 1, fireAbility, iceAbility, (n + m + 1) / 2);
        double right = kMinNum(0, n - 1, 0, m - 1, fireAbility, iceAbility, (n + m + 2) / 2);
        return (left + right) / 2;
    }

    private static double kMinNum(int startA, int endA, int startB, int endB, int[] fireAbility, int[] iceAbility, int k)
    {
        int lenA = endA - startA + 1;
        int lenB = endB - startB + 1;
        if (lenA > lenB)
            return kMinNum(startB, endB, startB, endA, iceAbility, fireAbility, k);
        if (lenA == 0)
            return iceAbility[startB + k - 1];
        if (k == 1)
            return Math.Min(fireAbility[startA], iceAbility[startB]);

        int i = startA + Math.Min(lenA, k / 2) - 1;
        int j = startB + Math.Min(lenB, k / 2) - 1;

        if (fireAbility[i] > iceAbility[j])
            return kMinNum(startA, endA, j + 1, endB, fireAbility, iceAbility, k - (j - startB + 1));
        else
            return kMinNum(i + 1, endA, startB, endB, fireAbility, iceAbility, k - (i - startA + 1));
    }
}
// 单元测试

public class TalentAssessmentSystemTests
{
    public static void TestFindMedianTalentIndex()
    {
        Console.WriteLine("4.魔法宝箱探险--------------------------");
        // 在这⾥编写测试⽤例
        int[] fireAbility = { 1, 3, 7, 9, 11 };
        int[] iceAbility = { 2, 4, 8, 10, 12, 14 };
        var ans = TalentAssessmentSystem.FindMedianTalentIndex(fireAbility, iceAbility);
        Console.WriteLine("\t学徒综合天赋指数为：" + ans);
    }
}

/*
 
时间复杂度：
    算法采用寻找第K大数的思想，每次排除K/2个数字。
    中位数的本质就是第length/2大个数，故算法将递归运行直到排除了(M+N)/2个数为止。
    故算法复杂度为O(log(M + N))。

空间复杂度：
    除了常数个变量外，没有额外的空间，故空间复杂度为O(1)。

进阶挑战：
    1.如果我们需要实时更新⼤量学徒的天赋指数，你会如何优化你的算法或数据结构？
        需要在一个已经有序的结构上频繁更新，故采用红黑树来存储fireAbility和iceAbility。
            这样虽然能保持O(logN)级别的插入删除更新，但每次获取综合天赋指数时，需要将红黑树转化成数组，这一步复杂度是O(N)。
        如果每次更新的幅度较小，依然可以使用数组维护数据，每次更新使用插入排序修改，效率较高。

    2.在游戏的⾼级模式中，可能会有更多的魔法属性（不仅仅是⽕和冰）。你的算法如何扩展到处理k个有序数组的中位数？
        处理k个有序数组的中位数，依然可以使用原来的思想，但相比于原来的(m+n)/2，每次排除的数据个数为(n1+n2+n3+……+nk)/k。
        时间复杂度和空间复杂度不变。

创意思考：
    这个天赋评估系统如何影响游戏的⻆⾊发展和技能学习机制？你能想到如何将这个概念融⼊到游戏的其他⽅⾯，⽐如任务系统或PVP对战中吗？
        采用中位数确定角色天赋指数，会导致角色只需要追求稍微超过一半种类数的能力值，而不需要所有天赋均衡发展。
            这样的机制下，会引导玩家放弃半数能力种类的培养，同时使另一半能力值尽量均衡发展。因为太强或太弱的能力值都没有意义。
        可以融入任务系统中：
            例如，在一个多地区的大世界冒险游戏中，每个任务的完成情况或任务期间玩家的选择可以影响对应地区的声望值，而所有声望值的中位数决定总声望值。
            这样就给了玩家半数地区任务的自由，而不用时刻为游戏内的道德规则所束缚。如果某个地区“玩脱了”，也不影响世界总声望。
        也可以融入PVP对战中：
            可以加入pvp中能力值的bp中。
            例如，每场PVP需要选择5种属性魔法作战，每种魔法间有克制关系，五种属性的中位数决定玩家的总能力值，总能力值可以影响蓝量或技能CD等。
            每场比赛分三轮，每轮结束后，玩家可以根据对方上一轮的魔法表现，禁用掉他的某种魔法。
            这样可以增加pvp模式的博弈乐趣，同时鼓励玩家培养更多种能力值来应对敌方的禁用。
 
 */