using MeteorVoyager.Assets.Localization.Scripts.TextsNS;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;

namespace MeteorVoyager
{
    public class SettingsTextsController : MonoBehaviour
    {


        [SerializeField] TextMeshProUGUI _settingsTitle;
        [SerializeField] TextMeshProUGUI _musicVolume;
        [SerializeField] TextMeshProUGUI _soundsVolume;
        [SerializeField] Text _trails;
        [SerializeField] Text _particles;
        [SerializeField] TextMeshProUGUI _starsAmount;
        [SerializeField] TextMeshProUGUI _language;
        void Awake()
        {
            SettingsTexts texts = Texts.SettingsTexts;
            _settingsTitle.text = texts.SettingsTitle;
            _musicVolume.text = texts.MusicVolume;
            _soundsVolume.text = texts.SoundsVolume;
            _trails.text = texts.Trails;
            _particles.text = texts.Particles;
            _starsAmount.text = texts.StarsAmount;
            _language.text = texts.Language;
        }
    }
}
