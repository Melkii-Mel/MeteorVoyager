using System;
using static GameStatsNS.GameStats;

public static class Calculator
{
    private static readonly Random Random = new();

    public static InfiniteInteger CalculateDefaultCoinsAmount(InfiniteInteger damage)
    {
        InfiniteInteger reward = damage;

        reward *= MainGameStatsHolder.Timers.CoinMultiplierTimer > 0 ? 3 : 1;
        reward *= MainGameStatsHolder.Timers.X10Reward > 0 ? 10 : 1;
        reward *= (int)UnityEngine.Mathf.Pow(2, MainGameStatsHolder.MeteorUpgrades.CoinMultiplier);

        return reward;
    }

    public static bool RandomBool(float chanceOfTrue)
    {
        return Random.NextDouble() < chanceOfTrue;
    }
}