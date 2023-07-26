using static GameStatsNS.GameStats;
public static class DamageCalculator
{
    public static InfiniteInteger CalculateDefaultDamage()
    {
        return new InfiniteInteger(MainGameStatsHolder.TurretUpgrades.Damage + 1) * (float)(MainGameStatsHolder.Timers.DamageMultiplierTimer > 0 ? 3 : 1);
    }
}