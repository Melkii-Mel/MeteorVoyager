using GameStatsNS;
using UnityEngine;
using UnityEngine.UI;

namespace Settings.Languages
{
    public class LanguageButton : MonoBehaviour
    {
        [SerializeField] private Localization.Scripts.Languages language;

        public void Start()
        {
            GetComponent<Button>().onClick.AddListener(ChangeLanguage);
        }
        private void ChangeLanguage()
        {
            GameStats.UpdateTexts(language);
        }
    }
}
