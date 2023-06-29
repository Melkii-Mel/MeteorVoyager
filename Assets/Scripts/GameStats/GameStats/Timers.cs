using SerializationLibrary;
using System;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class Timers : Serializable<Timers>
    {
        public Timers() : base()
        {
            GlobalTimer.AddAction(DecreaseTime);
        }
        public enum Timer
        {
            CoinMultiplierTimer,
            DamageMultiplierTimer,
            ExplosivesAttacksTimer,
            X10RewardTimer,
        }
        public float X10Reward { get; set; } = 0;
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
        private void DecreaseTime()
        {
            //decrease
            DamageMultiplierTimer -= GlobalTimer.TICK_TIME;
            ExplosivesAttacksTimer -= GlobalTimer.TICK_TIME;
            CoinMultiplierTimer -= GlobalTimer.TICK_TIME;
            X10Reward -= GlobalTimer.TICK_TIME;

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
