using MonoBehaviours;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class Timers : Serializable<Timers>
    {
        private const float MS_TO_S = 0.001F;
        public Timers()
        {
            GlobalTimer.OnTick += DecreaseTime;
        }
        public enum Timer
        {
            CoinMultiplierTimer,
            DamageMultiplierTimer,
            ExplosivesAttacksTimer,
            X10RewardTimer,
        }
        public float X10Reward { get; set; }
        public float CoinMultiplierTimer { get; set; }
        public float DamageMultiplierTimer { get; set; }
        public float ExplosivesAttacksTimer { get; set; }
        
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
                case Timer.X10RewardTimer:
                    X10Reward += time; break;
            }
        }
        private void DecreaseTime(float deltaTimeMS)
        {
            //decrease
            DamageMultiplierTimer -= deltaTimeMS * MS_TO_S;
            ExplosivesAttacksTimer -= deltaTimeMS * MS_TO_S;
            CoinMultiplierTimer -= deltaTimeMS * MS_TO_S;
            X10Reward -= deltaTimeMS * MS_TO_S;

            //check if lower than zero
            if (DamageMultiplierTimer <= 0) DamageMultiplierTimer = 0;
            if (ExplosivesAttacksTimer <= 0) ExplosivesAttacksTimer = 0;
            if (CoinMultiplierTimer <= 0) CoinMultiplierTimer = 0;
            if (X10Reward <= 0) X10Reward = 0;
        }
        public void ResetDeletableValues()
        {
            CoinMultiplierTimer = 0;
            DamageMultiplierTimer = 0;
            ExplosivesAttacksTimer = 0;
        }
    }
}
