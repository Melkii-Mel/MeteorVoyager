using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.Serialization;
using System;

namespace Assets.Scripts.GameStatsNameSpace
{
    [Serializable]
    public class SettingsGameStats : IGameStats
    {
        public static SettingsGameStats Instance = new();
        SettingsGameStats()
        {
            new DataLoaderSaver<IGameStats>().LoadGameStat(this);
            GameStats.AppendIGameStats(this);
        }
        public float SoundsVolume { get; set; } = 1;
        public float MusicVolume { get; set; } = 1;
        public float StarsAmountMultiplier { get; set; } = 1;
        public bool TrailsEnabled { get; set; } = true;
        public bool ParticlesEnabled { get; set; } = true;

        public string FileName { get; } = "Settings";

        public void AddToAutoSaver()
        {
            AutoSaver<IGameStats>.GameStats.Add(this);
        }
    }
}