using System;
using Localization.Scripts.TextsNS;
using UnityEngine.Serialization;

namespace Localization.Scripts
{
    [Serializable]
    public class Texts
    {
        public const string FILE_PATH = "Assets\\Localization\\LanguageFiles\\";

        [FormerlySerializedAs("ButtonTexts")] public ButtonTexts buttonTexts;
        [FormerlySerializedAs("CurrencyTexts")] public CurrencyTexts currencyTexts;
        [FormerlySerializedAs("StageTexts")] public GameProgressionStages stageTexts;
        [FormerlySerializedAs("OtherTexts")] public OtherTexts otherTexts;
        [FormerlySerializedAs("TimersTexts")] public TimersTexts timersTexts;
        [FormerlySerializedAs("SettingsTexts")] public SettingsTexts settingsTexts;
    }
}