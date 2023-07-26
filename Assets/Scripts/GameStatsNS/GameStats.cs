using System;
using Localization.Scripts;
using UnityEngine;

namespace GameStatsNS
{
    public static class GameStats
    {
        private static bool IsPlaying()
        {
            return Application.isPlaying;
        }
        public static bool IsSomeFieldEnabled { get; set; }
        public static bool IsDamageUpgradeEnabled { get; set; }

        public static SavesStatHolder SavesStatHolder { get; set; } = new(IsPlaying, 1);
        public static GameStatsHolder MainGameStatsHolder { get; set; } = new(SavesStatHolder.Save.SaveIndex, IsPlaying, 1);

        public static Texts Texts { get; set; }
        
        #region Update Texts Methods

        public static Texts UpdateTexts(TextAsset languageFile)
        {
            Texts = LanguageDeserializer.Deserialize(languageFile);
            return Texts;
        }

        private static Texts UpdateTexts(string languageFileContent)
        {
            Debug.Log(languageFileContent);
            Texts = LanguageDeserializer.Deserialize(languageFileContent);
            return Texts;
        }
        #endregion
    }
}