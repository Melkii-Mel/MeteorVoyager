using MeteorVoyager.Assets.Scripts;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.Serialization;
using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;
using System;

namespace Assets.Scripts.GameStatsNameSpace
{
    [Serializable]
    public class Currency : IGameStats, IDeletableOnRelocation, IMySerializable
    {
        public static Currency Instance { get; private set; } = new();
        Currency()
        {
            GameStats.AppendIGameStats(this);
            new DataLoaderSaver<IGameStats>().LoadGameStat(this);
        }
        public string FileName { get; } = "Currency";
        public InfiniteInteger Data { get; set; } = 0;
        public InfiniteInteger Balance { get; set; } = 0;

        public void AddToAutoSaver()
        {
            AutoSaver<IGameStats>.GameStats.Add(this);
        }

        public void ResetDeletableValues()
        {
            this.Data = 0;
        }
    }
}
