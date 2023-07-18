using UnityEngine;
using static GameStatsNS.GameStats;

public static class EnemyHealthAndSpawnDelayCoefficientsCalculator
{
    private const int DIVIDER = 30;
    private const float SPAWN_DELAY_COEFFICIENT = 1;
    public static InfiniteInteger CalculateHealthCoefficient()
    {
        return InfiniteInteger.Pow(2, MainGameStatsHolder.TurretUpgrades.SpawnCooldown / (float)DIVIDER);
    }
    public static float CalculateSpawnDelayCoefficient()
    {
        float log = Mathf.Log(MainGameStatsHolder.TurretUpgrades.SpawnCooldown + 1, 2) + 1;
        float sqrt = Mathf.Sqrt((MainGameStatsHolder.TurretUpgrades.SpawnCooldown % 20 / 20f) / DIVIDER + 1);
        return SPAWN_DELAY_COEFFICIENT / log * sqrt;
    }
}