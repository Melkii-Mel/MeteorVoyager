using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.Serialization;
using System;

namespace Assets.Scripts.GameStatsNameSpace
{
    [Serializable]
    public class DataUpgradesGameStats : IGameStats
    {
        public static DataUpgradesGameStats Instance = new DataUpgradesGameStats();

        DataUpgradesGameStats()
        {
            new DataLoaderSaver<IGameStats>().LoadGameStat(this);
            GameStats.AppendIGameStats(this);
        }

        public int BossSpawnChanceLvl { get; set; } = 0;
        public int ForceFieldLvl { get; set; } = 0;
        public int UltracoinSpawnChanceLvl { get; set; } = 0;
        public int ScreenExplosionLvl { get; set; } = 0;
        public int MultishotLvl { get; set; } = 0;
        public string FileName { get; } = "DataUpgrades";

        public void AddToAutoSaver()
        {
            AutoSaver<IGameStats>.GameStats.Add(this);
        }
    }
}
