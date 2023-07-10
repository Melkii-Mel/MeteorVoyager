using MeteorVoyager.Assets.Localization.Scripts;
using System;
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
        public static Texts Texts { get; private set; } = UpdateTexts(MainGameStatsHolder.Settings.Language);

        public static Action AfterRelocation;
        
        #region Update Texts Methods
        static Texts UpdateTexts(string Language)
        {
            if(Enum.TryParse(Language, out Languages language))
            {
                return UpdateTexts(language);
            }
            return UpdateTexts(Languages.en);
        }
        static Texts UpdateTexts(Languages language)
        {
            Texts = LanguageDeserializer.Deserialize(language);
            return Texts;
        }
        #endregion
    }
}