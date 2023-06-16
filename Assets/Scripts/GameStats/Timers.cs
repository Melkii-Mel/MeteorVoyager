using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.Serialization;
using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;
using System;

namespace Assets.Scripts.GameStatsNameSpace
{
    [Serializable]
    public class Timers : IGameStats, IDeletableOnRelocation
    {
        public enum Timer
        {
            CoinMultiplierTimer,
            DamageMultiplierTimer,
            ExplosivesAttacksTimer,
            X10RewardTimer,
        }
        public static Timers Instance = new Timers();
        Timers()
        {
            new DataLoaderSaver<IGameStats>().LoadGameStat(this);
            GameStats.AppendIGameStats(this);
        }
        public int X10Reward { get; set; } = 0;
        public float CoinMultiplierTimer { get; set; } = 0;
        public float DamageMultiplierTimer { get; set; } = 0;
        public float ExplosivesAttacksTimer { get; set; } = 0;
        public string FileName { get; set; } = "Timers";

        public void AddToAutoSaver()
        {
            AutoSaver<IGameStats>.GameStats.Add(this);
        }
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
