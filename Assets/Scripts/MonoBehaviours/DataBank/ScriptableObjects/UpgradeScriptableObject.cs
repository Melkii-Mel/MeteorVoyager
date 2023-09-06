using System;
using GameStatsNS;
using Localization.Scripts;
using MonoBehaviours.DataBank.Enums;
using UnityEngine;

namespace MonoBehaviours.DataBank.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "DataBankUpgrade", menuName = "Custom/DataBankUpgrade", order = 0)]
    public class UpgradeScriptableObject : ScriptableObject
    {
        [SerializeField] private UpgradeEnum upgradeEnum;
        [SerializeField] private bool oneTimeUpgrade;
        [SerializeField] private LanguageTextSet nameText;
        [SerializeField] private LanguageTextSet descText;
        [SerializeField] private Sprite sprite;

        private Language Language => GameStats.MainGameStatsHolder.Settings.Language;

        public Sprite Sprite => sprite;
        public bool OneTimeUpgrade => oneTimeUpgrade;
        public string UpgradeName => nameText[Language];
        public string UpgradeDesc => descText[Language];
        public UpgradeEnum UpgradeEnum => upgradeEnum;
        public int LvL => throw new NotImplementedException();
        public InfiniteInteger Cost => _formulas[upgradeEnum](LvL);

        private readonly UpgradeFormulas _formulas = new();
        
        
        [Serializable]
        private class LanguageText
        {
            [SerializeField] private Language lan;
            [SerializeField] private string text;
            public Language Language => lan;
            public string Text => text;
        }
        [Serializable]
        private class LanguageTextSet
        {
            [SerializeField] private LanguageText[] languageTexts;
            public string this[Language language] 
            {
                get
                {
                    foreach (var languageText in languageTexts)
                    {
                        if (languageText.Language == language)
                        {
                            return languageText.Text;
                        }
                    }
                    throw new ArgumentException($"Language {language} is not implemented");
                }
            }
        }
    }
}