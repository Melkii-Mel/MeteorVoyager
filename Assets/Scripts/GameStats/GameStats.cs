using Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class GameStats : MonoBehaviour
    {
        public static bool IsSomeFieldEnabled { get; set; }
        public static bool IsDamageUpgradeEnabled { get; set; }

        static List<IGameStats> ThisGameStats { get; set; } = new();
        static AutoSaver<IGameStats> _autoSaver = new();
        static DataLoaderSaver<IGameStats> _loaderSaver = new();

        public static System.Action onRelocation;

        public GameStats(params IGameStats[] gameStats)
        {
            IsSomeFieldEnabled = false;
            IsDamageUpgradeEnabled = false;
            ThisGameStats = new(gameStats);
        }

        /// <summary>
        /// When you use AppendIGameStats, it will automatically set to autosaving (so be careful lol)
        /// </summary>
        /// <param name="gameStats"> object that implement IGameStats. If it implements IDeletableOnRelocation then DeleteOnRelocation will be called on relocation</param>
        public static void AppendIGameStats(IGameStats gameStats)
        {
            ThisGameStats.Add(gameStats);
            AutoSaver<IGameStats>.GameStats.Add(gameStats);
        }

        public void RemoveIGameStats(IGameStats gameStats)
        {
            ThisGameStats.Remove(gameStats);
        }
        public void SaveAll()
        {
            _loaderSaver.SaveAllGameStats(ThisGameStats.ToArray());
        }

        public void Save(IGameStats gameStats)
        {
            _loaderSaver.SaveGameStat(gameStats);
        }

        public void LoadAll()
        {
            _loaderSaver.LoadAllGameStats(ThisGameStats.ToArray());
        }
        public void Load(IGameStats gameStats)
        {
            _loaderSaver.LoadGameStat(gameStats);
        }

        public void ChangeDataRelocation()
        {
            _loaderSaver.DeleteDataOnRelocation(ThisGameStats.ToArray());
            onRelocation?.Invoke();
        }
    }
}