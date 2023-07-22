using System;
using Localization.Scripts.TextsNS;

namespace Localization.Scripts
{
    [Serializable]
    public class Texts
    {
        public ButtonTexts ButtonTexts { get; set; }
        public CurrencyTexts CurrencyTexts { get; set; }
        public GameProgressionStageTexts StageTexts { get; set; }
        public OtherTexts OtherTexts { get; set; }
        public TimersTexts TimersTexts { get; set; }
        public SettingsTexts SettingsTexts { get; set; }
    }
}