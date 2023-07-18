using System;
using Localization.Scripts;
using UnityEngine;

namespace GameStatsNS
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
        public static Texts Texts { get; private set; } = UpdateTexts((string)MainGameStatsHolder.Settings.Language);

        public static Action AfterRelocation;
        
        #region Update Texts Methods

        public static Texts UpdateTexts(string language)
        {
            if(Enum.TryParse(language, out Languages enumLanguage))
            {
                return UpdateTexts(enumLanguage);
            }
            return UpdateTexts(Languages.En);
        }

        public static Texts UpdateTexts(Languages language)
        {
            Texts = LanguageDeserializer.Deserialize(language);
            return Texts;
        }
        #endregion
    }
}