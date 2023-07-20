using GameStatsNS;
using Localization.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Settings.Languages
{
    public class LanguageButton : MonoBehaviour
    {
        [SerializeField] private TextAsset text;
        [SerializeField] private Language language;

        public void Start()
        {
            GetComponent<Button>().onClick.AddListener(ChangeLanguage);
        }
        private void ChangeLanguage()
        {
            GameStats.UpdateTexts(text);
            GameStats.MainGameStatsHolder.Settings.Language = language;
        }
    }
}
