using static GameStatsNS.GameStats;
using Localization.Scripts.TextsNS;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsTextsController : MonoBehaviour
    {


        [SerializeField] private TextMeshProUGUI settingsTitle;
        [SerializeField] private TextMeshProUGUI musicVolume;
        [SerializeField] private TextMeshProUGUI soundsVolume;
        [SerializeField] private Text trails;
        [SerializeField] private Text particles;
        [SerializeField] private TextMeshProUGUI starsAmount;
        [SerializeField] private TextMeshProUGUI language;
        [SerializeField] private Text safeScreen;


        private void Awake()
        {
            SettingsTexts texts = Texts.SettingsTexts;
            settingsTitle.text = texts.SettingsTitle;
            musicVolume.text = texts.MusicVolume;
            soundsVolume.text = texts.SoundsVolume;
            trails.text = texts.Trails;
            particles.text = texts.Particles;
            starsAmount.text = texts.StarsAmount;
            language.text = texts.Language;
            safeScreen.text = texts.SafeScreen;
        }
    }
}
