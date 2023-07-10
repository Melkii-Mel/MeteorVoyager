namespace MeteorVoyager.Assets.Localization.Scripts
{
    using TextsNS;
    using System;
    [Serializable]
    public class Texts
    {
        public const string FILE_PATH = "Assets\\Localization\\LanguageFiles\\";

        public ButtonTexts ButtonTexts;
        public CurrencyTexts CurrencyTexts;
        public GameProgressionStages StageTexts;
        public OtherTexts OtherTexts;
    }
}