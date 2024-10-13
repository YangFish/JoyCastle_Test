using System;
using System.Collections.Generic;



public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> elements = new List<T>();

    static void Swap(List<T> list, int index1, int index2)
    {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }

    public void Enqueue(T item)
    {
        elements.Add(item);
        int currentIndex = elements.Count - 1;
        while (currentIndex > 0)
        {
            int parentIndex = (currentIndex - 1) / 2;
            if (elements[currentIndex].CompareTo(elements[parentIndex]) >= 0) break;
            Swap(elements, parentIndex, currentIndex);
            currentIndex = parentIndex;
        }
        //string result = string.Join(", ", elements);
        //Console.WriteLine(result);
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
        {
            throw new InvalidOperationException("The priority queue is empty.");
        }

        T root = elements[0];
        Swap(elements, 0, elements.Count - 1);
        elements.RemoveAt(elements.Count - 1);
        int currentIndex = 0;
        while (currentIndex < elements.Count)
        {
            int leftChild = 2 * currentIndex + 1;
            int rightChild = 2 * currentIndex + 2;
            if (rightChild > elements.Count - 1)
            {
                if (leftChild <= elements.Count - 1 && elements[currentIndex].CompareTo(elements[leftChild]) > 0)
                {
                    Swap(elements, leftChild, currentIndex);
                }
                break;
            }
            if (elements[leftChild].CompareTo(elements[rightChild]) > 0)
            {
                if (elements[currentIndex].CompareTo(elements[rightChild]) > 0) {
                    Swap(elements, rightChild, currentIndex);
                    currentIndex = rightChild;
                }
                else
                {
                    break;
                }
            }
            else
            {
                if (elements[currentIndex].CompareTo(elements[leftChild]) > 0)
                {
                    Swap(elements, leftChild, currentIndex);
                    currentIndex = leftChild;
                }
                else
                {
                    break;
                }
            }
        }
        return root;
    }

    public T Top()
    {
        if (elements.Count == 0)
        {
            throw new InvalidOperationException("The priority queue is empty.");
        }
        else
        {
            return elements[0];
        }
    }

    public int Size()
    {
        return elements.Count;
    }
}
public class LeaderboardSystem
{
    public static List<int> GetTopScores(int[] scores, int m)
    {
        // 边界情况
        if (m > scores.Length)
        {
            m = scores.Length;
            Console.WriteLine("参数异常，仅返回前" + m + "名玩家");
        }
        // 从所有玩家中筛选出前m名高分玩家
        var heap = new PriorityQueue<int>();
        foreach (var num in scores)
        {
            heap.Enqueue(num);
            if (heap.Size() > m)
            {
                heap.Dequeue();
            }
        }

        var ans = new List<int>();
        while (heap.Size() != 0)
        {
            ans.Add(heap.Dequeue());
        }
        return ans;
    }
}
// 单元测试

public class LeaderboardSystemTests
{
    public static void TestGetTopScores()
    {
        Console.WriteLine("1.休闲游戏排行榜------------------------");
        // 在这⾥编写测试⽤例
        int[] nums = { 1, 5, 9, 3, 8, 6, 5, 45, 5, 1, 78, 5, 2, 3, 6, 51, 25, 10 };
        int m = 6;
        var ans = LeaderboardSystem.GetTopScores(nums, m);
        ans.Reverse();
        string result = string.Join(", ", ans);
        Console.WriteLine("\t" + result);
    }
}

/*

时间复杂度：
    遍历数组的计算量为N，元素插入堆中的平均时间复杂度为logM，需要插入M个元素；
    综上，m相比n较小时，时间复杂度为O(N)，m较大时为O(MlogM)；

空间复杂度：
    遍历过程中需要维护一个大小为M的堆，堆使用数组存储，此外不需要其他空间，故空间复杂度为O(M)；

补充说明：
    此算法采用构建有限大小的堆的方法，用于从给定的未排序数组中选取前m名玩家。
    实际情况下，对于排行榜问题，通常是一个已经有序的数组，进行小幅度的排名变化，此时有限堆并不是最优解。
    所以在实际场景下，可以采用红黑树等数据结构便于插入删除，或是使用插入排序+分段排序，便于小幅度灵活调整。

进阶思考：
    如果我们的游戏变得⾮常受欢迎，玩家数量达到了数百万，你会如何优化这个算法以处理⼤规模数据？
    对于百万级别的玩家，如果需要每名玩家精确排名，可以使用Redis的Sorted Set，因为它可以在logN复杂度下完成插入、删除和获取排名操作。
    如果只需要维护小部分（例如前一百名）的精确实时排名，可以采用以下的方式：
        维护一个略大于100的数组（此处假设为1000名），使用插入排序进行实时更新。
        对于1000名以内玩家的分数增减，直接将其向前或向后插入。因为排名的变化频繁且幅度较小，故效率较高。
        对于1000名以外的玩家，使用分段排序的策略。每个分数段维护一个集合，该集合可以不完全有序，通过这样的方式可以估计大致排名。
        如果1000名以内的玩家掉出1000，则只需从最近的分数集合中选取最高分玩家加入前1000即可。

*/

