using System;

namespace JoyCastleTest
{
    class MainEntry
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Main Entry:");
            LeaderboardSystemTests.TestGetTopScores();
            EnergyFieldSystemTests.TestMaxEnergyField();
            TreasureHuntSystemTests.TestMaxTreasureValue();
            TalentAssessmentSystemTests.TestFindMedianTalentIndex();
        }
    }
}
