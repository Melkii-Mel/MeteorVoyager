using SerializationLibrary;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class Timers : Serializable<Timers>
    {
        public enum Timer
        {
            CoinMultiplierTimer,
            DamageMultiplierTimer,
            ExplosivesAttacksTimer,
            X10RewardTimer,
        }
        public int X10Reward { get; set; } = 0;
        public float CoinMultiplierTimer { get; set; } = 0;
        public float DamageMultiplierTimer { get; set; } = 0;
        public float ExplosivesAttacksTimer { get; set; } = 0;
        public void AddTime(float time, Timer timer)
        {
            switch (timer)
            {
                case Timer.DamageMultiplierTimer:
                    DamageMultiplierTimer += time; break;
                case Timer.ExplosivesAttacksTimer:
                    ExplosivesAttacksTimer += time; break;
                case Timer.CoinMultiplierTimer:
                    CoinMultiplierTimer += time; break;
            }
        }
        public void ResetDeletableValues()
        {
            CoinMultiplierTimer = 0;
            DamageMultiplierTimer = 0;
            ExplosivesAttacksTimer = 0;
        }
    }
}
