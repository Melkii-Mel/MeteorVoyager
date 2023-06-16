using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.Serialization;
using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;
using System;

namespace Assets.Scripts.GameStatsNameSpace
{
    [Serializable]
    public class Progression : IGameStats, IDeletableOnRelocation
    {
        public static Progression Instance = new();
        public Progression()
        {
            new DataLoaderSaver<IGameStats>().LoadGameStat(this);
            GameStats.AppendIGameStats(this);
        }
        public bool IsDamageUpgradeEnabled { get; set; }
        public int GameStage { get; set; }
        public string FileName { get; } = "Progression";

        public void AddToAutoSaver()
        {
            AutoSaver<IGameStats>.GameStats.Add(this);
        }

        public void ResetDeletableValues()
        {
            IsDamageUpgradeEnabled = false;
        }
    }
}
