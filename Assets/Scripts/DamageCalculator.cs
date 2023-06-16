using Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts
{
    public class DamageCalculator
    {
        public static InfiniteInteger CalculateDamage(InfiniteInteger damage, float multiplier)
        {
            return damage * multiplier;
        }
        public static InfiniteInteger CalculateDefaultDamage()
        {
            return new InfiniteInteger(TurretUpgrades.Instance.Damage) * (float)(Timers.Instance.DamageMultiplierTimer > 0 ? 3 : 1);
        }
    }
}