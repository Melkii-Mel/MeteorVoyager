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


        [FormerlySerializedAs("_settingsTitle")] [SerializeField] private TextMeshProUGUI settingsTitle;
        [FormerlySerializedAs("_musicVolume")] [SerializeField] private TextMeshProUGUI musicVolume;
        [FormerlySerializedAs("_soundsVolume")] [SerializeField] private TextMeshProUGUI soundsVolume;
        [FormerlySerializedAs("_trails")] [SerializeField] private Text trails;
        [FormerlySerializedAs("_particles")] [SerializeField] private Text particles;
        [FormerlySerializedAs("_starsAmount")] [SerializeField] private TextMeshProUGUI starsAmount;
        [FormerlySerializedAs("_language")] [SerializeField] private TextMeshProUGUI language;

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
        }
    }
}
