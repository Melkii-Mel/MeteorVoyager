using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public static class GameStats
    {
        public static bool IsPlaying()
        {
            return Application.isPlaying;
        }
        public static bool IsSomeFieldEnabled { get; set; }
        public static bool IsDamageUpgradeEnabled { get; set; }

        public static SavesStatHolder SavesStatHolder { get; set; } = new(IsPlaying, 1);
        public static GameStatsHolder MainGameStatsHolder { get; set; } = new(SavesStatHolder.Save.SaveIndex, IsPlaying, 1);
        public static System.Action OnRelocation { get; set; }
    }
}