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

        public void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(ChangeLanguage);
        }

        public void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(ChangeLanguage);
        }

        private void ChangeLanguage()
        {
            GameStats.UpdateTexts(text);
            GameStats.MainGameStatsHolder.Settings.Language = language;
        }
    }
}
